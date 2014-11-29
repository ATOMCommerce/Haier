using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goexw.Models
{
    public class MockDataProvider
    {
        public static List<MockProductCategory> GetProductCategories()
        {
            var mockData = new List<MockProductCategory>();
            mockData.Add(new MockProductCategory
            {
                Id = 1,
                Text = "Apparel, Textiles & Accessories",
                Children = new List<MockProductCategory> 
                {
                    new MockProductCategory {
                        Id = 101, Text = "Apparel", Children = new List<MockProductCategory> 
                        {
                            new MockProductCategory {Id = 10101, Text = "Apparel sub 1"},
                            new MockProductCategory {Id = 10102, Text = "Apparel sub 2"},
                            new MockProductCategory {Id = 10103, Text = "Apparel sub 3"},
                            new MockProductCategory {Id = 10104, Text = "Apparel sub 4"},
                            new MockProductCategory {Id = 10105, Text = "Apparel sub 5"}
                        }
                    }, 
                    new MockProductCategory {
                        Id = 102, Text = "Textiles", Children = new List<MockProductCategory>
                        {
                            new MockProductCategory {Id = 102001, Text = "Textiles sub 1"}
                        }
                    },
                    new MockProductCategory {Id = 103, Text = "Accessories"}
                },

            });

            mockData.Add(new MockProductCategory
            {
                Id = 2,
                Text = "Auto & Transportation",
                Children = new List<MockProductCategory> 
                {
                    new MockProductCategory {
                        Id = 201, Text = "Auto", Children = new List<MockProductCategory> 
                        {
                            new MockProductCategory {Id = 20101, Text = "Auto sub 1"},
                            new MockProductCategory {Id = 20102, Text = "Auto sub 2"},
                            new MockProductCategory {Id = 20103, Text = "Auto sub 3"},
                            new MockProductCategory {Id = 20104, Text = "Auto sub 4"},
                            new MockProductCategory {Id = 20105, Text = "Auto sub 5"}
                        }
                    }, 
                    new MockProductCategory {
                        Id = 202, Text = "Transportation", Children = new List<MockProductCategory>
                        {
                            new MockProductCategory {Id = 201, Text = "Transportation sub 1"}
                        }
                    }
                },

            });

            mockData.Add(new MockProductCategory { Id = 3, Text = "Machinery, Hardware &amp; Tools" });
            mockData.Add(new MockProductCategory { Id = 4, Text = "Gifts, Sports &amp; Toys" });
            mockData.Add(new MockProductCategory { Id = 5, Text = "Home, Lights &amp; Construction" });
            mockData.Add(new MockProductCategory { Id = 6, Text = "Health &amp; Beauty" });
            mockData.Add(new MockProductCategory { Id = 7, Text = "Jewelry, Bags &amp; Shoes" });
            mockData.Add(new MockProductCategory { Id = 8, Text = "Electrical Equipment, Components &amp; Telecom" });
            mockData.Add(new MockProductCategory { Id = 9, Text = "Agriculture &amp; Food" });
            mockData.Add(new MockProductCategory { Id = 10, Text = "Packaging, Advertising &amp; Office" });
            mockData.Add(new MockProductCategory { Id = 11, Text = "Metallurgy, Chemicals, Rubber &amp; Plastics" });
            mockData.Add(new MockProductCategory
            {
                Id = 12,
                Text = "Other",
                Children = new List<MockProductCategory> 
            { 
                new MockProductCategory {
                        Id = 1201, Text = "Other Stuffs", Children = new List<MockProductCategory>
                        {
                            new MockProductCategory {Id = 120101, Text = "Transportation sub 1"},
                            new MockProductCategory {Id = 120102, Text = "Transportation sub 1", Children = new List<MockProductCategory> 
                            {
                                new MockProductCategory {Id = 12010201, Text = "A deep one"}
                            }}
                        }
                    }
            }
            });

            return mockData;
        }

    }
}