using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3._0
{
    internal class EnclosureManager
    {
        /// <summary>
        /// this class will control the amount of animals and the type that can go into each enclosure
        /// </summary>
        private static Dictionary<int, Dictionary<string, int>> enclosureCapacities = new Dictionary<int, Dictionary<string, int>>()
        {
            { 1, new Dictionary<string, int> { { "Tiger", 3 }, { "Horse/Donkey", 8 }, { "Zebra/Marmoset monkey", 8 } } },
            { 2, new Dictionary<string, int> { { "Tiger", 2 }, { "Horse/Donkey", 6 }, { "Zebra/Marmoset monkey", 6 } } },
            { 3, new Dictionary<string, int> { { "Marmoset monkey", 6 }, { "Ape", 6 } } },
            { 4, new Dictionary<string, int> { { "Marmoset monkey", 2 }, { "Ape", 6 }, { "Guinea Pig/Rabbit", 20 } } },
            { 5, new Dictionary<string, int> { { "Crocodile", 3 } } },
            { 6, new Dictionary<string, int> { { "Snake", 6 }, { "Lizard/Bearded Dragon", 2 } } },
            { 7, new Dictionary<string, int> { { "Snake", 2 }, { "Lizard/Bearded Dragon", 4 } } },
            { 8, new Dictionary<string, int> { { "Owls - Flying", 8 }, { "Vultures - Flying", 8 }, { "Emus - Non Flying", 8 }, { "Penguins - Non Flying", 8 },{ "Eagles - Flying", 8 } } }
        };

        private static Dictionary<int, List<int>> enclosureToCages = new Dictionary<int, List<int>>()
        { 
            { 1, new List<int> { 11, 12, 13, 14 } },  //this is the enclosure ID and the cages that are in that enclosure
            { 2, new List<int> { 21, 22, 23 } },
            { 3, new List<int> { 31, 32 } },
            { 4, new List<int> { 41, 42 } },
            { 5, new List<int> { 51 } },
            { 6, new List<int> { 61, 62, 63} },
            { 7, new List<int> { 71, 72} },
            { 8, new List<int> { 81} }
        };

        private static Dictionary<int, Dictionary<string, int>> cageOccupancy = new Dictionary<int, Dictionary<string, int>>();
        private const int MaxKeepersPerCage = 4;
        private static Dictionary<int, int> cageKeeperCounts = new Dictionary<int, int>();


        public static bool canAddAnimalToCage(Animals animal, int cageId)
        {
            int enclosureId = GetEnclosureIdByCage(cageId);
            if (enclosureId == -1)
            {
                Console.WriteLine("Invalid cage ID.");
                return false;
            }

            // Get the rules for animal types and capacities in the given enclosure
            var enclosureRules = enclosureCapacities[enclosureId];
            if (!cageOccupancy.ContainsKey(cageId))
            {
                cageOccupancy[cageId] = new Dictionary<string, int>();
            }

            // Check the current occupancy of the cage
            var currentCageOccupancy = cageOccupancy[cageId];

            // Check if the cage already contains a different animal type
            if (currentCageOccupancy.Count > 0)
            {
                // Check if animal group types already exist in the cage
                bool isSameGroupPresent = false;

                foreach (var rule in enclosureRules)
                {
                    var group = rule.Key.Split('/');
                    // If the cage already contains an animal from this group, we can add others from the same group
                    if (Array.Exists(group, type => currentCageOccupancy.ContainsKey(type)))
                    {
                        isSameGroupPresent = true;
                        break;
                    }
                }

                // if animal types are different then deny new animal
                if (!isSameGroupPresent && !currentCageOccupancy.ContainsKey(animal.ANIMALTYPE))
                {
                    Console.WriteLine($"Cannot add {animal.ANIMALTYPE}. Cage {cageId} already contains animals from a different group.");
                    return false;
                }
            }

            // check if the animal type is allowed in this enclosure and check the capacity
            foreach (var rule in enclosureRules)
            {
                // Split grouped animal typesby a /
                var group = rule.Key.Split('/');
                // Check if the animal's type matches the rule (allow any of the group types to be added)
                if (Array.Exists(group, type => type.Equals(animal.ANIMALTYPE, StringComparison.OrdinalIgnoreCase)))
                {
                    // verify current count for this animal group in the cage
                    int currentCount = 0;
                    foreach (var type in group)
                    {
                        if (currentCageOccupancy.ContainsKey(type))
                        {
                            currentCount += currentCageOccupancy[type];
                        }
                    }

                    // Check if adding this animal would exceed the limit for this group
                    if (currentCount + 1 > rule.Value)
                    {
                        Console.WriteLine($"Cannot add {animal.ANIMALTYPE}. The cage exceeds its limit of {rule.Value} for {rule.Key}.");
                        return false;
                    }

                    // Update occupancy for this specific animal type
                    if (!currentCageOccupancy.ContainsKey(animal.ANIMALTYPE))
                    {
                        currentCageOccupancy[animal.ANIMALTYPE] = 0;
                    }

                    currentCageOccupancy[animal.ANIMALTYPE]++;
                    Console.WriteLine($"{animal.ANIMALTYPE} successfully added to cage {cageId}.");
                    return true;
                }
            }

            Console.WriteLine($"{animal.ANIMALTYPE} is not allowed in cage {cageId}.");
            return false;
        }

        public static bool CanAddKeeperToCage(int cageId)
        {
            // Check if the cage already has keepers
            if (!cageKeeperCounts.ContainsKey(cageId))
            {
                cageKeeperCounts[cageId] = 0;
            }

            // Check if adding another keeper would exceed the limit
            if (cageKeeperCounts[cageId] >= MaxKeepersPerCage)
            {
                Console.WriteLine($"Cage {cageId} already has the maximum number of keepers ({MaxKeepersPerCage}).");
                return false;
            }

            // Increment the keeper count for the cage
            cageKeeperCounts[cageId]++;
            return true;
        }

        private static int GetEnclosureIdByCage(int cageId)
        {
            foreach (var enclosure in enclosureToCages)
            {
                if (enclosure.Value.Contains(cageId))
                {
                    return enclosure.Key; // Return the enclosure ID for the given cage
                }
            }
            return -1; // Return -1 if cage ID is invalid
        }



        public static void displayEnclosureCapacities()
        {
            Console.WriteLine("Enclosure Capacities:");

            foreach (var enclosure in enclosureCapacities)
            {
                int enclosureId = enclosure.Key;
                Console.WriteLine($"\nEnclosure {enclosureId}:");

                foreach (var animal in enclosure.Value)
                {
                    string animalType = animal.Key;
                    int capacity = animal.Value;

                    Console.WriteLine($"\tAnimal: {animalType}, Capacity: {capacity}");
                }
            }
        }
    }
}
