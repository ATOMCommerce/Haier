using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Goexw.Models.Mock;

namespace Goexw.Models
{
    public class MockDataProvider
    {

        public static List<MockPromotion> GetPromotions()
        {
            var mockData = new List<MockPromotion>();

            mockData.Add(new MockPromotion { Id = 1, Name = "House in sale", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp1.jpg" });
            mockData.Add(new MockPromotion { Id = 2, Name = "Scroller", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp2.jpg" });
            mockData.Add(new MockPromotion { Id = 3, Name = "Ball in sale", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp3.jpg" });
            mockData.Add(new MockPromotion { Id = 4, Name = "Mobile Phones", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp4.jpg" });
            mockData.Add(new MockPromotion { Id = 5, Name = "Child Food", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp5.jpg" });

            mockData.Add(new MockPromotion { Id = 6, Name = "House in sale", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp1.jpg" });
            mockData.Add(new MockPromotion { Id = 7, Name = "Scroller", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp2.jpg" });
            mockData.Add(new MockPromotion { Id = 8, Name = "Ball in sale", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp3.jpg" });
            mockData.Add(new MockPromotion { Id = 9, Name = "Mobile Phones", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp4.jpg" });
            mockData.Add(new MockPromotion { Id = 10, Name = "Child Food", Descritpion = "Beautiful house in sale", ImageUrl = "/Content/mockimgs/mp5.jpg" });

            return mockData;
        }

        public static List<MockProductCategory> GetProductCategories()
        {
            var mockData = new List<MockProductCategory>();
            mockData.Add(new MockProductCategory
            {
                id = 1,
                text = "Apparel, Textiles & Accessories",
                children = new List<MockProductCategory> 
                {
                    new MockProductCategory {
                        id = 101, text = "Apparel", children = new List<MockProductCategory> 
                        {
                            new MockProductCategory {id = 10101, text = "Apparel sub 1"},
                            new MockProductCategory {id = 10102, text = "Apparel sub 2"},
                            new MockProductCategory {id = 10103, text = "Apparel sub 3"},
                            new MockProductCategory {id = 10104, text = "Apparel sub 4"},
                            new MockProductCategory {id = 10105, text = "Apparel sub 5"}
                        }
                    }, 
                    new MockProductCategory {
                        id = 102, text = "Textiles", children = new List<MockProductCategory>
                        {
                            new MockProductCategory {id = 102001, text = "Textiles sub 1"}
                        }
                    },
                    new MockProductCategory {id = 103, text = "Accessories"}
                },

            });

            mockData.Add(new MockProductCategory
            {
                id = 2,
                text = "Auto & Transportation",
                children = new List<MockProductCategory> 
                {
                    new MockProductCategory {
                        id = 201, text = "Auto", children = new List<MockProductCategory> 
                        {
                            new MockProductCategory {id = 20101, text = "Auto sub 1"},
                            new MockProductCategory {id = 20102, text = "Auto sub 2"},
                            new MockProductCategory {id = 20103, text = "Auto sub 3"},
                            new MockProductCategory {id = 20104, text = "Auto sub 4"},
                            new MockProductCategory {id = 20105, text = "Auto sub 5"}
                        }
                    }, 
                    new MockProductCategory {
                        id = 202, text = "Transportation", children = new List<MockProductCategory>
                        {
                            new MockProductCategory {id = 201, text = "Transportation sub 1"}
                        }
                    }
                },

            });

            mockData.Add(new MockProductCategory { id = 3, text = "Machinery, Hardware & Tools" });
            mockData.Add(new MockProductCategory { id = 4, text = "Gifts, Sports & Toys" });
            mockData.Add(new MockProductCategory { id = 5, text = "Home, Lights & Construction" });
            mockData.Add(new MockProductCategory { id = 6, text = "Health & Beauty" });
            mockData.Add(new MockProductCategory { id = 7, text = "Jewelry, Bags & Shoes" });
            mockData.Add(new MockProductCategory { id = 8, text = "Electrical Equipment, Components & Telecom" });
            mockData.Add(new MockProductCategory { id = 9, text = "Agriculture & Food" });
            mockData.Add(new MockProductCategory { id = 10, text = "Packaging, Advertising & Office" });
            mockData.Add(new MockProductCategory { id = 11, text = "Metallurgy, Chemicals, Rubber & Plastics" });
            mockData.Add(new MockProductCategory
                {
                    id = 12,
                    text = "Other",
                    children = new List<MockProductCategory> 
                    { 
                    new MockProductCategory {
                            id = 1201, text = "Other Stuffs", children = new List<MockProductCategory>
                            {
                                new MockProductCategory {id = 120101, text = "Transportation sub 1"},
                                new MockProductCategory {id = 120102, text = "Transportation sub 1", 
                                    children = new List<MockProductCategory> 
                                    {
                                        new MockProductCategory {id = 12010201, text = "A deep one"}
                                    }}
                            }
                        }
                    }
                });

            return mockData;
        }


        public static List<MockShipMethod> GetShipMethods()
        {
            var methods = new List<MockShipMethod>
            {
                new MockShipMethod() {Code = 0, Name = "Air"},
                new MockShipMethod() {Code = 1, Name = "Ship"},
                new MockShipMethod() {Code = 2, Name = "Train"}
            };

            return methods;
        }

        public static String GetCatalogReponse()
        {
            return MockCatalogResponse.GetHttpBody();
        }

    }
}