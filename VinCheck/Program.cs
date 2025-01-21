using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinCheck
{
    internal class Program
    {
        static void Main()
        {
            string vin = "WAUZZZ8V8DA084485";
            string vin1 = "A1BS31Z0430336179";
            string vin3 = "TMBBH25J0A3009215";
            string vin4 = "0000LY341U49810IX";

            try
            {
                int checkDigit = CalculateCheckDigit(vin);
                Console.WriteLine("Die Prüfziffer der VIN ist: " + checkDigit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static int CalculateCheckDigit(string vin)
        {
            if (vin.Length != 17)
            {
                throw new ArgumentException("Die VIN muss 17 Zeichen lang sein.");
            }

            Dictionary<char, int> implementationTable = new Dictionary<char, int>
        {
            {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6}, {'G', 7}, {'H', 8}, {'J', 1}, {'K', 2},
            {'L', 3}, {'M', 4}, {'N', 5}, {'P', 7}, {'R', 9}, {'S', 2}, {'T', 3}, {'U', 4}, {'V', 5}, {'W', 6},
            {'X', 7}, {'Y', 8}, {'Z', 9}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7},
            {'8', 8}, {'9', 9}, {'0', 0}
        };

            int[] weights = {9, 8, 7, 6, 5, 4, 3, 2, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum = 0;
            for (int i = 0; i < vin.Length; i++)
            {
                char c = vin[i];
                if (!implementationTable.ContainsKey(c))
                {
                    throw new ArgumentException("Ungültiges Zeichen in der VIN.");
                }
                int value = implementationTable[c];
                sum += value * weights[i];
            }

            int p = sum % 11;
            return p == 10 ? 'X' : p;
        }
    }
}
