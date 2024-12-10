/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  2.0
 * Filename: OutputDataFactory.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

using System.Data.SQLite;
using System.Linq.Expressions;

namespace ClassLibrary
{
    // Creates instances of OutputData implementations based on database status
    public class OutputDataFactory
    {
        private readonly string _connectString;
        public OutputDataFactory(string connectionString) => _connectString = connectionString;

        private bool IsDatabaseUp()
        {
            SQLiteConnection connection = new (_connectString);
            try 
            {
                connection.Open();
                connection.Close();

                return true;
            }
            catch (SQLiteException)
            {
                Console.WriteLine("Database is not available");
                return false;
            }
        }

        public OutputData CreateOutputData() 
        {
            if (IsDatabaseUp())
            {
                return new MYSQL(_connectString);
            }
            else {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "orders.json");
                return new JSON(filePath);
            }
        }

    }
}
