using GlasgowZoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    internal class Cages
    {


        /// <summary>
        /// reads the cage file and displays the cages as well as keepers
        /// </summary>
        public Cages()
        {

        }

        public static void DisplayAllCages()
        {
            string filePath = "C:\\Users\\20024538\\source\\repos\\zoo3.0\\zoo3.0\\Cage.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Cage file not found.");
                return;
            }

            Console.WriteLine("Cages:");
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');
                int enclosureID = int.Parse(parts[0].Trim());
                int cageId = int.Parse(parts[1].Trim());
                string cageName = parts[2].Trim();


                Console.WriteLine($" Enclosure ID:{enclosureID}, Cage ID:{cageId}, Name:{cageName}\n");
            }
        }

        public static void displayAllKeepers()
        {
            string filePath = "C:\\Users\\20024538\\source\\repos\\zoo3.0\\zoo3.0\\Keepers.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Cage file not found.");
                return;
            }

            Console.WriteLine("Cages:");
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');
                int enclosureID = int.Parse(parts[0].Trim());
                int cageId = int.Parse(parts[1].Trim());
                string cageName = parts[2].Trim();


                Console.WriteLine($"CageID:{cageId}, KeeperID:{cageId}, Name:{cageName}\n");
            }

        }
    }
}

