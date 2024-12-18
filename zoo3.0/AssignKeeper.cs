using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    public class AssignKeeper
    {
        private const string FilePath = "../../../Cages.txt";  // File path to the cages data file
        private Dictionary<int, List<string>> cages;           // Dictionary to store cage IDs with associated keeper names

        public AssignKeeper()
        {
            cages = LoadCages();  // Initialize cages by loading from file
        }

        // Loads cage data from the file using StreamReader
        private Dictionary<int, List<string>> LoadCages()
        {
            var cages = new Dictionary<int, List<string>>();

            // Opens the file
            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) // Read each line until end of file
                {
                    var parts = line.Split(',');           // Split line by comma to separate cage ID and keeper name
                    int cageId = int.Parse(parts[0].Trim()); // Parse and trim cage ID
                    string keeperName = parts[1].Trim();

                    // If cage ID doesn't exist in the dictionary, create a new list for it
                    if (!cages.ContainsKey(cageId))
                    {
                        cages[cageId] = new List<string>();
                    }
                    cages[cageId].Add(keeperName); // Add keeper name to the cage ID
                }
            }
            return cages; // Return populated dictionary
        }

        // Method to assign a keeper to a new cage
        public void AssignKeeperToCage(string keeperName, int newCageId)
        {
            // If the new cage ID doesn't have a list, create one
            if (!cages.ContainsKey(newCageId))
            {
                cages[newCageId] = new List<string>();
            }
            cages[newCageId].Add(keeperName); // Assign keeper to the new cage

            UpdateCageFile(); // Update file to save changes
            Console.WriteLine($"Keeper {keeperName} has been assigned to cage ID {newCageId}.");
        }

        // Update the file with the current keeper data using StreamWriter
        private void UpdateCageFile()
        {
            using (StreamWriter writer = new StreamWriter(FilePath, false)) // Overwrite file (false parameter)
            {
                foreach (var cage in cages)
                {
                    foreach (var keeper in cage.Value)
                    {
                        writer.WriteLine($"{cage.Key}, {keeper}"); // Write cage ID and keeper name to file
                    }
                }
            }
        }

        // Displays all the keepers by cage for the user to see
        public void DisplayAllKeepers()
        {
            Console.WriteLine("Keepers by Cage:");
            foreach (var cage in cages)
            {
                int cageId = cage.Key;
                List<string> keeperList = cage.Value;

                Console.WriteLine($"\nCage ID: {cageId}");
                if (keeperList.Count > 0)
                {
                    Console.WriteLine("Keepers:");
                    foreach (var keeper in keeperList)
                    {
                        Console.WriteLine($"\t{keeper}");
                    }
                }
                else
                {
                    Console.WriteLine("No keepers assigned.");
                }
            }
        }

        // Checks if a keeper exists in the list
        public bool KeeperExists(string keeperName)
        {
            // Loop through each list of keepers in the dictionary
            foreach (var keeperList in cages.Values)
            {
                // Check if the keeper's name exists in the current list
                foreach (var keeper in keeperList)
                {
                    if (keeper == keeperName)
                    {
                        return true; // Return true if the keeper is found
                    }
                }
            }
            return false; // Return false if no matching keeper is found
        }

        // Allows user to assign a new keeper to a cage
        public void AssignNewCage()
        {
            AssignKeeper cageManager = new AssignKeeper(); // Initialize cage manager to handle keeper data

            Console.WriteLine("Enter the keeper's name to assign:");
            string keeperName = Console.ReadLine();

            // Verify if the keeper exists in the file
            if (!cageManager.KeeperExists(keeperName))
            {
                Console.WriteLine("Keeper does not exist");
                return;
            }

            Console.WriteLine("Enter the cage ID to assign the keeper to:");
            if (int.TryParse(Console.ReadLine(), out int newCageId)) // Validate cage ID input
            {
                cageManager.AssignKeeperToCage(keeperName, newCageId); // Assign keeper to cage
            }
            else
            {
                Console.WriteLine("Invalid cage ID. Please enter a numeric value.");
            }
        }
    }
}
