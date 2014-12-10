using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Models
{
    public class MockProductCategory
    {
        public int id { get; set; }
        public String text { get; set; }

        public List<MockProductCategory> children { get; set; }
    }
}