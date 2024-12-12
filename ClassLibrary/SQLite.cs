/* Author:   Mariana Marquez
 * Date:     12/11/2024
 * Version:  2.0
 * Filename: MYSQL.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

using System.Data.SQLite;

namespace ClassLibrary
{
    // Implements OutputData interface to write order data to a MySQL database
    public class SQLite: OutputData
    {
        private readonly string _connectString;

        // Preconditions:
        // - connectString must not be null or empty
        public SQLite(string connectString) {
            if (string.IsNullOrEmpty(connectString)) {
                throw new ArgumentException("Connection string must not be null or empty");
            }

            _connectString = connectString;
        }

        public int GetMaxOrderNumber(SQLiteConnection connection)
        {
            string query = "SELECT MAX(orderNumber) FROM Orders;";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                if (result != DBNull.Value && result != null) {
                    return Convert.ToInt32(result);
                }
                else {
                    return 999;
                }
            }
        }


        // Preconditions:
        // - order must not be null
        // - There must be a connection with SQLite database
        // Postconditions:
        // - Order data is written to the SQLite database.
        public void Write(Order order) {
            if (order == null) {
                throw new ArgumentNullException("Order must not be null");
            }

            SQLiteConnection connection = CreateConnection();
            CreateTables(connection);
            InsertOrder(connection, order);
            InsertOrderDetails(connection, order);
            Console.WriteLine($"Inserted Order #{order.orderNumber} and its details into the database");
        }

        public SQLiteConnection CreateConnection() { 
            SQLiteConnection connection = new (_connectString);
            connection.Open();
            return connection;
        }
        
        // Postconditions:
        // - Creates Order and OrderDetail tables if they do not exist       
        static void CreateTables(SQLiteConnection connection) {
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

        static void InsertOrder(SQLiteConnection connection, Order order)
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Orders (orderNumber, dateTime, customerName, customerPhone, taxAmount, totalAmount)
                            VALUES (@orderNumber, @dateTime, @customerName, @customerPhone, @taxAmount, @totalAmount)";
            command.Parameters.AddWithValue("@orderNumber", order.orderNumber);
            command.Parameters.AddWithValue("@dateTime", order.dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@customerName", order.customerName);
            command.Parameters.AddWithValue("@customerPhone", order.customerPhone);
            command.Parameters.AddWithValue("@taxAmount", order.taxAmount);
            command.Parameters.AddWithValue("@totalAmount", order.totalAmount);
            command.ExecuteNonQuery();
        }


        private void InsertOrderDetails(SQLiteConnection connection, Order order)
        {
            string insertDetailQuery = @"INSERT INTO OrderDetails (orderNumber, detailNumber, stockID, stockName, stockPrice, quantity)
                                         VALUES (@orderNumber, @detailNumber, @stockID, @stockName, @stockPrice, @quantity);";

            SQLiteTransaction transaction = connection.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(insertDetailQuery, connection);    
                foreach (var detail in order.orderDetails)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@orderNumber", order.orderNumber);
                    command.Parameters.AddWithValue("@detailNumber", detail.detailNumber);
                    command.Parameters.AddWithValue("@stockID", detail.stockID);
                    command.Parameters.AddWithValue("@stockName", detail.stockName);
                    command.Parameters.AddWithValue("@stockPrice", detail.stockPrice);
                    command.Parameters.AddWithValue("@quantity", detail.quantity);

                    command.ExecuteNonQuery();
                }
            
            transaction.Commit();
            
        }
    }
}