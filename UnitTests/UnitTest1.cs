using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibrary;

namespace UnitTests
{
    [TestClass]
    public class TestOrderClassLibrary
    {
        [TestMethod]
        public void OrderDetail_Constructor_ValidCreation()
        {
            OrderDetail detail = new OrderDetail("ELECT001", "42 Inch TV", 300.00);
        }
    }
}
