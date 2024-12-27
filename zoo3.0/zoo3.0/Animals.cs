using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    internal class Animals
    {
        /// <summary>
        /// animal class to pass the animal details to the other classes
        /// kristopher mann
        /// 10/12/2024
        /// </summary>
        public string ANIMALNAME { get; set; }
        public int ANIMALAGE {get; set; }
        public string ANIMALGENDER { get; set; }
        public string ANIMALTYPE { get; set; }


        public Animals(string name, int animalAge, string gender, string animalType)
        {
            ANIMALNAME = name;
            ANIMALAGE = animalAge;
            ANIMALGENDER = gender;
            ANIMALTYPE = animalType;
        }

        
        public static void AddAnimalToCage()
        {
            
            // basic menu that will give the user a choice of what animal to use, using inheritance
            Console.WriteLine("Select the type of animal to add to the cage:");
            Console.WriteLine("1. Mammal");
            Console.WriteLine("2. Reptile");
            Console.WriteLine("3. Bird");
            Console.Write("Enter your choice (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("You selected Mammal.");
                    Mamals.PromptUserForChoice();
                    break;
                case "2":

                    Console.WriteLine("You selected Reptile.");
                    Reptiles.PromptUserForChoice();
                    break;
                case "3":
                    Console.WriteLine("You selected Bird.");
                    Birds.PromptUserForChoice();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                    return;
            }

            
        }

        public static void RemoveAnimalFromFile()
        {
            string filePath = "C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Animals.txt";

            // Display the file's contents to the user
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The animals data file does not exist.");
                return;
            }

            Console.WriteLine("Current animals in the zoo:");
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            // Prompt for the animal ID to remove
            Console.Write("\nEnter the Animal ID of the animal you wish to remove: ");
            string animalIdToRemove = Console.ReadLine();

            // Check if the ID exists and remove it
            var updatedLines = lines.Where(line => !line.Contains($",{animalIdToRemove},")).ToList();

            if (updatedLines.Count == lines.Length)
            {
                Console.WriteLine("Animal ID not found. No changes made.");
                return;
            }

            // Rewrite the file with the updated lines
            File.WriteAllLines(filePath, updatedLines);
            Console.WriteLine("Animal removed successfully.");
        }
    }

}

