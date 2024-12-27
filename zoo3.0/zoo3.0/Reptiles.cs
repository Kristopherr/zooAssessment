using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoo3._0;

namespace zoo3
{
    internal class Reptiles : Animals
    {
        /// <summary>
        /// Retile class to inherit from the Animals class and allow the user to add a reptile to the zoo.
        /// Kristopher mann
        /// 25/12/2024
        /// </summary>

        public string NAME { get; set; }
        public int AGE { get; set; }
        public string GENDER { get; set; }
        public string ANIMALID { get; set; }
        public string OPTIONS { get; set; }
        public string TYPE { get; set; }

        private static int NextAnimalID = 1; 

        // Constructor
        public Reptiles(string name, int age, string options, string gender, string animalType) : base(name, age, gender, animalType)
        {
            NAME = name;
            AGE = age;
            GENDER = gender;
            OPTIONS = options;
            TYPE = animalType; 
            ANIMALID = GenerateAnimalID(name); 
        }

        
        private string GenerateAnimalID(string name)
        {
            char initial = char.ToUpper(name[0]); 
            return $"{initial}{NextAnimalID++:D4}";
        }

        
        public static void PromptUserForChoice()
        {
            List<string> options = new List<string>
            {
                "Crocodiles",
                "Snakes",
                "Bearded Dragons",
                "Lizards"
            };

          
            Console.WriteLine("Choose an animal from the following list:");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            int choice = int.Parse(Console.ReadLine()) - 1;
            if (choice < 0 || choice >= options.Count)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                return;
            }

           
            Console.Write("Enter the name of the animal: ");
            string name = Console.ReadLine();

            Console.Write("Enter the Gender of the animal (Male/Female): ");
            string gender = Console.ReadLine();

            Console.Write("Enter the age of the animal: ");
            int age = int.Parse(Console.ReadLine());

            string animalType = options[choice];

            Console.WriteLine("Select the Cage ID where the animal will be housed:");
            int cageId = PromptForCageId();


            Reptiles selectedAnimal = new Reptiles(name, age, options[choice], gender, animalType);

            
            bool canFitInCage = EnclosureManager.CanAddAnimalToCage(selectedAnimal, cageId);

            if (canFitInCage)
            {
                
                WriteToFile(selectedAnimal, cageId);
            }
            else
            {
                Console.WriteLine("The selected animal cannot fit in the chosen cage.");
            }
        }

        
        private static int PromptForCageId()
        {
            var cages = new Dictionary<int, string>();
            foreach (var line in File.ReadLines("C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Cage.txt"))
            {
                var parts = line.Split(',');
                int cageId = int.Parse(parts[1].Trim());
                string cageName = parts[2].Trim();
                cages[cageId] = cageName;
            }

            foreach (var cage in cages)
            {
                Console.WriteLine($"Cage ID: {cage.Key}, Cage Name: {cage.Value}");
            }

            Console.Write("Enter the Cage ID: ");
            int selectedCageId = int.Parse(Console.ReadLine());

            if (!cages.ContainsKey(selectedCageId))
            {
                Console.WriteLine("Invalid Cage ID. Please try again.");
                return PromptForCageId();
            }

            return selectedCageId;
        }

        
        private static void WriteToFile(Reptiles animal, int cageId)
        {
            string filePath = "C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Animals.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                
                writer.WriteLine($"{cageId},{animal.ANIMALID},{animal.NAME},{animal.AGE},{animal.OPTIONS},{animal.GENDER},{animal.ANIMALTYPE}");
            }
            Console.WriteLine("Animal information saved to file.");
        }
    }
}


