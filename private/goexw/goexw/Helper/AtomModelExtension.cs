using MsStore.Mfl.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Helper
{
    public static class AtomModelExtension
    {
        public static int GetFrontId(this MsStore.Mfl.Core.Models.Item item)
        {
            var pair = item.OptionalFields.Where(i => i.Key == "FrontId").FirstOrDefault();
            if (pair != null)
            {
                return Convert.ToInt32(pair.Value);
            }

            return -1;
        }

        public static void SetFrontId(this MsStore.Mfl.Core.Models.Item item, int id)
        {
            var pair = item.OptionalFields.Where(i => i.Key == "FrontId").FirstOrDefault();
            if (pair != null)
            {
                pair.Value = id.ToString();
            }
            else
            {
                pair = new CustomKeyValuePair();
                pair.Key = "FrontId";
                pair.Value = id.ToString();
                item.OptionalFields.Add(pair);
            }
        }
    }
}