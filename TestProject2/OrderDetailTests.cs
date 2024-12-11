using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests
{
    [TestClass]
    public class OrderDetailTests
    {
        [TestMethod]
        public void Constructor_ValidArguments_ValidInstantiation() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            Assert.AreEqual("ELECT001", item.stockID);
            Assert.AreEqual("42 Inch TV", item.stockName);
            Assert.AreEqual(300.00, item.stockPrice);
            Assert.AreEqual(-1, item.OrderNumber);
            Assert.AreEqual(-1, item.DetailNumber);
            Assert.AreEqual(-1, item.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyStockID_ThrowsException() {
            OrderDetail item = new("", "42 Inch TV", 300.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullStockID_ThrowsException() {
            OrderDetail item = new(null, "42 Inch TV", 300.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyStockName_ThrowsException() {
            OrderDetail item = new("ELECT001", "", 300.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullStockName_ThrowsException() {
            OrderDetail item = new("ELECT001", null, 300.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_NegativeStockPrice_ThrowsException() {
            OrderDetail item = new("ELECT001", "42 Inch TV", -300.00);
        }

        [TestMethod]
        public void DeepCopy_CopiesObject() {
            OrderDetail original = new("ELECT001", "42 Inch TV", 300.00);
            OrderDetail copy = new(original);

            Assert.AreEqual(original.stockID, copy.stockID);
            Assert.AreEqual(original.stockName, copy.stockName);
            Assert.AreEqual(original.stockPrice, copy.stockPrice);
            Assert.AreEqual(original.OrderNumber, copy.OrderNumber);
            Assert.AreEqual(original.DetailNumber, copy.DetailNumber);
            Assert.AreEqual(original.Quantity, copy.Quantity);
        }

        [TestMethod]
        public void OrderNumberProperty_SetsValue() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            item.OrderNumber = 1;
            Assert.AreEqual(1, item.OrderNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OrderNumberProperty_NegativeValue_ThrowsException() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            item.OrderNumber = -2;
        }

        [TestMethod]
        public void DetailNumberProperty_SetsValue() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            item.DetailNumber = 1;
            Assert.AreEqual(1, item.DetailNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DetailNumberProperty_NegativeValue_ThrowsException() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            item.DetailNumber = 0;
        }

        [TestMethod]
        public void QuantityProperty_SetsValue() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            item.Quantity = 1;
            Assert.AreEqual(1, item.Quantity);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentOutOfRangeException))]
        public void QuantityProperty_InvalidValue_ThrowsEsception() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00);
            item.Quantity = 0;
        }


        [TestMethod]
        public void CalculateAmountWithTariffs_ElectTarrifApplied() {
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
        public void CalculateAmountWithTariffs_NoElectTarrifApplied() {
            OrderDetail item = new("FURN001", "Office Chair", 250.00)
            {
                // Assume item has been added to an Order
                Quantity = 2,
                OrderNumber = 1,
                DetailNumber = 1
            };

            double result = item.CalculateAmountWithTariffs();
            Assert.AreEqual(500.00, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateAmountWithTariffs_NotAddedToOrder_ThrowsException() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00) {
                Quantity = 1
            };

            item.CalculateAmountWithTariffs();
        }

        [TestMethod]
        public void ToString_ReturnsFormattedString() {
            OrderDetail item = new("ELECT001", "42 Inch TV", 300.00) { 
                OrderNumber = 1,
                DetailNumber = 1,
                Quantity = 2,
            };

            string formattedString = item.ToString();
            Assert.AreEqual("OrderDetail { 1, 1, ELECT001, 42 Inch TV, 300, 2}", formattedString);
        }
    }
}
