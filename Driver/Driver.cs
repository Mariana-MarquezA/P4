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



            string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OrderSystem");
            string JSONFilePath = Path.Combine(dataDirectory, "orders.json");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            JSON jsonObject = new(JSONFilePath);

            Order order = new Order("Billy Smith", "6780923750");
            OrderDetail detail = new OrderDetail("ELECT001", "42 Inch TV", 300.00);
            order.AddOrderDetail(detail, 1);

            jsonObject.Write(order);

            Console.WriteLine("Order has been saved to: " + JSONFilePath);
        } 
    }
}
