using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject2
{
    [TestClass]
    public class TestOrderClassLibrary
    {
        [TestMethod]
        public void OrderDetail_ValidArguments_ValidateConstructor()
        {
            OrderDetail item = new OrderDetail("ELECT001", "42 Inch TV", 300.00);
            Assert.AreEqual("ELECT001", item.stockID);
            Assert.AreEqual("42 Inch TV", item.stockName);
            Assert.AreEqual(300.00, item.stockPrice);
        }
    }
}