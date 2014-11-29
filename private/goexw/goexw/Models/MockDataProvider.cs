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
                Name = "Apparel, Textiles & Accessories",
                SubCategories = new List<MockProductCategory> 
                {
                    new MockProductCategory {
                        Id = 101, Name = "Apparel", SubCategories = new List<MockProductCategory> 
                        {
                            new MockProductCategory {Id = 10101, Name = "Apparel sub 1"},
                            new MockProductCategory {Id = 10102, Name = "Apparel sub 2"},
                            new MockProductCategory {Id = 10103, Name = "Apparel sub 3"},
                            new MockProductCategory {Id = 10104, Name = "Apparel sub 4"},
                            new MockProductCategory {Id = 10105, Name = "Apparel sub 5"}
                        }
                    }, 
                    new MockProductCategory {
                        Id = 102, Name = "Textiles", SubCategories = new List<MockProductCategory>
                        {
                            new MockProductCategory {Id = 102001, Name = "Textiles sub 1"}
                        }
                    },
                    new MockProductCategory {Id = 103, Name = "Accessories"}
                },

            });

            mockData.Add(new MockProductCategory
            {
                Id = 2,
                Name = "Auto & Transportation",
                SubCategories = new List<MockProductCategory> 
                {
                    new MockProductCategory {
                        Id = 201, Name = "Auto", SubCategories = new List<MockProductCategory> 
                        {
                            new MockProductCategory {Id = 20101, Name = "Auto sub 1"},
                            new MockProductCategory {Id = 20102, Name = "Auto sub 2"},
                            new MockProductCategory {Id = 20103, Name = "Auto sub 3"},
                            new MockProductCategory {Id = 20104, Name = "Auto sub 4"},
                            new MockProductCategory {Id = 20105, Name = "Auto sub 5"}
                        }
                    }, 
                    new MockProductCategory {
                        Id = 202, Name = "Transportation", SubCategories = new List<MockProductCategory>
                        {
                            new MockProductCategory {Id = 201, Name = "Transportation sub 1"}
                        }
                    }
                },

            });

            mockData.Add(new MockProductCategory { Id = 3, Name = "Machinery, Hardware &amp; Tools" });
            mockData.Add(new MockProductCategory { Id = 4, Name = "Gifts, Sports &amp; Toys" });
            mockData.Add(new MockProductCategory { Id = 5, Name = "Home, Lights &amp; Construction" });
            mockData.Add(new MockProductCategory { Id = 6, Name = "Health &amp; Beauty" });
            mockData.Add(new MockProductCategory { Id = 7, Name = "Jewelry, Bags &amp; Shoes" });
            mockData.Add(new MockProductCategory { Id = 8, Name = "Electrical Equipment, Components &amp; Telecom" });
            mockData.Add(new MockProductCategory { Id = 9, Name = "Agriculture &amp; Food" });
            mockData.Add(new MockProductCategory { Id = 10, Name = "Packaging, Advertising &amp; Office" });
            mockData.Add(new MockProductCategory { Id = 11, Name = "Metallurgy, Chemicals, Rubber &amp; Plastics" });
            mockData.Add(new MockProductCategory
            {
                Id = 12,
                Name = "Other",
                SubCategories = new List<MockProductCategory> 
            { 
                new MockProductCategory {
                        Id = 1201, Name = "Other Stuffs", SubCategories = new List<MockProductCategory>
                        {
                            new MockProductCategory {Id = 120101, Name = "Transportation sub 1"},
                            new MockProductCategory {Id = 120102, Name = "Transportation sub 1", SubCategories = new List<MockProductCategory> 
                            {
                                new MockProductCategory {Id = 12010201, Name = "A deep one"}
                            }}
                        }
                    }
            }
            });

            return mockData;
        }

    }
}