/* Author:   Mariana Marquez
 * Date:     12/11/2024
 * Version:  3.0
 * Filename: JSON.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

using System.Text.Json;

namespace ClassLibrary
{
    // Implements OutputData interface to write order data to a JSON file.
    public class JSON : OutputData
    /* Class invariants:
     * - filePath must be a valid and non-empty string referencing JSON file
     * - if JSON file exists, file must contain a valid or empty JSON array of Order objects
     */
    {
        private string _filePath;

        // Constructor
        // Preconditions:
        // - filePath must not be null or empty
        public JSON(string filePath) {
            if (string.IsNullOrEmpty(filePath)) {
                throw new ArgumentException("File path must not be null or empty");
            }
            _filePath = filePath;
        }

        // Postconditions:
        // - Appends order to the existing orders in the JSON file
        // - If file does not exist, new file is created
        // - If deserialization of existing orders fails, file is overwritten
        //   with empty JSON array and appends the new order
        public void Write(Order order)
        {
            if (order == null) { 
                throw new ArgumentNullException(nameof(order), "Order must not be null");
            }

            try {
                CheckDirectoryExists();
                List<Order> orders = ReadPrevOrders();
                orders.Add(order);
                string jsonData = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, jsonData);
            }
            catch (DirectoryNotFoundException) {
                throw new IOException("Invalid file path");
            }
            catch (JsonException) {
                throw new JsonException("Unable to deserialize");
            }
        }
            
        // Postconditions:
        // - If the directory does not exist, it is created
        private void CheckDirectoryExists() {
            string directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)){
                    Directory.CreateDirectory(directory);
            }
        }

        // Postconditions:
        // - If file exists, returns a list of orders
        // - If file is empty or does not exist, returns an empty list
        // - If JsonException is caught, file is overwritten with empty JSON array
        private List<Order> ReadPrevOrders() {
            List<Order> orders = new List<Order>();
            if (!File.Exists(_filePath)){
                return new List<Order>();
            }

            string existingOrders = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(existingOrders)) {
                return new List<Order>();
            }

            try {
                List<Order>? deserializedOrders = JsonSerializer.Deserialize<List<Order>>(existingOrders);

                return deserializedOrders != null ? deserializedOrders: new List<Order>();
                
            }
            catch (JsonException) {
                File.WriteAllText(_filePath, "[]");
                Console.WriteLine("File will be overwritten with empty JSON array");
                return new List<Order>();
            }
        }

    }
}
