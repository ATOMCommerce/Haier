using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Models
{
    public class MockProductCategory
    {
        public int Id { get; set; }
        public String Text { get; set; }

        public List<MockProductCategory> Children { get; set; }
    }
}