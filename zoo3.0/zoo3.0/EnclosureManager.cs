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
            { 1, new Dictionary<string, int> { { "Tiger", 3 }, { "Horse/Donkey", 8 }, { "Zebra/Marmoset monkeys", 8 } } },
            { 2, new Dictionary<string, int> { { "Tiger", 2 }, { "Horse/Donkey", 6 }, { "Zebra/Marmoset monkeys", 6 } } },
            { 3, new Dictionary<string, int> { { "Marmoset monkeys", 6 }, { "Apes", 6 } } },
            { 4, new Dictionary<string, int> { { "Marmoset monkeys", 2 }, { "Apes", 6 }, { "Guinea Pig/Rabbit", 20 } } },
            { 5, new Dictionary<string, int> { { "Crocodile", 3 } } },
            { 6, new Dictionary<string, int> { { "Snakes", 6 }, { "Lizard/Bearded Dragons", 2 } } },
            { 7, new Dictionary<string, int> { { "Snake", 2 }, { "Lizard/Bearded Dragons", 4 } } },
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


        public static bool CanAddAnimalToCage(Animals animal, int cageId)
        {
            //Find the enclosureId for the given cageId
            int enclosureId = GetEnclosureIdByCage(cageId);
            if (enclosureId == -1)
            {
                Console.WriteLine("Invalid cage ID.");
                return false;
            }

            
            var enclosureRules = enclosureCapacities[enclosureId];
            if (!cageOccupancy.ContainsKey(cageId))
            {
                cageOccupancy[cageId] = new Dictionary<string, int>();
            }

            //check the occupancy of the cage
            var currentCageOccupancy = cageOccupancy[cageId];

            foreach (var rule in enclosureRules)
            {
                // Split grouped animal types via /
                var group = rule.Key.Split('/');
                //if elements in the array meet the condition then check if type match the ANIMALTYPE igonoring case sensitivity
                if (Array.Exists(group, type => type.Equals(animal.ANIMALTYPE, StringComparison.OrdinalIgnoreCase))) 
                {
                    //check the current amount of animals in a cage
                    int currentCount = 0;
                    foreach (var type in group)
                    {
                        if (currentCageOccupancy.ContainsKey(type))
                        {
                            currentCount += currentCageOccupancy[type];
                        }
                    }

                    //Check if adding this animal would exceed the limit
                    if (currentCount + 1 > rule.Value)
                    {
                        Console.WriteLine($"Cannot add {animal.ANIMALTYPE}. The cage exceeds its limit of {rule.Value} for {rule.Key}.");
                        return false;
                    }

                    // pdate occupancy for this specific animal type
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
    }
}
