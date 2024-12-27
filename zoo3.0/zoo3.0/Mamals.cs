using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoo3._0;

namespace zoo3
{
    internal class Mamals : Animals
    {
        /// <summary>
        /// class that inherits from the Animals class and allows the user to add a mammal to the zoo.
        /// Kristopher Mann
        /// 16/12/2024
        /// </summary>
        public string NAME { get; set; }
        public int AGE { get; set; }
        public string GENDER { get; set; }
        public string ANIMALID { get; set; }
        public string OPTIONS { get; set; }
        public string TYPE { get; set; }//horses,tiger or zebra etc

        private static int NextAnimalID = 1; //counter for the ID

        // Constructor
        public Mamals(string name, int age, string options, string gender, string animalType) : base(name, age, gender, animalType)
        {
            NAME = name;
            AGE = age;
            GENDER = gender;
            OPTIONS = options;
            TYPE = animalType;
            ANIMALID = GenerateAnimalID(name); //Generate the ID when the animal is created
        }

        //Creates the ID based on the first initial of the name and the number and the type of animal "mamal" so it wil start with an M
        private string GenerateAnimalID(string name)
        {
            char initial = char.ToUpper(name[0]); // take first initial for ID
            return $"M{initial}{NextAnimalID++:D4}";
        }

        //Method to prompt the user for animal details
        public static void PromptUserForChoice()
        {
            List<string> options = new List<string>
            {
                "Apes",
                "Marmoset monkeys",
                "Tigers",
                "Rabbits",
                "Guinea pigs",
                "Horses",
                "Donkeys",
                "Zebra"
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

            Console.WriteLine("Select the Cage ID where the animal will be housed:");
            int cageId = PromptForCageId();

            string animalType = options[choice]; 

            //Create the animal object
            Mamals selectedAnimal = new Mamals(name, age, options[choice], gender, animalType);

            //Check if the selected animal can fit in the chosen cage
            bool canFitInCage = EnclosureManager.CanAddAnimalToCage(selectedAnimal, cageId);

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

        //Prompt for a valid Cage ID
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

        //Write the animal information to the file
        private static void WriteToFile(Mamals animal, int cageId)
        {
            string filePath = "C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Animals.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                // the format in which i would like the details to be written to the file
                writer.WriteLine($"{cageId},{animal.ANIMALID}, {animal.NAME}, {animal.AGE}, {animal.OPTIONS}, {animal.GENDER}");
            }
            Console.WriteLine("Animal information saved to file.");
        }
    }
}