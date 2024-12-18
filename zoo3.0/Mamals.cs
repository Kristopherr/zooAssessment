using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    internal class Mamals:Animals
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Sex { get; set; }

        public Mamals(string name, string type, string sex) : base(name, type, sex)
        {
            Type = type;
            Sex = sex;
        }

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

            Console.WriteLine("Choose an animal from the following list:");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            int choice = Convert.ToInt32(Console.ReadLine()) - 1;
            if (choice < 0 || choice >= options.Count)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                return;
            }

            Console.Write("Enter the name of the animal: ");
            string name = Console.ReadLine();

            Console.Write("Enter the sex of the animal (Male/Female): ");
            string sex = Console.ReadLine();

            Mamals selectedAnimal = new Mamals(name, options[choice], sex);
            WriteToFile(selectedAnimal);
        }

        private static void WriteToFile(Mamals animal)
        {
            string filePath = "animals.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"Name: {animal.Name}, Type: {animal.Type}, Sex: {animal.Sex}");
            }
            Console.WriteLine("Animal information saved to file.");
        }
    }
}
