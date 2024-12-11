using ClassLibrary;
using System.Text.Json;

namespace UnitTests
{
    [TestClass]
    public class JSONTests
    {
        private const string TestDirectory = "TestOutput";
        private const string TestFilePath = "TestOutput/test_orders.json";

        [TestInitialize]
        public void InitializeTest() {
            if (!Directory.Exists(TestDirectory)) {
                Directory.CreateDirectory(TestDirectory);
            }

            if (File.Exists(TestFilePath)) {
                File.Delete(TestFilePath);
            }
        }

        [TestCleanup]
        public void TestCleanup() {
            
            if (File.Exists(TestFilePath)) {
                File.Delete(TestFilePath);
            }

            if (Directory.Exists(TestDirectory)) {
                Directory.Delete(TestDirectory, true);
            }
        }

        [TestMethod]
        public void Constructor_ValidFilePath_ValidInstantiation() {
            JSON jsonObject = new(TestFilePath);
            Assert.IsNotNull(jsonObject);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullFilePath_ThrowsException() {
            JSON jsonObject = new(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyFilePath_ThrowsException() {
            JSON jsonObject = new("");
        }

        [TestMethod]
        public void Write_ValidOrder_WritesToFile() {
            JSON jsonObject = new(TestFilePath);

            Order order = new ("Billy Smith", "6780923750");
            OrderDetail item = new OrderDetail("ELECT001", "42 Inch TV", 300.00);
            order.AddOrderDetail(item, 1);

            jsonObject.Write(order);

            Assert.IsTrue(File.Exists(TestFilePath));

        
            string fileContent = File.ReadAllText(TestFilePath);
            List<Order>? deserializedOrders = JsonSerializer.Deserialize<List<Order>>(fileContent);
            Assert.IsNotNull(deserializedOrders);
            Assert.AreEqual(1, deserializedOrders.Count);
            Assert.AreEqual("Billy Smith", deserializedOrders[0].customerName);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Write_NullOrder_ThrowsException() {
            JSON jsonObject = new(TestFilePath);
            jsonObject.Write(null);
        }

        [TestMethod]
        public void Write_MultipleOrders_AppendsToFile() {
            JSON jsonObject = new(TestFilePath);
            OrderDetail item1 = new("ELECT001", "42 Inch TV", 300.00);
            OrderDetail item2 = new("FURN001", "Sofa", 500.00);

            Order order1 = new("Billy Smith", "6780923750");
            Order order2 = new("Sara Wilkinson", "5678761426");

            order1.AddOrderDetail(item1, 1);
            order1.AddOrderDetail(item2, 1);
            jsonObject.Write(order1);

            order2.AddOrderDetail(item2, 1);
            jsonObject.Write(order2);

            string fileContent = File.ReadAllText(TestFilePath);
            List<Order>? deserializedOrders = JsonSerializer.Deserialize<List<Order>>(fileContent);
            Assert.AreEqual(2, deserializedOrders.Count);

            Assert.AreEqual("Billy Smith", deserializedOrders[0].customerName);
            Assert.AreEqual("Sara Wilkinson", deserializedOrders[1].customerName);
        }
    
        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void Write_InvalidFilePath_ThrowsIOException() {
            // Create invalid file path
            string invalidFilePath = "X:\\Invalid\\Path\\test_orders.json"; // Invalid path
            JSON jsonObject = new(invalidFilePath);

            Order order = new("Billy Smith", "6780923750");
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            order.AddOrderDetail(item, 1);

            jsonObject.Write(order);
        }

        [TestMethod]
        public void Write_EmptyOrderDetails_WritesToFile() {
            JSON jsonObject = new(TestFilePath);
            Order order = new Order("Billy Smith", "6780923750");
            jsonObject.Write(order);

            Assert.IsTrue(File.Exists(TestFilePath));

            string fileContent = File.ReadAllText(TestFilePath);
            List<Order>? deserializedOrders = JsonSerializer.Deserialize<List<Order>>(fileContent);
            Assert.IsNotNull(deserializedOrders);
            Assert.AreEqual(1, deserializedOrders.Count);
            Assert.AreEqual(0, deserializedOrders[0].orderDetails.Count);
        }

        [TestMethod]
        public void Write_InvalidExistingJson_OverwritesAndAppendsNewOrder() {
            JSON jsonObject = new(TestFilePath);

            // Create an invalid JSON file
            File.WriteAllText(TestFilePath, "{ }"); 

            Order order = new("Billy Smith", "6780923750");
            OrderDetail item = new("GARD003", "Lawn Mower", 500.00);
            order.AddOrderDetail(item, 2);

            jsonObject.Write(order);
            Assert.IsTrue(File.Exists(TestFilePath));

            string fileContent = File.ReadAllText(TestFilePath);
            List<Order>? deserializedOrders = JsonSerializer.Deserialize<List<Order>>(fileContent);
            Assert.IsNotNull(deserializedOrders);
            Assert.AreEqual(1, deserializedOrders.Count);
            Assert.AreEqual("Billy Smith", deserializedOrders[0].customerName);
            Assert.AreEqual(1, deserializedOrders[0].orderDetails.Count);
            Assert.AreEqual("GARD003", deserializedOrders[0].orderDetails[0].stockID);
            Assert.AreEqual("Lawn Mower", deserializedOrders[0].orderDetails[0].stockName);
            Assert.AreEqual(500.00, deserializedOrders[0].orderDetails[0].stockPrice);
            Assert.AreEqual(2, deserializedOrders[0].orderDetails[0].Quantity);
        }
    }
}
