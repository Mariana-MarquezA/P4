/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  2.0
 * Filename: JSON.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

// using System.Reflection;
using System.Text.Json;

namespace ClassLibrary
{
    // Implements OutputData interface to write order data to a JSON file.
    public class JSON : OutputData
    {
        private readonly string _filePath;

        public JSON(string filePath)
        {
            _filePath = filePath;
        }

        // Preconditions:
        // - order must not be null 
        // Postconditions:
        // - Order data is saved to the JSON file.
        public void Write(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("Order must not be null");
            }
            string jsonData = JsonSerializer.Serialize(order);
            File.AppendAllText(_filePath, jsonData + Environment.NewLine);
        }
    }
}
