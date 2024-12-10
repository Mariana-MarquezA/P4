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

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void OrderDetail_CalculateAmountWithTariffs_PreconditionsNotMet() { 
            OrderDetail item = new ("ELECT001", "42 Inch TV", 300.00);
            item.CalculateAmountWithTariffs();
        }


        [TestMethod]
        public void OrderDetail_CalculateAmountWithTariffs_ElectTarrifApplied() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00)
            {
                // Assume item has been added to an Order
                Quantity = 1,
                OrderNumber = 1,
                DetailNumber = 1
            };

            double result = item.CalculateAmountWithTariffs();
            Assert.AreEqual(315.00, result);
        }

        [TestMethod]
        public void OrderDetail_CalculateAmountWithTariffs_NoElectTarrifApplied() {
            OrderDetail item = new("FURN001", "Sofa", 500.00)
            {
                // Assume item has been added to an Order
                Quantity = 1,
                OrderNumber = 1,
                DetailNumber = 1
            };

            double result = item.CalculateAmountWithTariffs();
            Assert.AreEqual(500.00, result);
        }
    }
}