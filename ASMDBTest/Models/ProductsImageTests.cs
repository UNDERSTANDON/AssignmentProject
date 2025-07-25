using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Models
{
    [TestClass]
    public class ProductsImageTests
    {
        [TestMethod]
        public void Can_Set_And_Get_All_Properties()
        {
            var image = new ProductsImage
            {
                Image_ID = 3,
                Prod_ID = 4
            };
            Assert.AreEqual(3, image.Image_ID);
            Assert.AreEqual(4, image.Prod_ID);
        }
    }
} 