using Goexw.Business.Models;
using Goexw.Config;
using Goexw.Helper;
using MsStore.Mfl.Core.Models.Request;
using MsStore.Mfl.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using AtomOrder = MsStore.Mfl.Core.Models.SalesOrder;

namespace Goexw.Business.Managers
{
    public class SalesOrderManager
    {
        public SalesOrderResponseModel PrepareOrder(Order order, IPrincipal user)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            var requestModel = BuildSalesOrderRequestModel(order, user);
            requestModel.ActionCode = MsStore.Mfl.Core.Enumeration.SalesOrderAction.Prepare;
            return AtomCommerceProxy.ProcessSalesOrderRequest(requestModel);
        }

        public SalesOrderResponseModel CommitOrder(Order order, IPrincipal user)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            var requestModel = BuildSalesOrderRequestModel(order, user);
            requestModel.ActionCode = MsStore.Mfl.Core.Enumeration.SalesOrderAction.Commit;
            return AtomCommerceProxy.ProcessSalesOrderRequest(requestModel);
        }

        public SalesOrderResponseModel DirectCommitOrder(Order order, IPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var requestModel = BuildSalesOrderRequestModel(null, user);
            requestModel.ActionCode = MsStore.Mfl.Core.Enumeration.SalesOrderAction.DirectCommit;
            return AtomCommerceProxy.ProcessSalesOrderRequest(requestModel);
        }

        public SalesOrderResponseModel OrderHistory(IPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var requestModel = BuildSalesOrderRequestModel(null, user);
            requestModel.ActionCode = MsStore.Mfl.Core.Enumeration.SalesOrderAction.QueryList;
            return AtomCommerceProxy.ProcessSalesOrderRequest(requestModel);
        }

        private SalesOrderRequestModel BuildSalesOrderRequestModel(Order order, IPrincipal user)
        {
            SalesOrderRequestModel requestModel = new SalesOrderRequestModel();
            requestModel.ChannelId = SystemConfig.ChannelId;
            if (order != null)
            {
                if (order.AtomOrder != null)
                {
                    requestModel.SalesOrder = order.AtomOrder;
                }
                else
                {
                    requestModel.SalesOrder = ModelConvert.ConvertToAtomOrder(order);
                }
            }
            else
            {
                requestModel.SalesOrder = new AtomOrder();
            }
            requestModel.SalesOrder.AuthUserId = user.Identity.Name;
            requestModel.SalesOrder.AuthProvider = user.Identity.AuthenticationType;
            return requestModel;
        }
    }
}