/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  2.0
 * Filename: MYSQL.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

using System.Data.SQLite;

namespace ClassLibrary
{
    // Implements OutputData interface to write order data to a MySQL database
    public class MYSQL : OutputData
    {
        // Preconditions:
        // - order must not be null
        // - There must be a connection with MySQL database
        // Postconditions:
        // - Order data is written to the MySQL database.
        public void Write(Order order) {
            if (order == null) {
                throw new ArgumentNullException("Order must not be null");
            }
        }

        private SQLiteConnection CreateConnection() { 
            SQLiteConnection connection = new SQLiteConnection("Data Source=orders.db;Version=3;");
            connection.Open();
            return connection;
        }
        
        // Postconditions:
        // - Creates Order and OrderDetail tables if they do not exist       
        private void CreateTables(SQLiteConnection connection) {
            SQLiteCommand sqlite_cmd;

            string createOrderTable = @"CREATE TABLE IF NOT EXISTS Orders (
                                        orderNumber INTEGER PRIMARY KEY,
                                        dateTime TEXT NOT NULL,
                                        customerName TEXT NOT NULL,
                                        customerPhone TEXT NOT NULL,
                                        taxAmount REAL NOT NULL,
                                        totalAmount REAL NOT NULL);";

            string createOrderDetailsTable = @"CREATE TABLE IF NOT EXISTS OrderDetails (
                                        orderNumber INTEGER NOT NULL,
                                        detailNumber INTEGER NOT NULL,
                                        stockID TEXT NOT NULL,
                                        stockName TEXT NOT NULL,
                                        stockPrice REAL NOT NULL,
                                        quantity INTEGER NOT NULL,
                                        PRIMARY KEY (orderNumber, detailNumber),
                                        FOREIGN KEY (orderNumber) REFERENCES Orders(orderNumber));";
            
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = createOrderTable;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = createOrderDetailsTable;
            sqlite_cmd.ExecuteNonQuery();
        }

        private void InsertOrder(SQLiteConnection connection, Order order) {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Orders (orderNumber, dateTime, customerName, customerPhone, taxAmount, totalAmount)
                                    VALUES (@orderNumber, @dateTime, @customerName, @customerPhone, @taxAmount, @totalAmount)";
            command.Parameters.AddWithValue("@orderNumber", order.orderNumber);
            command.Parameters.AddWithValue("@dateTime", order.dateTime);
            command.Parameters.AddWithValue("@customerName", order.customerName);
            command.Parameters.AddWithValue("@customerPhone", order.customerPhone);
            command.Parameters.AddWithValue("@taxAmount", order.taxAmount);
            command.Parameters.AddWithValue("@totalAmount", order.totalAmount);
            command.ExecuteNonQuery();
        }
    }
}