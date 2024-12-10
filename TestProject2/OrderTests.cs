using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void Order_ValidArguments_ValidInstantiation()
        {
            var order = new Order("Billy Smith", "6780923750");
            Assert.AreEqual("Billy Smith", order.customerName);
            Assert.AreEqual("6780923750", order.customerPhone);
        }

        [TestMethod]
        public void Order_AddDetail_ValidDetail_Added()
        {
            Order order = new("Billy Smith", "6780923750");
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            order.AddOrderDetail(item, 2);

            Assert.AreEqual(1, order.orderDetails.Count);
            Assert.AreEqual(2, order.orderDetails[0].Quantity);
        }
    }
}