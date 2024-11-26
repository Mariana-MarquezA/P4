/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  1.0
 * Filename: MYSQL.cs
 * Platform: Windows Vista 2022
 * .NET Version: NET 8.0
 */

namespace ClassLibrary
{
    // Implements OutputData interface to write order data to a MySQL database
    public class MYSQL : OutputData
    {
        // Preconditions:
        // - order must not be null or empty
        // - There must be a connection with MySQL database
        // Postconditions:
        // - Order data is written to the MySQL database.
        public void Write(Order order) { }
    }
}
