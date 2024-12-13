/* Author:   Mariana Marquez
 * Date:     11/25/2024
 * Version:  1.0
 * Filename: OutputData.cs
 * Platform: Windows Visual Studio 2022
 * .NET Version: NET 8.0
 */

using System;

namespace ClassLibrary
{
    // Interface for output operations
    public interface OutputData {
        // Writes order data to an output medium
        void Write(Order order);
    }
}
