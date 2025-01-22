using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zoo3._0;

namespace GlasgowZoo
{
    public class Keepers
    {

        /// <summary>
        /// allows the user to create and add a new keeper and assign them to cages.
        /// 11/12/2024
        /// </summary>
        public string KEEPERNAME { get; set; }

        public int KEEPERAGE { get; set; }
        public string KEEPERSPECIALTY { get; set; }
        public int ASSIGNEDCAGEID { get; set; }

        public string KEEPERID { get; set; }

        private static int NextKeeperID = 1;
        public Keepers(string keeperName, int keeperAge, string keeperSpecialty, int assignedCageID, string keeperID = null)
        {
            KEEPERNAME = keeperName;
            KEEPERAGE = keeperAge;
            KEEPERSPECIALTY = keeperSpecialty;
            ASSIGNEDCAGEID = assignedCageID;
            KEEPERID = keeperID ?? generateKeeperID(); // using ?? to check if ID is not  null, if it is null, generate a new ID
        }

        private string generateKeeperID()
        {
            string[] idInitials = KEEPERNAME.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string initials = "";

            if (idInitials.Length > 0)
            {
                initials += idInitials[0][0]; 
                if (idInitials.Length > 1)// if the array is more than 2  then it will add a surname inital too 
                {
                    initials += idInitials[^1][0];  //index from end to call the last name entered into the array ^1
                }
                else
                {
                    initials += 'Z'; //if there's no last name, add a Z
                }
            }
            //auto-generate keeper ID
            return $"K{initials.ToUpper()}{NextKeeperID++:D4}";
        }
        public static void addKeeper()
        {
            Console.Write("Enter the first and last name of the keeper: ");
            string keeperName = Console.ReadLine();

            Console.Write("Enter the age of the keeper: ");
            int keeperAge;
            while (!int.TryParse(Console.ReadLine(), out keeperAge) || keeperAge <= 0)//checks age is greater than 0
            {
                Console.WriteLine("Invalid age. Please enter a positive number:");
            }

            Console.Write("Enter the specialty of the keeper: ");
            string keeperSpecialty = Console.ReadLine();

            Console.WriteLine("Enter the ID of the cage to assign this keeper to: ");
            int assignedCageID = promptForCageId();

            
            //Keepers keeper = new Keepers(keeperName, keeperAge, keeperSpecialty, assignedCageID);

            if (EnclosureManager.CanAddKeeperToCage(assignedCageID))
            {
                Keepers keeper = new Keepers(keeperName, keeperAge, keeperSpecialty, assignedCageID);
                writeToFile(keeper);
                Console.WriteLine($"Keeper {keeper.KEEPERNAME} successfully assigned to cage {assignedCageID}.");
            }
            else
            {
                Console.WriteLine($"Keeper {keeperName} could not be assigned to cage {assignedCageID}.");
            }
        }
        private static int promptForCageId()
        {

            var cages = new Dictionary<int, string>();
            foreach (var line in File.ReadLines("C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Cage.txt"))
            {
                var parts = line.Split(',');
                int cageId = int.Parse(parts[1].Trim());
                string cageName = parts[2].Trim();
                cages[cageId] = cageName;
            }

            Console.WriteLine("Available cages:");
            foreach (var cage in cages)
            {
                Console.WriteLine($"Cage ID: {cage.Key}, Cage Name: {cage.Value}");
            }

            while (true)
            {
                Console.Write("Enter the Cage ID: ");
                if (int.TryParse(Console.ReadLine(), out int selectedCageId) && cages.ContainsKey(selectedCageId))
                {
                    return selectedCageId;
                }
                Console.WriteLine("Invalid Cage ID. Please try again.");
            }
        }
        private static void writeToFile(Keepers keeper)
        {
            string filePath = "C:\\Users\\kris-\\source\\repos\\zoo3.0\\zoo3.0\\Keepers.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{keeper.ASSIGNEDCAGEID},{keeper.KEEPERID},{keeper.KEEPERNAME},{keeper.KEEPERAGE},{keeper.KEEPERSPECIALTY}");
            } 
            Console.WriteLine("Keeper information saved to file.");
        }  
    }
}

