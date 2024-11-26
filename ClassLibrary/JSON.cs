/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  1.0
 * Filename: JSON.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

namespace ClassLibrary
{
    // Implements OutputData interface to write order data to a JSON file.
    public class JSON : OutputData
    {
        // Preconditions:
        // - order must not be null or empty
        // Postconditions:
        // - Order data is saved to the JSON file.
        public void Write(Order order) { }
        
    }
}
