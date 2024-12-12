using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class OrderTests
    {
        private const string TestDirectory = "TestOutput";
        private const string TestFilePath = "TestOutput/test_orders.json";

        private OutputDataFactory _outputFactory;

        [TestInitialize]
        public void Setup()
        {            
            if (!Directory.Exists(TestDirectory)) {
                Directory.CreateDirectory(TestDirectory);
            }

            string testConnectionString = "Data Source=test_orders.db;Version=3;";
            _outputFactory = new OutputDataFactory(testConnectionString);

            if (File.Exists("test_orders.db")) {
                File.Delete("test_orders.db");
            }

            
            if (File.Exists(TestFilePath)) {
                File.Delete(TestFilePath);
            }
        }

        [TestCleanup]
        public void Cleanup() {    
            if (File.Exists("test_orders.db")) {
                File.Delete("test_orders.db");
            }
                        
            if (File.Exists(TestFilePath)) {
                File.Delete(TestFilePath);
            }
                        
            if (Directory.Exists(TestDirectory) && 
                Directory.GetFiles(TestDirectory).Length == 0) {
                Directory.Delete(TestDirectory);
            }
        }



        [TestMethod]
        public void Constructor_ValidArguments_ValidInstantiation() {
            Order order = new("Billy Smith", "6780923750", _outputFactory);
            Assert.AreEqual("Billy Smith", order.customerName);
            Assert.AreEqual("6780923750", order.customerPhone);
            Assert.AreEqual(0.0, order.taxAmount);
            Assert.AreEqual(0.0, order.totalAmount);
            Assert.AreEqual(0, order.orderDetails.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyCustomerName_ThrowsException() {
            Order order = new("", "6780923750", _outputFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullCustomerName_ThrowsException() {
            Order order = new(null, "6780923750", _outputFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyCustomerPhone_ThrowsException() {
            Order order = new("Billy Smith", "", _outputFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullCustomerPhone_ThrowsException() {
            Order order = new(null, "6780923750", _outputFactory);
        }

        [TestMethod]
        public void DeepCopy_CreateCopy() {
            Order original = new Order("Billy Smith", "6780923750", _outputFactory);
            OrderDetail item1 = new OrderDetail("ELECT001", "42 Inch TV", 300.00);
            original.AddOrderDetail(item1, 1);
            original.ProcessOrder();

            Order copy = new(original, _outputFactory);

            Assert.AreEqual(original.customerName, copy.customerName);
            Assert.AreEqual(original.customerPhone, copy.customerPhone);
            Assert.AreEqual(original.taxAmount, copy.taxAmount);
            Assert.AreEqual(original.totalAmount, copy.totalAmount);
            Assert.AreEqual(original.orderDetails.Count, copy.orderDetails.Count);

            for (int i = 0; i < original.orderDetails.Count; i++)
            {
                Assert.AreEqual(original.orderDetails[i].stockID, copy.orderDetails[i].stockID);
                Assert.AreEqual(original.orderDetails[i].stockName, copy.orderDetails[i].stockName);
                Assert.AreEqual(original.orderDetails[i].stockPrice, copy.orderDetails[i].stockPrice);
                Assert.AreEqual(original.orderDetails[i].quantity, copy.orderDetails[i].quantity);
            }
        }

        [TestMethod]
        public void DeepCopy_CreateIndependentObject() {
            Order original = new("Billy Smith", "6780923750", _outputFactory);
            OrderDetail item1 = new("ELECT001", "42 Inch TV", 300.00);
            OrderDetail item2 = new ("ELECT044", "Battery", 50.00);
            
            original.AddOrderDetail(item1, 2);
            original.AddOrderDetail(item2, 5);
            original.ProcessOrder();

            Order copy = new Order(original, _outputFactory);

            copy.orderDetails[0].stockName = "50 Inch TV";
            copy.orderDetails[1].quantity = 10;

            Assert.AreEqual("42 Inch TV", original.orderDetails[0].stockName);
            Assert.AreEqual(5, original.orderDetails[1].quantity);

            Assert.AreNotSame(original.orderDetails, copy.orderDetails);
            Assert.AreNotSame(original.orderDetails[0], copy.orderDetails[0]);
            Assert.AreNotSame(original.orderDetails[1], copy.orderDetails[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeepCopy_NullOrder_ThrowsException() {
            Order order = null;
            Order copy = new(order, _outputFactory);
        }

        [TestMethod]
        public void AddDetail_ValidDetail_Added() {
            Order order = new("Billy Smith", "6780923750", _outputFactory);
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            order.AddOrderDetail(item, 2);

            Assert.AreEqual(1, order.orderDetails.Count);
            Assert.AreEqual(2, order.orderDetails[0].Quantity);
            Assert.AreEqual(1, order.orderDetails[0].DetailNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddDetail_NullDetail_ThrowsException() {
            Order order = new Order("Billy Smith", "6780923750", _outputFactory);
            order.AddOrderDetail(null, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddDetail_InvalidQuantity_ThrowsException() {
            Order order = new("Billy Smith", "6780923750", _outputFactory);
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);

            order.AddOrderDetail(item, 0);
        }

        [TestMethod]
        public void ProcessOrder_OrderDetailsAdded_ProcessedSuccesfully() {
            Order order = new("Billy Smith", "6780923750", _outputFactory);
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
            Order order = new("John Jenkins", "2533124578", _outputFactory);
            order.ProcessOrder();
        }

        
        [TestMethod]
        public void ToString_ReturnsFormattedString() {
            Order order = new("John Jenkins", "2533124578", _outputFactory);
            OrderDetail item = new OrderDetail("ELECT001", "42 Inch TV", 300.00);

            order.AddOrderDetail(item, 2);

            string formatted = order.ToString();

            string expectedOrderHeader = $"Order {{{order.orderNumber}, {order.dateTime}, {order.customerName}, {order.customerPhone}, {order.taxAmount}, {order.totalAmount}}}";
            string expectedOrderDetail = $"OrderDetail {{ {order.orderNumber}, {order.orderDetails[0].DetailNumber}, {item.stockID}, {item.stockName}, {item.stockPrice}, {order.orderDetails[0].quantity}}}";

            string expectedFormatted = $"{expectedOrderHeader}\n{expectedOrderDetail}";

            Assert.AreEqual(expectedFormatted, formatted);
        }
    }
}