using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    internal class Enclosure
    {
        public int enclosureID { get; set; }
        public string enclosureName { get; set; }
        public List<Cages> cages { get; set; } = new List<Cages>();

  

        public Enclosure(int id, string name)
        {
            enclosureID = id;
            enclosureName = name;
        }

        public static void displayAllEnclosures()
        {
            string filePath = "C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Enclosure.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("enclosure.txt not found");
                return;
            }

            Console.WriteLine("Enclosures:");
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');
                int enclosureID = int.Parse(parts[0].Trim());
                string enclosureName = parts[1].Trim();


                Console.WriteLine($"Enclosure ID {enclosureID}, Type of Enclosure: {enclosureName}\n");
            }
        }

    }
}
