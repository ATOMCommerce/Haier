using System;
using Mock.MsStore.Mfl.Core.Enumeration;
using MsStore.Mfl.Core.Models.Request;

namespace Mock.MsStore.Mfl.Core.Models.Request
{
    public class CatalogRequestModel : RequestModelBase
    {
        public CatalogAction ActionCode { get; set; }

        public string SupplierId { get; set; }

        public DateTime? LastSync { get; set; }

        // used when search action
        public string SearchCategory { get; set; }

        // used when search action
        public string SearchKeyword { get; set; }

        public override string GetActionCodeString()
        {
            return this.ActionCode.ToString();
        }
    }
}
