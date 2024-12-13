/* Author:   Mariana Marquez
 * Date:     12/11/2024
 * Version:  3.2
 * Filename: OutputDataFactory.cs
 * Platform: Windows Visual Studio 2022
 * .NET Version: NET 8.0
 */

using System.Data.SQLite;

namespace ClassLibrary
{
    // Creates instances of OutputData implementations based on database status
    public class OutputDataFactory {
        private readonly string _connectString;

        // Preconditions:
        // - ConnectionString must not be empty nor null
        public OutputDataFactory(string connectionString)  {
            if (string.IsNullOrEmpty(connectionString)) {
                throw new ArgumentException("Connection string must not be null or empty", nameof(connectionString));
            }

            _connectString = connectionString;
        }

        public OutputData CreateOutputData() {
            if (IsDatabaseUp()) {
                return new SQLite(_connectString);
            }
            else {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "orders.json");
                return new JSON(filePath);
            }
        }

        private bool IsDatabaseUp() {
            SQLiteConnection connection = new(_connectString);
            try {
                connection.Open();
                connection.Close();
                Console.WriteLine("Connected to SQLite database successfully");
                return true;
            }
            catch (SQLiteException) {
                Console.WriteLine("Database is not available");
                return false;
            }
        }
    }
}
