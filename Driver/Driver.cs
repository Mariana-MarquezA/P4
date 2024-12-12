/* Author:   Mariana Marquez
 * Date:     12/11/2024
 * Version:  3.2
 * Filename: Driver.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

using ClassLibrary;
using System.Text.Json;

namespace Driver
{
    public class Driver
    {
        public static void Main(){
            string connectionString = "Data Source=orders.db;Version=3;";
            
            OutputDataFactory outputFactory = new(connectionString);

            Console.WriteLine("***** Hardware Store Order System *****\n");

            Console.WriteLine("Clerk captures information about items that can be added to an order\n");
            OrderDetail item1 = new("ELECT001", "42 Inch TV", 300.00);
            OrderDetail item2 = new("GARD003", "Lawn Mower", 500.00);
            OrderDetail item3 = new("FURN001", "Office Chair", 100.00);
            OrderDetail item4 = new("ELECT002", "Bluetooth Speaker", 45.00);
            OrderDetail item5 = new("TOOL010", "Hammer", 20.00);
            OrderDetail item6 = new("PLUMB005", "Wrench Set", 35.00);
            OrderDetail item7 = new("ELECT044", "Battery", 50.00);

            Order order1 = new Order("Billy Smith", "6780923750", outputFactory);
            Console.WriteLine("Created " + order1);

            Console.WriteLine("Items are being added to the order:");
            order1.AddOrderDetail(item1, 1);
            order1.AddOrderDetail(item2, 2);
            Console.WriteLine("\tAdded 1x 42 Inch TV and 2x Office Chairs\n");

            order1.ProcessOrder();
            Console.WriteLine("Processed Order:\n" + order1 + "\n");
            Console.WriteLine("Tax amount and total amount of order have been updated\n");

            
            Order order2 = new Order("Sara Wilkinson", "5678761426", outputFactory);
            Console.WriteLine("Created " + order2);

            Console.WriteLine("Items are being added to the order:");
            order2.AddOrderDetail(item6, 2);
            order2.AddOrderDetail(item7, 3);
            Console.WriteLine("\tAdded 2x Wrench Set and 3x Battery\n");

            
            order2.ProcessOrder();
            Console.WriteLine("Processed Order:\n" + order2 + "\n");
            Console.WriteLine("Tax amount and total amount of order have been updated\n");

            Console.WriteLine("\nBilly Smith wants to reorder everything in his past order\n" +
                              "\t**Deep copy demonstration**\n");

            Order copyOfOrder1 = new Order(order1, outputFactory);
            Console.WriteLine("Copy created:\n" + copyOfOrder1 + "\n");

            Console.WriteLine("\nBilly Smith wants to add items to his new order\n");
            copyOfOrder1.AddOrderDetail(item5, 3);            
            Console.WriteLine("Items were added to the copied order:");
            Console.WriteLine("\tAdded 1x Hammer\n");
            copyOfOrder1.ProcessOrder();
            Console.WriteLine("Processed Order:\n" + copyOfOrder1 + "\n");
            Console.WriteLine("Tax amount and total amount have been updated\n");

            Console.WriteLine("Original Order:\n" + order1 + "\n");
            Console.WriteLine("Copied Order:\n" + copyOfOrder1 + "\n");

            Console.WriteLine("\n***Retrieving and displaying all stored orders\n");

            OutputData storageOutput = outputFactory.CreateOutputData();
            if (storageOutput is SQLite) {
                Console.WriteLine("Orders are stored in SQLite database\n");     
            }
            else if (storageOutput is JSON jsonStorage){ 
                Console.WriteLine("Orders are stored in JSON file.\n");
            }
            else {
                Console.WriteLine("Error");
            }

            Console.WriteLine("End of program");
        }

    }

} 
    

