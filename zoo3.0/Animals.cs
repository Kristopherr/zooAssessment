using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    internal class Animals
    {
        public string ANIMALNAME { get; set; }
        public string ANIMALTYPE { get; set; }
        public string ANIMALGENDER { get; set; }


        public Animals(string name, string type, string gender)
        {
            ANIMALNAME = name;
            ANIMALTYPE= type;
            ANIMALGENDER = gender;
        }

        

        protected static (string, string) GetAnimalDetails(string name, string gender)
        {
            

            Console.Write("Enter the name of the animal: ");
            name = Console.ReadLine();
            Console.Write("Enter the sex of the animal (Male/Female): ");
            gender = Console.ReadLine();

            return (name, gender);
        }

        public static void AddAnimalToCage(string name, string type, string gender)
        {
            string animalType = string.Empty;

            Console.WriteLine("Select the type of animal to add to the cage:");
            Console.WriteLine("1. Mammal");
            Console.WriteLine("2. Reptile");
            Console.WriteLine("3. Bird");
            Console.Write("Enter your choice (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    animalType = "Mammal";
                    GetAnimalDetails(name, gender);
                    Console.WriteLine("You selected Mammal.");
                    break;
                case "2":
                    animalType = "Reptile";
                    GetAnimalDetails(name, gender);
                    Console.WriteLine("You selected Reptile.");
                    break;
                case "3":
                    animalType = "Bird";
                    GetAnimalDetails(name, gender);
                    Console.WriteLine("You selected Bird.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                    return;
            }

            // Here you can add code to use animalName, animalSex, and animalType as needed
        }         
        
    }
}
