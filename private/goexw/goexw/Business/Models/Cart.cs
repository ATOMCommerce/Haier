using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Goexw.Business.Models
{
    public class Cart
    {
        public string UserId { get; set; }

        public string AuthType { get; set; }

        public IList<SalesLine> SalesLines { get; set; }
    }
}