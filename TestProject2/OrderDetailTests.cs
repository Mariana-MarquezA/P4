using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal class OrderDetailTests
    {
        [TestMethod]
        public void OrderDetail_ValidArguments_ValidInstantiation()
        {
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
        public void OrderDetail_InvalidStockID_ThrowsException()
        {
            OrderDetail item = new("", "42 Inch TV", 300.00);
        }
    }
}
