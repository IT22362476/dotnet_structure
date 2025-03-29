using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Helper
{
    public static class RackCodeGenerator
    {
        // Function to convert a rack code (e.g., "AA", "AZ", "BA") to an index (e.g., 0, 25, 26)
        public static int ConvertRackCodeToIndex(string rackCode)
        {
            int index = 0;
            int length = rackCode.Length;

            // Adjust calculation to start from "AA" as index 0
            for (int i = 0; i < length; i++)
            {
                index = index * 26 + (rackCode[i] - 'A' + 1);
            }

            // Subtract 1 to adjust for "AA" starting at index 0
            return index - 1;
        }

        // Function to convert an index (e.g., 0, 1, 26) back to a rack code (e.g., "AA", "AB", "BA")
        public static string ConvertIndexToRackCode(int index)
        {
            index += 1; // Adjust to make "AA" correspond to index 0
            StringBuilder rackCode = new StringBuilder();

            // Reverse base-26 conversion
            while (index > 0)
            {
                index--; // Adjust for 0-based index
                rackCode.Insert(0, (char)('A' + (index % 26)));
                index /= 26;
            }

            return rackCode.ToString();
        }

        // Function to get the next available rack code
        public static string GetNextRackCode(string lastUsedRackCode)
        {
            int currentIndex = ConvertRackCodeToIndex(lastUsedRackCode);
            int nextIndex = currentIndex + 1; // Increment the index to get the next code
            return ConvertIndexToRackCode(nextIndex);
        }
    }


}
