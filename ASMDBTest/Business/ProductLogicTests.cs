using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASMDBTest.Business
{
    [TestClass]
    public class ProductLogicTests
    {
        public class Product
        {
            public bool IsMarkdown { get; set; }
        }

        [TestMethod]
        public void ProductMarkdown_TogglesStatus()
        {
            var product = new Product { IsMarkdown = false };
            product.IsMarkdown = !product.IsMarkdown;
            Assert.IsTrue(product.IsMarkdown);
            product.IsMarkdown = !product.IsMarkdown;
            Assert.IsFalse(product.IsMarkdown);
        }
    }
} 