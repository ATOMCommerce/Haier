// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphHelper.cs" company="atom-commerce">
//   Copyright @ atom-commerce 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the GraphHelper type.
// </summary>
// <author>
//   Pengzhi Sun (pzsun@atom-commerce.com)
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Exiao.Demo.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;

    using Microsoft.Azure.ActiveDirectory.GraphClient;
    using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    /// <summary>
    /// Defines the GraphHelper type.
    /// </summary>
    public static class GraphHelper
    {
        #region Methods

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <param name="claimPrincipal">The claim principal.</param>
        /// <returns>The task.</returns>
        public static async Task<IEnumerable<Group>> GetGroups(ClaimsPrincipal claimPrincipal)
        {
            var groupIds = claimPrincipal.FindAll("groups").Select(c => c.Value).ToList();

            return await GetGroups(groupIds);
        }

        /// <summary>
        /// Creates the group if not exist.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="description">The description.</param>
        /// <returns>The task.</returns>
        public static async Task<IGroup> CreateGroupIfNotExist(string groupName, string description)
        {
            var graphClient = GetGraphClient();

            var pagedResult = await graphClient.Groups.Where(g => g.DisplayName.Equals(groupName)).ExecuteAsync();

            var graph = pagedResult.CurrentPage.FirstOrDefault();

            if (graph == null)
            {
                graph = new Group
                {
                    DisplayName = groupName,
                    Description = description,
                    MailNickname = groupName,
                    MailEnabled = false,
                    SecurityEnabled = true,
                };
                await graphClient.Groups.AddGroupAsync(graph);
            }

            return graph;
        }

        /// <summary>
        /// Gets all groups.
        /// </summary>
        /// <returns>The task.</returns>
        public static async Task<IEnumerable<IGroup>> GetAllGroups()
        {
            var groups = new List<IGroup>();

            var graphClient = GetGraphClient();

            var pagedResult = await graphClient.Groups.ExecuteAsync();
            groups.AddRange(pagedResult.CurrentPage);

            while (pagedResult.MorePagesAvailable)
            {
                pagedResult = await pagedResult.GetNextPageAsync();
                groups.AddRange(pagedResult.CurrentPage);
            }

            return groups;
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <param name="groupIds">The group ids.</param>
        /// <returns>The task.</returns>
        /// <exception cref="Exception">Directory Object Not Found.</exception>
        public static async Task<IEnumerable<Group>> GetGroups(IList<string> groupIds)
        {
            var groups = new List<Group>();

            var graphClient = GetGraphClient();

            var batchCount = 0;
            var requests = new List<Task<IBatchElementResult[]>>();
            var batch = new List<IReadOnlyQueryableSetBase<IDirectoryObject>>();
            var idIndex = groupIds.GetEnumerator();
            var nextId = groupIds.GetEnumerator();
            nextId.MoveNext();
            while (idIndex.MoveNext())
            {
                var thisId = idIndex.Current;
                batch.Add(graphClient.DirectoryObjects.Where(o => o.ObjectId.Equals(thisId)));
                batchCount++;
                if (nextId.MoveNext() && batchCount != 5)
                {
                    continue;
                }

                requests.Add(graphClient.Context.ExecuteBatchAsync(batch.Cast<IReadOnlyQueryableSetBase>().ToArray()));
                batchCount = 0;
                batch.Clear();
            }

            var responses = await Task.WhenAll(requests);
            foreach (var query in responses.SelectMany(batchResult => batchResult))
            {
                if (query.SuccessResult != null && query.FailureResult == null)
                {
                    var group = query.SuccessResult.CurrentPage.First() as Group;
                    if (group != null)
                    {
                        groups.Add(@group);
                    }
                }
                else
                {
                    throw new Exception("Directory Object Not Found.");
                }
            }

            return groups;
        }

        /// <summary>
        /// Gets the application roles.
        /// </summary>
        /// <returns>The task.</returns>
        public static async Task<IList<AppRole>> GetAppRoles()
        {
            var application = await GetCurrentApplication();

            return application.AppRoles;
        }

        /// <summary>
        /// Gets the current application.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>The task.</returns>
        public static async Task<IApplication> GetCurrentApplication(string accessToken = null)
        {
            var graphClient = GetGraphClient(accessToken);
            var tenantApps =
                await graphClient.Applications.Where(a => a.AppId.Equals(ConfigHelper.ClientId)).ExecuteAsync();

            return tenantApps.CurrentPage[0];
        }

        /// <summary>
        /// Gets the display name of the current.
        /// </summary>
        /// <returns>The current display name.</returns>
        public static string GetCurrentDisplayName()
        {
            var userObjectId = GetCurrentUserObjectId();

            var graphClient = GetGraphClient();

            var user = graphClient.Users.Where(u => u.ObjectId == userObjectId).ExecuteAsync().Result.CurrentPage[0];

            return user.DisplayName;
        }

        /// <summary>
        /// Gets the graph client.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>The active directory client instance.</returns>
        public static ActiveDirectoryClient GetGraphClient(string accessToken = null)
        {
            return new ActiveDirectoryClient(
                new Uri(ConfigHelper.GraphServiceRoot),
                async () =>
                {
                    if (accessToken == null)
                    {
                        return await AcquireToken();
                    }

                    return accessToken;
                });
        }

        /// <summary>
        /// Acquires the token.
        /// </summary>
        /// <returns>The task.</returns>
        public static async Task<string> AcquireToken()
        {
            var userObjectId = GetCurrentUserObjectId();
            var userIdentifier = new UserIdentifier(userObjectId, UserIdentifierType.UniqueId);
            var credential = new ClientCredential(ConfigHelper.ClientId, ConfigHelper.AppKey);
            var authContext = new AuthenticationContext(ConfigHelper.Authority, new TokenDbCache(userObjectId));

            var result =
                await authContext.AcquireTokenSilentAsync(ConfigHelper.GraphResourceId, credential, userIdentifier);

            return result.AccessToken;
        }

        /// <summary>
        /// Acquires the token by code.
        /// </summary>
        /// <param name="userObjectId">The user object identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns>The task.</returns>
        public static async Task<string> AcquireTokenByCode(string userObjectId, string code)
        {
            var credential = new ClientCredential(ConfigHelper.ClientId, ConfigHelper.AppKey);
            var authContext = new AuthenticationContext(ConfigHelper.Authority, new TokenDbCache(userObjectId));

            var result =
                await
                authContext.AcquireTokenByAuthorizationCodeAsync(
                    code,
                    new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)),
                    credential,
                    ConfigHelper.GraphResourceId);

            return result.AccessToken;
        }

        /// <summary>
        /// Gets the current user object identifier.
        /// </summary>
        /// <returns>Current user object identifier.</returns>
        private static string GetCurrentUserObjectId()
        {
            return ClaimsPrincipal.Current.FindFirst(ClaimExtendedTypes.ObjectIdentifier).Value;
        } 

        #endregion
    }
}