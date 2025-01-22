using GlasgowZoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    public class AssignKeeper
    {
        /// <summary>
        /// this class allows the user to assign a keeper to a cage
        /// </summary>
        private const string KeeperFilePath = "C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Keepers.txt"; // Define the file path as a constant
        private Dictionary<int, List<string>> cages; // Dictionary to store cage IDs and their assigned keeper IDs
        private Dictionary<string, Keepers> keeperDetails; // Dictionary to store keeper details by keeper ID

        public AssignKeeper()
        {
            cages = loadKeepers();  // Initialise cages by loading from file
            keeperDetails = loadKeeperDetails(); // Initialise keeper details by loading from file
        }



        // Loads cage data from the file using StreamReader
        private Dictionary<int, List<string>> loadKeepers()
        {
            var cages = new Dictionary<int, List<string>>();
            try
            {
                using (StreamReader reader = new StreamReader(KeeperFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        int cageId = int.Parse(parts[0].Trim());
                        string keeperID = parts[1].Trim();

                        // If the cage ID is not already in the dictionary, create a new list for it
                        if (!cages.ContainsKey(cageId))
                        {
                            cages[cageId] = new List<string>();
                        }
                        cages[cageId].Add(keeperID); // Assign the keeper ID to the cage
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading keeper data: {ex.Message}");
            }
            return cages;
        }



        // Updates the keeper file with the latest data from the cages dictionary
        private void updateKeeperFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(KeeperFilePath, false)) // Overwrite the file 
                {
                    foreach (var cage in cages)
                    {
                        foreach (var keeperID in cage.Value)
                        {
                            if (keeperDetails.ContainsKey(keeperID))
                            {
                                var keeper = keeperDetails[keeperID];
                                // Write the keeper's details to the file
                                writer.WriteLine($"{cage.Key},{keeper.KEEPERID},{keeper.KEEPERNAME},{keeper.KEEPERAGE},{keeper.KEEPERSPECIALTY}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating the file: {ex.Message}");
            }
        }



        // Checks if a keeper exists in the system
        public bool keeperExists(string keeperID)
        {
            return keeperDetails.ContainsKey(keeperID);
        }



        // Displays all the keepers by cage for the user to see
        public void displayAllKeepers()
        {
            Console.WriteLine("Keepers by Cage:");
            foreach (var cage in cages)
            {
                int cageId = cage.Key;
                var keeperList = cage.Value;

                Console.WriteLine($"\nCage ID: {cageId}");
                if (keeperList.Any())
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


        // Assigns a new keeper to a cage and optionally removes them from the current cage
        public void assignNewCage()
        {
            Console.WriteLine("Enter the keeper's ID to assign:");
            string keeperID = Console.ReadLine().ToUpper();

            if (!keeperExists(keeperID))
            {
                Console.WriteLine("Keeper does not exist.");
                return;
            }

            int assignedCageCount = cages.Values.Count(cageList => cageList.Contains(keeperID));

            if (assignedCageCount >= 4)
            {
                Console.WriteLine("This keeper is already assigned to the maximum of 4 cages.");
                return;  // Prevent further assignment
            }

            Console.WriteLine("Enter the cage ID to assign the keeper to:");
            if (int.TryParse(Console.ReadLine(), out int newCageId))
            {
                Console.WriteLine("Do you want to remove the keeper from a specific cage? (yes/no):");
                string specificCageResponse = Console.ReadLine().ToLower();

                if (specificCageResponse == "yes")
                {
                    Console.WriteLine("Enter the cage ID to remove the keeper from:");
                    if (int.TryParse(Console.ReadLine(), out int cageID))
                    {
                        // Ensure keeper is assigned to this cage
                        if (cages.ContainsKey(cageID) && cages[cageID].Contains(keeperID))
                        {
                            removeKeeperFromCage(keeperID); // Remove the keeper from the specified cage
                        }
                        else
                        {
                            Console.WriteLine($"Keeper {keeperID} is not assigned to cage {cageID}.");
                            return; // Exit method if keeper is not assigned to the cage
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid cage ID.");
                        return; // Exit method if cage ID is invalid
                    }
                }

                // Proceed to assign the keeper to the new cage
                assignKeeperToNewCage(keeperID, newCageId);
            }
            else
            {
                Console.WriteLine("Invalid cage ID. Please enter a numeric value.");
            }
        }



        // Assigns a keeper to a cage, removing from the current cage if needed
        public void assignKeeperToCage(string keeperID, int newCageId, bool removeFromCurrent = false)
        {
            if (removeFromCurrent)
            {
                removeKeeperFromCage(keeperID); // Remove keeper from the current cage
            }

            assignKeeperToNewCage(keeperID, newCageId); // Assign keeper to the new cage
            updateKeeperFile(); // Update the file to reflect changes
        }

        // Removes a keeper from any cage they are currently assigned to
        private void removeKeeperFromCage(string keeperID)
        {
            foreach (var cage in cages)
            {
                if (cage.Value.Contains(keeperID))
                {
                    cage.Value.Remove(keeperID);
                    Console.WriteLine($"Keeper {keeperID} removed from cage ID {cage.Key}.");
                    return;
                }
            }
            Console.WriteLine($"Keeper {keeperID} not found in any cage.");
        }

        // Assigns a keeper to a new cage
        private void assignKeeperToNewCage(string keeperID, int newCageId)
        {
            if (!cages.ContainsKey(newCageId))
            {
                cages[newCageId] = new List<string>();
            }
            cages[newCageId].Add(keeperID);
            Console.WriteLine($"Keeper {keeperID} assigned to cage ID {newCageId}.");
        }


        // delete a keeper, with options to remove from cage or delete from system
        public void deleteKeeper()
        {
            Console.WriteLine("Enter the keeper's ID to delete:");
            string keeperID = Console.ReadLine().ToUpper();

            if (!keeperExists(keeperID))
            {
                Console.WriteLine("Keeper does not exist.");
                return;
            }

            // Count the number of cages the keeper is assigned to
            int assignedCageCount = cages.Values.Count(cageList => cageList.Contains(keeperID));

            if (assignedCageCount == 0)
            {
                Console.WriteLine($"Keeper {keeperID} is not assigned to any cage.");
            }
            else
            {
                Console.WriteLine($"Keeper {keeperID} is assigned to {assignedCageCount} cage(s).");
            }

            // prompt to remove the keeper from a specific cage or not
            Console.WriteLine("Do you want to remove the keeper from a specific cage? (yes/no):");
            string specificCageRemove = Console.ReadLine().ToLower();

            if (specificCageRemove == "yes")
            {
                Console.WriteLine("Enter the cage ID to remove the keeper from:");
                if (int.TryParse(Console.ReadLine(), out int cageID))
                {
                    if (cages.ContainsKey(cageID) && cages[cageID].Contains(keeperID))
                    {
                        cages[cageID].Remove(keeperID);
                        Console.WriteLine($"Keeper {keeperID} has been removed from cage {cageID}.");
                    }
                    else
                    {
                        Console.WriteLine($"Keeper {keeperID} is not assigned to cage {cageID}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid cage ID.");
                    return;
                }
            }

            // Check if keeper has any remaining cage assignments and include to the count if they are
            assignedCageCount = cages.Values.Count(cageList => cageList.Contains(keeperID));

            if (assignedCageCount == 0)
            {
                // delete the keeper from the system entirely if agree'd
                Console.WriteLine("Keeper is no longer assigned to any cages. Do you want to delete the keeper from the system completely? (yes/no):");
                string keeperDel = Console.ReadLine().ToLower();

                if (keeperDel == "yes")
                {
                    if (keeperDetails.ContainsKey(keeperID))
                    {
                        keeperDetails.Remove(keeperID);
                        Console.WriteLine($"Keeper {keeperID} has been deleted from the system.");
                    }
                    else
                    {
                        Console.WriteLine($"Keeper {keeperID} does not exist in the system details.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Keeper {keeperID} is still assigned to {assignedCageCount} cage(s). No further actions taken.");
            }

            // Update the file to reflect the changes
            updateKeeperFile();
        }

        // Loads keeper details from the file
        private Dictionary<string, Keepers> loadKeeperDetails()
        {
            var keeperDetails = new Dictionary<string, Keepers>();
            try
            {
                using (StreamReader reader = new StreamReader(KeeperFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        int assignedCageID = int.Parse(parts[0].Trim());
                        string keeperID = parts[1].Trim();
                        string keeperName = parts[2].Trim();
                        int keeperAge = int.Parse(parts[3].Trim());
                        string keeperSpecialty = parts[4].Trim();

                        // Store the keeper details in the dictionary
                        keeperDetails[keeperID] = new Keepers(keeperName, keeperAge, keeperSpecialty, assignedCageID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading keeper details: {ex.Message}");
            }
            return keeperDetails;
        }
    }
}
