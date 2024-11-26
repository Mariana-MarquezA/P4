/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  1.0
 * Filename: OrderDetails.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */
using ClassLibrary;
using System;

namespace ClassLibrary
{
 
// Represents an item or order line in an order, including attributes and information 
// Allowins initialization without orderNumber, detailNumber, or quantity.
// Once these properties are set after  to anbeing added to an order,
// the CalculateAmountWithTariffs method can be used.

public class OrderDetail
    {
        /* Class invariants:
        * - orderNumber must be a positive integer, unique to each order.
        * - detailNumber must be a positive integer, starting from 1 and incrementing by 1 for each detail.
        * - stockID must be valid and non-empty 
        * - stockName must be valid and non-empty
        * - stockPrice must be non-negative
        * - quantity must be positive, greater than 0
        */

        private int orderNumber;
        private int detailNumber;
        private string stockID;
        private string stockName;
        private double stockPrice;
        private int quantity;

        // Preconditions:
        // - stockID must be non-empty 
        // - stockName must be non-empty 
        // - stockPrice must be non-negative.
        // Postconditions:
        // - orderNumber, detailNumber and quantity are initialized to default values
        public OrderDetail(string stockID, string stockName, double stockPrice){}

        // Copy constructor
        // Preconditions:
        // - 'other' orderDetail object must not be null.

        public void SetOrderNumber(int orderNumber){}

        public void SetDetailNumber(int detailNumber){}

        public void SetQuantity(int quantity) {}

        public OrderDetail(OrderDetail other){}

        // Preconditions:
        // - quantity has been set to a different value than 0
        // - quantity must be positive
        // - orderNumber and detailNumber must have been set, indicating the object was added to an order
        // Postconditions:
        // - Returns the calculated amount based on stockPrice and quantity, with tariff applied if applicable.
        public double CalculateAmountWithTariffs(){ return 0.0; }        
    }
}

