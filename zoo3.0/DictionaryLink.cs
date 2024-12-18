using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    
    public class DictionaryLink
    {

        /// <summary>
        /// Kristopher mann
        /// Class to link Enclosures, Cages, Animals and Keepers through the use of dictionaries
        /// </summary>
        public static void DisplayDataList()
        {
            var enclosures = new Dictionary<int, string>();
            foreach (var line in File.ReadLines("C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Enclosure.txt"))
            {
                var parts = line.Split(','); // splits the file into 2 sections via the comma
                int enclosureId = int.Parse(parts[0].Trim()); // Assigns the number as enclosureID
                string enclosureName = parts[1].Trim(); // Assigns the text within the file to EnclosureName
                enclosures[enclosureId] = enclosureName;
            }

            // Step 2: Read the cages file
            var cages = new Dictionary<int, List<(int cageId, string cageName)>>(); // first int is enclosureId, then returns a tuple for cage name and id
            foreach (var line in File.ReadLines("C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Cage.txt"))
            {
                var parts = line.Split(",");
                int enclosureId = int.Parse(parts[0].Trim());
                int cageId = int.Parse(parts[1].Trim());
                string cageName = parts[2].Trim();

                if (!cages.ContainsKey(enclosureId)) // checks if cages DOES NOT contain a key for enclosure
                {
                    cages[enclosureId] = new List<(int, string)>();
                }
                cages[enclosureId].Add((cageId, cageName));
            }

            // Read the animals file
            var animals = new Dictionary<int, List<string>>();
            foreach (var line in File.ReadLines("C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Animals.txt"))
            {
                var parts = line.Split(",");
                int cageId = int.Parse(parts[0].Trim());
                string animalName = parts[1].Trim();

                if (!animals.ContainsKey(cageId)) // checks if animals DO NOT contain a key for cages
                {
                    animals[cageId] = new List<string>();
                }
                animals[cageId].Add("\n \t" + animalName);
            }

            // Creating a data list for keepers
            var keepers = new Dictionary<int, List<string>>();
            foreach (var line in File.ReadLines("C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Keepers.txt"))
            {
                var parts = line.Split(',');
                int cageId = int.Parse(parts[0].Trim());
                string keeperName = parts[1].Trim();

                if (!keepers.ContainsKey(cageId))
                {
                    keepers[cageId] = new List<string>(); // adds keeper name with a cageID as the key
                }
                keepers[cageId].Add(keeperName);
            }

            // Linking the data together with the keys
            var linkedData = new Dictionary<string, Dictionary<string, (List<string> Animals, List<string> Keepers)>>();

            foreach (var enclosure in enclosures)
            {
                string enclosureName = enclosure.Value;
                int enclosureId = enclosure.Key;

                if (!linkedData.ContainsKey(enclosureName))
                {
                    linkedData[enclosureName] = new Dictionary<string, (List<string>, List<string>)>();
                }

                if (cages.ContainsKey(enclosureId))
                {
                    foreach (var cage in cages[enclosureId])
                    {
                        string cageName = cage.cageName;
                        int cageId = cage.cageId;

                        // Get animals for the cage
                        List<string> cageAnimals = animals.ContainsKey(cageId) ? animals[cageId] : new List<string>();

                        // Get keepers for the cage
                        List<string> cageKeepers = keepers.ContainsKey(cageId) ? keepers[cageId] : new List<string>();

                        linkedData[enclosureName][cageName] = (cageAnimals, cageKeepers);
                    }
                }
            }

            foreach (var enclosure in linkedData)
            {
                Console.WriteLine($"Enclosure: {enclosure.Key}"); // Print enclosure name
                foreach (var cage in enclosure.Value)
                {
                    Console.WriteLine($"Cage: {cage.Key}"); // Print cage name

                    List<string> cageAnimals = cage.Value.Animals; // Get all animals in the cage
                    List<string> cageKeepers = cage.Value.Keepers; // Get all keepers for the cage

                    // Print animals, joining them with commas, or show "None" if there are no animals
                    if (cageAnimals.Count > 0)
                    {
                        Console.WriteLine("Animals: " + string.Join(", ", cageAnimals));
                    }
                    else
                    {
                        Console.WriteLine("Animals: None");
                    }

                    // Print keepers, joining them with commas, or show "None" if there are no keepers
                    if (cageKeepers.Count > 0)
                    {
                        Console.WriteLine("Keepers: " + string.Join(", ", cageKeepers));
                    }
                    else
                    {
                        Console.WriteLine("Keepers: None");
                    }
                }
            }
        }
    }
}

       
