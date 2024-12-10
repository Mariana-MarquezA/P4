/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  2.0
 * Filename: OrderDetails.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

using System;
using System.Configuration;

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

        private readonly double _electronicsTariff = 0.05;
        internal string stockID;
        internal string stockName;
        internal double stockPrice;
        internal int orderNumber;
        internal int detailNumber;
        internal int quantity;

        // OrderDetail constructor
        // Preconditions:
        // - stockID must be non-empty 
        // - stockName must be non-empty 
        // - stockPrice must be non-negative.
        // Postconditions:
        // - orderNumber, detailNumber and quantity are initialized to default values
        public OrderDetail(string stockID, string stockName, double stockPrice)
        {
            if (string.IsNullOrEmpty(stockID))
                throw new ArgumentException("Stock ID must not be empty.");
            if (string.IsNullOrEmpty(stockName))
                throw new ArgumentException("Stock Name must not be empty.");
            if (stockPrice < 0)
                throw new ArgumentOutOfRangeException("Stock Price must be non-negative");

            this.stockID = stockID;
            this.stockName = stockName;
            this.stockPrice = stockPrice;
            orderNumber = -1;
            detailNumber = -1;
            quantity = -1;
        }

        // Copy constructor
        // Preconditions:
        // - 'other' orderDetail object must not be null.
        public OrderDetail(OrderDetail other)
        {
            orderNumber = other.orderNumber;
            detailNumber = other.detailNumber;
            stockID = other.stockID;
            stockName = other.stockName;
            stockPrice = other.stockPrice;
            quantity = other.quantity;
        }

        // Preconditions:
        // orderNumber must be positive
        internal int OrderNumber
        {
            get 
            { 
                return orderNumber; 
            }
            
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Order number must be positive");
                }

                orderNumber = value;
            }
            
        }

        // Preconditions:
        // - detailNumber must be greater or equal to one 
        internal int DetailNumber 
        {
            get 
            {
                return detailNumber;
            }
            set 
            {
                if (value < 1) 
                {
                    throw new ArgumentOutOfRangeException("detailNumber must be 1 or greater");
                }

                detailNumber = value;
            }        
        }

        // Preconditions:
        // - quantity must be greater than zero
        internal int Quantity
        {
            get
            {
                return quantity;
            }
            
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Quantity must be greater than zero");
                }

                quantity = value;
            }
        }
       
        // Preconditions:
        // - quantity has been set to a different value than 0
        // - quantity must be positive
        // - orderNumber and detailNumber must have been set, indicating the object was added to an order
        // Postconditions:
        // - Returns the calculated amount based on stockPrice and quantity, with tariff applied if applicable.
        internal double CalculateAmountWithTariffs()
        {
            if(orderNumber == -1 || detailNumber == -1)
            {
                throw new ArgumentException("Order detail must be added to an order");
            }
            
            double finalPrice = stockPrice;
            if (!string.IsNullOrEmpty(stockID) && stockID.Length >= 5 &&
                stockID.Substring(0, 5).Equals("ELECT", StringComparison.OrdinalIgnoreCase))
            {
                finalPrice *= (1 + _electronicsTariff);
            }
            return finalPrice * quantity;
        }

        public override string ToString() {
            return $"OrderDetail {{ {orderNumber}, {detailNumber}, {stockID}, {stockName}, {stockPrice}, {quantity}}}";
        }
    }
}

