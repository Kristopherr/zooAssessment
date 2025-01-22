using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoo3._0;

namespace zoo3
{
    internal class Birds : Animals
    {
        /// <summary>
        /// this class will allow the user to select a type of bird and add it to the zoo
        /// Kristopher Mann
        /// 16/12/2024
        /// </summary>
        public string NAME { get; set; }
        public int AGE { get; set; }
        public string GENDER { get; set; }
        public string ANIMALID { get; set; }
        public string OPTIONS { get; set; }
        public string TYPE { get; set; }  // Animal type (e.g., "Owl", "Vulture", etc.)

        private static int NextAnimalID = 1; // Counter for generating unique animal IDs

        // Constructor
        public Birds(string name, int age, string options, string gender, string animalType) : base(name, age, gender, animalType)
        {
            NAME = name;
            AGE = age;
            GENDER = gender;
            OPTIONS = options;
            TYPE = animalType; //emu or penguin etc
            ANIMALID = GenerateAnimalID(name);//creates an id when the animal is created
        }

        // Method to generate a unique animal ID starts with the category of the animal then the first inital of the name and then a number
        private string GenerateAnimalID(string name)
        {
            char initial = char.ToUpper(name[0]); //Gets the first initial of the name
            return $"B{initial}{NextAnimalID++:D4}";
        }

        //Method to prompt the user for animal details and store the information
        public static void PromptUserForChoice()
        {
            List<string> options = new List<string>
            {
                "Owls - Flying",
                "Vultures - Flying",
                "Emus - None Flying",
                "Penguins - Non Flying",
                "Eagles - Flying"
            };

            //Display animal choices
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

            //Collect details about the animal
            Console.Write("Enter the name of the animal: ");
            string name = Console.ReadLine();

            Console.Write("Enter the Gender of the animal (Male/Female): ");
            string gender = Console.ReadLine();

            Console.Write("Enter the age of the animal: ");
            int age = int.Parse(Console.ReadLine());

            //Select animal type based on the chosen option
            string animalType = options[choice];

            Console.WriteLine("Select the Cage ID where the animal will be housed:");
            int cageId = PromptForCageId();

            //Create the animal object
            Birds selectedAnimal = new Birds(name, age, options[choice], gender, animalType);

            //Check if the selected animal can fit in the chosen cage
            bool canFitInCage = EnclosureManager.canAddAnimalToCage(selectedAnimal, cageId);

            if (canFitInCage)
            {
                //Write the animal details to a file
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
            foreach (var line in File.ReadLines("C:\\Users\\20024538\\source\\repos\\zoo3.0\\zoo3.0\\Cage.txt"))
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

        private static void WriteToFile(Birds animal, int cageId)
        {
            string filePath = "C:\\Users\\20024538\\source\\repos\\zoo3.0\\zoo3.0\\Animals.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                // Write animal info including ID, name, age, gender, and cage ID
                writer.WriteLine($"{cageId},{animal.ANIMALID},{animal.NAME},{animal.AGE},{animal.OPTIONS},{animal.GENDER},{animal.ANIMALTYPE}");
            }
            Console.WriteLine("Animal information saved to file.");
        }
    }
}
    
