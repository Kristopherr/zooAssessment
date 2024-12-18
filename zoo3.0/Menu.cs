using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3._0
{
    internal class Menu
    {
        /// <summary>
        /// Kristopher Mann
        /// Menu class that contains a display menu method with the option of selecting a choice.
        /// </summary>
        public static void DisplayMenu()
        {
            int choice = 0;
            do
            {
                
                Console.WriteLine("Welcome to the Zoo Management System");
                Console.WriteLine("1. Enclosures");
                Console.WriteLine("2. Keepers");
                Console.WriteLine("3. Cages");
                Console.WriteLine("4. Add Cages");
                Console.WriteLine("5. Add Keepers");
                Console.WriteLine("6. Add Animals");
                Console.WriteLine("7. View All"); // Added option for viewing all
                Console.WriteLine("8. Exit");
                Console.WriteLine("Please select an option:");

                choice = int.Parse(Console.ReadLine());
            
                switch (choice)
                {
                    case 1:
                        //Enclosure.DisplayDataList();
                        break;
                    case 2:
                        //Keepers.DisplayDataList();
                        break;
                    case 3:
                        //Cages.DisplayDataList();
                        break;
                    case 4:
                        //Cages.AddCages();
                        break;
                    case 5:
                        //Keepers.AddKeepers();
                        break;
                    case 6:
                        //Animals.AddAnimals();
                        break;
                    case 7:
                        DictionaryLink.DisplayDataList();
                        break;
                    case 8:
                        
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (choice != 8);
        }
    }
}
