/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  2.0
 * Filename: Order.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

using System;


namespace ClassLibrary
{
    // Represents a customer order containing multiple order details.
   
    public class Order

    // Class invariants:
    // - OrderNumber must be unique and positive.
    // - dateTime must be valid
    // - customerNmae must not be empty or null
    // - customerPhone must not be empty or null
    // - customerPhone must have a valid phone format (10 digits)
    // - taxAmount must be non-negative
    // - Tariffs are first applied to the applicable items. Then total amount
    //   is calculated and tax is apllied. 
    // - TotalAmount must be non-negative
    // - orderDetails must contain at least one OrderDetail object

    {
        private readonly double _taxFactor = 0.10;
        internal int orderNumber;
        internal DateTime dateTime;
        internal string customerName;
        internal string customerPhone;
        internal double taxAmount;
        internal double totalAmount;
        internal double amountBeforeTax;
        internal List<OrderDetail> orderDetails;
       

        // Preconditions:
        // - customerName must not be empty or null
        // - customerPhone must not be empty or null
        public Order(string customerName, string customerPhone) {
            if (string.IsNullOrEmpty(customerName))
            {
                throw new ArgumentException("Customer Name must not be empty or null");
            }
            if (string.IsNullOrEmpty(customerPhone))
            {
                throw new ArgumentException("Customer Phone must not be empty or null");
            }

            this.customerName = customerName;
            this.customerPhone = customerPhone;
            dateTime = DateTime.Now;
            orderNumber = GenerateUniqueOrderNumber();
            orderDetails = [];
            taxAmount = 0.0;
            totalAmount = 0.0;
            amountBeforeTax = 0.0;
        }

        // Copy constructor
        // Preconditions:
        // - 'other' must not be null
        public Order(Order other) {
            if (other == null) {
                throw new ArgumentNullException("Cannot copy a null order");
            }
            this.orderNumber = other.orderNumber;
            this.dateTime = other.dateTime;
            this.customerName = other.customerName;
            this.customerPhone = other.customerPhone;
            this.taxAmount = other.taxAmount;
            this.totalAmount = other.totalAmount;
            this.amountBeforeTax = other.amountBeforeTax;
            orderDetails = new List<OrderDetail>(other.orderDetails);
        }

        // Adds a new order detail to the order with the specified quantity.
        // Preconditions:
        // - orderDetail must not be null
        // - quantity must be a positive integer
        // Postconditions:
        // - A new OrderDetail is added to orderDetails with the specified quantity,
        //   orderNumber, and detailNumber set
        public void AddOrderDetail(OrderDetail orderDetail, int quantity)
        {
            if (orderDetail == null)
            {
                throw new ArgumentNullException("OrderDetail must not be null");
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException("Quantity must be greater than zero");
            }

            OrderDetail newDetail = new (orderDetail)
            {
                Quantity = quantity,
                OrderNumber = this.orderNumber,
                DetailNumber = orderDetails.Count + 1,
            };

            orderDetails.Add(newDetail);
        }

        // Processes the order, applies taxes and calculates total amount
        // Preconditions:
        // - Order must have at least one OrderDetail
        // Postconditions:
        // - Order is stored in the Database or JSON file based on database availability
        public void ProcessOrder() {

            if (orderDetails == null || orderDetails.Count == 0) 
            {
                throw new InvalidOperationException("Order must have one order detail at least");
            }
            
            CalculateTotalAmount();

            ApplyTax();

            SaveOrder();
        }

        public override string ToString()
        {
            string details = string.Join("\n", orderDetails);
            return $"Order {{{orderNumber}, {dateTime}, {customerName}, {customerPhone}, {taxAmount}, {totalAmount}}}\n{details}";
        }

        // Calculates the total amount of the order, sums up all order detail amounts.
        // Postconditions:
        // - amountBeforeTax is updated with the total amount of the order
        private void CalculateTotalAmount()
        {
            double sum = 0.0;

            foreach (OrderDetail orderDetail in orderDetails)
            {
                sum += orderDetail.CalculateAmountWithTariffs();
            }

            amountBeforeTax = sum;
        }

        // Preconditions:
        // - amountBeforeTax must be greater than 0   
        // Postconditions:
        // - taxAmount is set to 10% of amountBeforeTax
        // - totalAmount is set to the sum of amountBeforeTax and taxAmount
        private void ApplyTax() 
        {
            if (amountBeforeTax < 0) 
            {
                throw new InvalidOperationException("Total amount must be greater than zero");
            }

            taxAmount = amountBeforeTax * _taxFactor;
            totalAmount = amountBeforeTax + taxAmount;
        }

        // Postconditions
        // - Order is stored in database or JSON file depending on database availability 
        private void SaveOrder() 
        {
            string connectString = "Data Source=orders.db;Version=3;";
            OutputDataFactory outputFactory = new (connectString);
            OutputData output = outputFactory.CreateOutputData();
            output.Write(this);
        }

        private int GenerateUniqueOrderNumber() 
        {
            return Math.Abs(DateTime.Now.Ticks.GetHashCode());
        }

    }
}

