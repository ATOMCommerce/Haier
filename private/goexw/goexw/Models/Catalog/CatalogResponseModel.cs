using System;
using System.Collections.ObjectModel;
using MsStore.Mfl.Core.Enumeration;
using MsStore.Mfl.Core.Models.Request;

namespace Mock.MsStore.Mfl.Core.Models.Response
{
    public class CatalogResponseModel
    {
        public CatalogAction ActionCode { get; set; }

        public Collection<CatalogItem> CatalogItems { get; set; }

    }
}
