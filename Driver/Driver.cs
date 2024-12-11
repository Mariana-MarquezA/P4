/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  1.0
 * Filename: Driver.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */
using System;
using ClassLibrary;


namespace Driver
{
    public class Driver
    {
        public static void Main(){
            string connectionString = "Data Source=orders.db;Version=3;";

            OutputDataFactory outputFactory = new OutputDataFactory(connectionString);

            

            Order order = new Order("Billy Smith", "6780923750", outputFactory);
            OrderDetail detail = new OrderDetail("ELECT001", "42 Inch TV", 300.00);
            order.AddOrderDetail(detail, 1);

            order.ProcessOrder();
            
        } 
    }
}
