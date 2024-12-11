using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void Constructor_ValidArguments_ValidInstantiation() {
            Order order = new("Billy Smith", "6780923750");
            Assert.AreEqual("Billy Smith", order.customerName);
            Assert.AreEqual("6780923750", order.customerPhone);
            Assert.AreEqual(0.0, order.taxAmount);
            Assert.AreEqual(0.0, order.totalAmount);
            Assert.AreEqual(0, order.orderDetails.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyCustomerName_ThrowsException() {
            Order order = new("", "6780923750");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullCustomerName_ThrowsException() {
            Order order = new(null, "6780923750");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyCustomerPhone_ThrowsException() {
            Order order = new("Billy Smith", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullCustomerPhone_ThrowsException() {
            Order order = new(null, "6780923750");
        }

        [TestMethod]
        public void DeepCopy_CopiesObject() {
            Order original = new("Billy Smith", "6780923750");
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);

            original.AddOrderDetail(item, 2);

            Order copy = new Order(original);

            Assert.AreEqual(original.customerName, copy.customerName);
            Assert.AreEqual(original.customerPhone, copy.customerPhone);
            Assert.AreEqual(original.orderDetails.Count, copy.orderDetails.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeepCopy_NullOrder_ThrowsException() {
            Order order = null;
            Order copy = new(order);
        }

        [TestMethod]
        public void AddDetail_ValidDetail_Added() {
            Order order = new("Billy Smith", "6780923750");
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            order.AddOrderDetail(item, 2);

            Assert.AreEqual(1, order.orderDetails.Count);
            Assert.AreEqual(2, order.orderDetails[0].Quantity);
            Assert.AreEqual(1, order.orderDetails[0].DetailNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddDetail_NullDetail_ThrowsException() {
            Order order = new Order("Billy Smith", "6780923750");
            order.AddOrderDetail(null, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddDetail_InvalidQuantity_ThrowsException() {
            Order order = new("Billy Smith", "6780923750");
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);

            order.AddOrderDetail(item, 0);
        }

        [TestMethod]
        public void ProcessOrder_OrderDetailsAdded_ProcessedSuccesfully() {
            Order order = new("Billy Smith", "6780923750");
            OrderDetail item1 = new("ELECT001", "42 Inch TV", 300.00);
            OrderDetail item2 = new("GARD003", "Lawn Mower", 500.00);

            order.AddOrderDetail(item1, 1);
            order.AddOrderDetail(item2, 1);

            order.ProcessOrder();

            Assert.AreEqual(815.00, order.amountBeforeTax);
            Assert.AreEqual(81.50, order.taxAmount);
            Assert.AreEqual(896.50, order.totalAmount);
        }
        
        [TestMethod]
        [ExpectedException (typeof(InvalidOperationException))]
        public void ProcessOrder_NoOrderDetails_ThrowsException() {
            Order order = new("John Jenkins", "2533124578");
            order.ProcessOrder();
        }

        
        [TestMethod]
        public void ToString_ReturnsFormattedString() {
            Order order = new("John Jenkins", "2533124578");
            OrderDetail item = new OrderDetail("ELECT001", "42 Inch TV", 300.00);

            order.AddOrderDetail(item, 2);

            string formatted = order.ToString();
        }
    }
}