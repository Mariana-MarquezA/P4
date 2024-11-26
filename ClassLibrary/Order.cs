/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  1.0
 * Filename: Order.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */
using ClassLibrary;
using System;

namespace ClassLibrary
{
    // Represents a customer order containing multiple order details.
   
    public class Order

    // Invariants:
    // - OrderNumber must be unique and positive.
    // - dateTime must be valid
    // - customerNmae must be non-empty
    // - customerPhone must have a valid phone format (10 digits)
    // - taxAmount must be non-negative
    // - Tariffs are first applied to the applicable items. Then total amount
    //   is calculated and tax is apllied. 
    // - TotalAmount must be non-negative
    // - orderDetails must contain at least one OrderDetail object

    {
        private int orderNumber;
        private DateTime dateTimes;
        private string customerName;
        private string customerPhone;
        private double taxAmount;
        private double totalAmount;
        private List<OrderDetail> orderDetails;

        // Preconditions:
        // - customerName must be a non-empty
        // - customerPhone must be a non-empty 
        public Order(string customerName, string customerPhone) { }

        // Copy constructor
        // Preconditions:
        // - 'other' must not be null
        public Order(Order other) { }

        // Adds a new order detail to the order with the specified quantity.
        // Preconditions:
        // - orderDetail must not be null
        // - quantity must be a positive integer
        // Postconditions:
        // - A new OrderDetail is added to orderDetails with the specified quantity,
        //   orderNumber, and detailNumber set
        public void AddOrderDetail(OrderDetail orderDetail, int quantity) { }

        // Processes the order, applies taxes and calculates total amount
        // Preconditions:
        // - Order must have at least one OrderDetail
        // Postconditions:
        // - Order is stored in the Database or JSON file based on database availability
        public void ProcessOrder() { }

        // Calculates the total amount of the order, sums up all order detail amounts.
        // Postconditions:
        // - totalAmount is updated with the total amount of the order
        private void CalculateTotalAmount() { }

        // Preconditions:
        // - totalAmount must be greater than 0   
        // Postconditions:
        // - taxAmount is set to 10% of totalAmount
        // - totalAmount is increased by taxAmount
        private void ApplyTax() { }

        private int GenerateUniqueOrderNumber() { return 0; }

    }
}

