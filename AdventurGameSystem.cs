using System.Text.Json;

namespace AdventureGame
{
    public class AdventurGameSystem
    {
        public GeneriskaClass<Player> playerManager = new GeneriskaClass<Player>();




        // Method to add a player
        public void AddPlayer(MyDB myDB)
        {

            var playerManager = new GeneriskaClass<Player>();

            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {

                playerManager.AddTo(p);

            }
            //Console.WriteLine("Enter Player ID:");
            //int id = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter Player Name:");
            string name = Console.ReadLine()!;

            Console.WriteLine("Enter Player Health:");
            int health = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter Player Damage:");
            int damage = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter Player Points:");
            int points = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter Player Coins:");
            int coins = int.Parse(Console.ReadLine()!);

            Player newPlayer = new(myDB.AllPlayersDatafromAdventureGameData.Count + 1, name, points, health, damage, coins);
            playerManager.AddTo(newPlayer);
            myDB.AllPlayersDatafromAdventureGameData.Add(newPlayer);

            Console.WriteLine("Player added successfully!");


            SaveAllData(myDB);


        }
        // Method to remove a player
        public void RemovePlayer(MyDB myDB)

        {

            var playerManager = new GeneriskaClass<Player>();

            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {

                playerManager.AddTo(p);

            }
            Console.WriteLine("Enter Player ID to remove:");
            int idToDelet = int.Parse(Console.ReadLine()!);

            var player = playerManager.GetByID(idToDelet);
            if (player == null)
            {
                Console.WriteLine("not found");
                return;

            }

            playerManager.RemoveThis(idToDelet);
            myDB.AllPlayersDatafromAdventureGameData = playerManager.GetAll();
            SaveAllData(myDB);
            Console.WriteLine("Player removed successfully!");


        }



        // Method to update a player's information
        public void UpdatePlayer(MyDB myDB)
        {

            var playerManager = new GeneriskaClass<Player>();

            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {

                playerManager.AddTo(p);

            }
            Console.WriteLine("Enter Player ID to update:");
            int id = int.Parse(Console.ReadLine()!);

            Player existingPlayer = playerManager.GetByID(id);
            if (existingPlayer != null)
            {
                Console.WriteLine("Enter new Player Name (Leave blank to keep current):");
                string name = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(name))
                {
                    existingPlayer.Name = name;
                }

                Console.WriteLine("Enter new Health (Leave blank to keep current):");
                string healthInput = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(healthInput))
                {
                    existingPlayer.Health = int.Parse(healthInput);
                }

                Console.WriteLine("Enter new Damage (Leave blank to keep current):");
                string damageInput = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(damageInput))
                {
                    existingPlayer.Damage = int.Parse(damageInput);
                }

                Console.WriteLine("Enter new Points (Leave blank to keep current):");
                string pointsInput = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(pointsInput))
                {
                    existingPlayer.Points = int.Parse(pointsInput);
                }

                Console.WriteLine("Enter new Coins (Leave blank to keep current):");
                string coinsInput = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(coinsInput))
                {
                    existingPlayer.Coins = int.Parse(coinsInput);
                }

                // Update player using the generic class
                playerManager.Updater(myDB.AllPlayersDatafromAdventureGameData, existingPlayer, p => p.Id);
                myDB.AllPlayersDatafromAdventureGameData = playerManager.GetAll();
                SaveAllData(myDB);



                Console.WriteLine("Player updated successfully!");
            }
            else
            {
                Console.WriteLine("Player not found!");
            }

            myDB.AllPlayersDatafromAdventureGameData.Add(existingPlayer);



        }





        // Method to display all players
        public void ShowAllPlayers(MyDB myDB)
        {

            var playerManager = new GeneriskaClass<Player>();

            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {

                playerManager.AddTo(p);

            }
            List<Player> players = playerManager.GetAll();
            if (players.Count == 0)
            {
                Console.WriteLine("No players available.");
            }
            else
            {
                foreach (var player in players)
                {
                    player.DisplayStats();
                }
            }

        }



        // Method to show a player by ID
        public void ShowPlayerById(MyDB myDB)
        {
            var playerManager = new GeneriskaClass<Player>();

            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {

                playerManager.AddTo(p);

            }
            Console.WriteLine("Enter Player ID to view:");
            int id = int.Parse(Console.ReadLine()!);

            Player player = playerManager.GetByID(id);
            if (player != null)
            {
                player.DisplayStats();
            }
            else
            {
                Console.WriteLine("Player not found!");
            }
        }


        public void SaveAllData(MyDB myDB)
        {
            string dataJsonFilePath = "AdventureGameData.json";

            string updatedPlayerDB = JsonSerializer.Serialize(myDB, new JsonSerializerOptions { WriteIndented = true });


            File.WriteAllText(dataJsonFilePath, updatedPlayerDB);

            MirrorChangesToProjectRoot("AdventureGameData.json");

        }

        static void MirrorChangesToProjectRoot(string fileName)
        {
            // Get the path to the output directory
            string outputDir = AppDomain.CurrentDomain.BaseDirectory;

            // Get the path to the project root directory
            string projectRootDir = Path.Combine(outputDir, "../../../");

            // Define paths for the source (output directory) and destination (project root)
            string sourceFilePath = Path.Combine(outputDir, fileName);
            string destFilePath = Path.Combine(projectRootDir, fileName);

            // Copy the file if it exists
            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destFilePath, true); // true to overwrite
                Console.WriteLine($"{fileName} has been mirrored to the project root.");
            }
            else
            {
                Console.WriteLine($"Source file {fileName} not found.");
            }
        }



        public void ShowPlayers()
        {
            var players = playerManager.GetAll();
            if (players.Count == 0)
            {
                Console.WriteLine("No players found.");
                return;
            }

            Console.WriteLine("Players in the game:");
            foreach (var player in players)
            {
                player.DisplayStats();
            }
        }




    }
}
