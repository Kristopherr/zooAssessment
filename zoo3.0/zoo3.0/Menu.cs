using GlasgowZoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3._0
{
    internal class Menu : DictionaryLink
    {
        /// <summary>
        /// Kristopher Mann
        /// Menu class that contains a display menu method with the option of selecting a choice.
        /// </summary>
       

        public Menu() { }

        public  void displayMenu()
        {
            var enclosures = new Dictionary<int, string>();
            int choice = 0;
            do
            {
                
                Console.WriteLine("Welcome to the Zoo Management System");
                Console.WriteLine("1. Enclosures");
                Console.WriteLine("2. Cages");
                Console.WriteLine("3. Keeper Menu");
                Console.WriteLine("4. Add Keeper");
                Console.WriteLine("5. Add Animals");
                Console.WriteLine("6. Delete an Animal");
                Console.WriteLine("7. View All");
                Console.WriteLine("8. Exit");
                Console.WriteLine("Please select an option:");

                choice = int.Parse(Console.ReadLine());
            
                switch (choice)
                {
                    case 1:
                        Enclosure.displayAllEnclosures();
                        break;
                    case 2:
                        Cages.DisplayAllCages();
                        break;
                    case 3:
                        DictionaryLink.DisplayDataList();
                        keeperMenu();
                        break;
                    case 4:
                        Keepers.addKeeper();
                        break;

                    case 5:
                        EnclosureManager.displayEnclosureCapacities();
                        Animals.AddAnimalToCage();
                        break;
                    case 6:
                        Animals.RemoveAnimalFromFile();
                        break;
                    case 7:
                        DictionaryLink.DisplayDataList();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (choice != 8);
        }

        public void keeperMenu()
        {
            AssignKeeper assignKeeper = new AssignKeeper();

            int choice = 0;
            do
            {
                Console.WriteLine("\nKeeper Menu\n");
                
                Console.WriteLine("1. Assign Keeper");
                Console.WriteLine("2. View Keepers");
                Console.WriteLine("3. Delete Keeper");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Please select an option:");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {

                    case 1:
                        
                        assignKeeper.displayAllKeepers();
                        assignKeeper.assignNewCage();
                        break;
                    case 2:
                        assignKeeper.displayAllKeepers();
                        break;
                    case 3:
                        assignKeeper.displayAllKeepers();
                        assignKeeper.deleteKeeper();
                        break;
                    case 4:
                        displayMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (choice != 4);
        }


    }
}
