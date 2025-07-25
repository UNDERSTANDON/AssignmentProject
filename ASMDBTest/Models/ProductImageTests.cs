using ASMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Models
{
    [TestClass]
    public class ProductImageTests
    {
        [TestMethod]
        public void Can_Set_And_Get_All_Properties()
        {
            var image = new ProductImage
            {
                Image_ID = 1,
                Prod_ID = 2
            };
            Assert.AreEqual(1, image.Image_ID);
            Assert.AreEqual(2, image.Prod_ID);
        }
    }
} 