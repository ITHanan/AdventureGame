using System.Text.Json;
using Figgle;
using Spectre.Console;
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

            // Add existing players to the manager
            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {
                playerManager.AddTo(p);
            }

            // Collect player details using Spectre's prompts
            string name = AnsiConsole.Ask<string>("[yellow]Enter Player Name:[/]");
            int health = AnsiConsole.Ask<int>("[yellow]Enter Player Health:[/]");
            int damage = AnsiConsole.Ask<int>("[yellow]Enter Player Damage:[/]");
            int points = AnsiConsole.Ask<int>("[yellow]Enter Player Points:[/]");
            int coins = AnsiConsole.Ask<int>("[yellow]Enter Player Coins:[/]");

            // Create a new player with the input data
            Player newPlayer = new Player(
                myDB.AllPlayersDatafromAdventureGameData.Count + 1,
                name,
                points,
                health,
                damage,
                coins
            );

            // Add the new player to the manager and the database
            playerManager.AddTo(newPlayer);
            myDB.AllPlayersDatafromAdventureGameData.Add(newPlayer);

            // Display confirmation using a table
            var table = new Table();
            table.AddColumn("[bold yellow]Attribute[/]");
            table.AddColumn("[bold cyan]Value[/]");

            table.AddRow("Player Name", newPlayer.Name);
            table.AddRow("Health", newPlayer.Health.ToString());
            table.AddRow("Damage", newPlayer.Damage.ToString());
            table.AddRow("Points", newPlayer.Points.ToString());
            table.AddRow("Coins", newPlayer.Coins.ToString());

            AnsiConsole.MarkupLine("[bold green]Player added successfully![/]");
            AnsiConsole.Render(table);

            // Save all data (method is assumed to exist)
            SaveAllData(myDB);

        }


        // Method to remove a player
        public void RemovePlayer(MyDB myDB)

        {

            var playerManager = new GeneriskaClass<Player>();

            // Add existing players to the manager
            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {
                playerManager.AddTo(p);
            }

            ViewPlayers(myDB);

            // Prompt user to enter Player ID to remove
            int idToDelete = AnsiConsole.Ask<int>("[yellow]Enter Player ID to remove:[/]");

            // Fetch the player by ID
            var player = playerManager.GetByID(idToDelete);

            if (player == null)
            {
                // If player is not found, display an error message
                AnsiConsole.MarkupLine("[bold red]Player not found![/]");
                return;
            }

            // Remove the player from the manager
            playerManager.RemoveThis(idToDelete);

            // Update the database
            myDB.AllPlayersDatafromAdventureGameData = playerManager.GetAll();

            // Save the updated data
            SaveAllData(myDB);

            // Display success confirmation in a table
            var table = new Table();
            table.AddColumn("[bold yellow]Attribute[/]");
            table.AddColumn("[bold cyan]Value[/]");

            table.AddRow("Player Name", player.Name);
            table.AddRow("Player ID", player.Id.ToString());

            AnsiConsole.MarkupLine("[bold green]Player removed successfully![/]");
            AnsiConsole.Render(table);

        }




        // Method to update a player's information
        public void UpdatePlayer(MyDB myDB)
        {

            var playerManager = new GeneriskaClass<Player>();

            // Add existing players to the player manager
            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {
                playerManager.AddTo(p);
            }

            ViewPlayers(myDB);


            // Prompt user to enter the Player ID to update
            int id = AnsiConsole.Ask<int>("[yellow]Enter Player ID to update:[/]");

            // Fetch the player by ID
            Player existingPlayer = playerManager.GetByID(id);

            if (existingPlayer != null)
            {
                // Prompt for each player attribute with option to leave blank to keep current value
                string name = AnsiConsole.Ask<string>("[yellow]Enter new Player Name (Leave blank to keep current):[/]");
                if (!string.IsNullOrEmpty(name))
                {
                    existingPlayer.Name = name;
                }

                string healthInput = AnsiConsole.Ask<string>("[yellow]Enter new Health (Leave blank to keep current):[/]");
                if (!string.IsNullOrEmpty(healthInput))
                {
                    existingPlayer.Health = int.Parse(healthInput);
                }

                string damageInput = AnsiConsole.Ask<string>("[yellow]Enter new Damage (Leave blank to keep current):[/]");
                if (!string.IsNullOrEmpty(damageInput))
                {
                    existingPlayer.Damage = int.Parse(damageInput);
                }

                string pointsInput = AnsiConsole.Ask<string>("[yellow]Enter new Points (Leave blank to keep current):[/]");
                if (!string.IsNullOrEmpty(pointsInput))
                {
                    existingPlayer.Points = int.Parse(pointsInput);
                }

                string coinsInput = AnsiConsole.Ask<string>("[yellow]Enter new Coins (Leave blank to keep current):[/]");
                if (!string.IsNullOrEmpty(coinsInput))
                {
                    existingPlayer.Coins = int.Parse(coinsInput);
                }

                // Update the player in the database
                playerManager.Updater(myDB.AllPlayersDatafromAdventureGameData, existingPlayer, p => p.Id);
                myDB.AllPlayersDatafromAdventureGameData = playerManager.GetAll();

                // Save all changes to data
                SaveAllData(myDB);

                // Show the updated player data in a table
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("[yellow]Attribute[/]")
                    .AddColumn("[yellow]Updated Value[/]");

                table.AddRow("Name", existingPlayer.Name);
                table.AddRow("Health", existingPlayer.Health.ToString());
                table.AddRow("Damage", existingPlayer.Damage.ToString());
                table.AddRow("Points", existingPlayer.Points.ToString());
                table.AddRow("Coins", existingPlayer.Coins.ToString());
                table.AddRow("Weapons", string.Join(", ", existingPlayer.Weapons));

                AnsiConsole.MarkupLine("[bold green]Player updated successfully![/]");
                AnsiConsole.Write(table);
            }
            else
            {
                // If the player is not found
                AnsiConsole.MarkupLine("[bold red]Player not found![/]");
            }



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

            // Use Figgle to display a title banner
            AnsiConsole.Markup("[bold green]" + FiggleFonts.Standard.Render("Adventure Game Players") + "[/]");

            if (players.Count == 0)
            {
                AnsiConsole.Markup("[bold red]No players available.[/]");
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
        public void ShowAPlayerById(MyDB myDB)
        {
            // Create the player manager and add players to it
            var playerManager = new GeneriskaClass<Player>();
            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {
                playerManager.AddTo(p);
            }

            // Prompt user for the Player ID with Spectre.Console
            int id = AnsiConsole.Ask<int>("[yellow]Enter Player ID to view:[/]");

            // Fetch the player by ID
            Player player = playerManager.GetByID(id);

            // Check if player exists and display stats in a table, or show a message if not found
            if (player != null)
            {
                // Create a table to display the player's details
                var table = new Table();
                table.AddColumn("[bold yellow]Attribute[/]");
                table.AddColumn("[bold cyan]Value[/]");

                // Add rows to the table for each player attribute
                table.AddRow("Player", player.Name);
                table.AddRow("ID", player.Id.ToString());
                table.AddRow("Health", player.Health.ToString());
                table.AddRow("Points", player.Points.ToString());
                table.AddRow("Damage", player.Damage.ToString());
                table.AddRow("Coins", player.Coins.ToString());
                table.AddRow("Weapons", string.Join(", ", player.Weapons));

                // Render the table
                AnsiConsole.Render(table);
            }
            else
            {
                // If the player isn't found, display an error message
                AnsiConsole.MarkupLine("[bold red]Player not found![/]");
            }
        }



        public void ViewPlayers(MyDB myDB)
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]Player ID[/]")
                .AddColumn("[yellow]Name[/]")
                .AddColumn("[yellow]Health[/]")
                .AddColumn("[yellow]Points[/]")
                .AddColumn("[yellow]Damage[/]")
                .AddColumn("[yellow]Coins[/]")
                .AddColumn("[yellow]Weapons[/]");

            // Add player data to the table
            foreach (var player in myDB.AllPlayersDatafromAdventureGameData)
            {
                table.AddRow(
                    player.Id.ToString(),
                    player.Name,
                    player.Health.ToString(),
                    player.Points.ToString(),
                    player.Damage.ToString(),
                    player.Coins.ToString(),
                    string.Join(", ", player.Weapons)
                );
            }

            // Display the table with player information
            AnsiConsole.Write(table);
        }

        public void PlayAsPlayer(MyDB myDB)
        {
            // Display the list of players (assuming ViewPlayers is another method you have)
            ViewPlayers(myDB);

            // Ask the user to input the player ID with enhanced styling
            AnsiConsole.MarkupLine("[yellow]Enter Player ID to play as:[/]");
            int Id = Convert.ToInt32(Console.ReadLine());

            var playerManager = new GeneriskaClass<Player>();

            // Adding players to the player manager
            foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
            {
                playerManager.AddTo(p);
            }

            // Fetch the player by ID
            Player player = playerManager.GetByID(Id);
            if (player == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Player not found![/]");
                return;
            }

            // Notify the user that they are now playing as the selected player
            AnsiConsole.MarkupLine($"[bold green]You are now playing as {player.Name}![/]");

            // Main loop for player actions
            while (true)
            {
                // Display action options in a well-styled table
                var actionTable = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("[yellow]Action[/]");

                actionTable.AddRow("[bold]1.[/] Attack Another Player");
                actionTable.AddRow("[bold]2.[/] Take Damage (simulate)");
                actionTable.AddRow("[bold]3.[/] Add a Weapon");
                actionTable.AddRow("[bold]4.[/] Collect Coins");
                actionTable.AddRow("[bold]5.[/] View Player Stats");
                actionTable.AddRow("[bold]6.[/] Exit Player Mode");

                AnsiConsole.Write(actionTable);

                AnsiConsole.MarkupLine("[yellow]Please select an action (1-6):[/]");
                string action = Console.ReadLine()!;

                // Switch case for different actions with clear feedback
                switch (action)
                {
                    case "1": // Attack another player
                        AnsiConsole.MarkupLine("[yellow]Enter the target Player ID to attack:[/]");
                        int targetId = int.Parse(Console.ReadLine()!);
                        var targetPlayer = playerManager.GetByID(targetId);

                        if (targetPlayer != null)
                        {
                            player.Attack(targetPlayer);
                            SaveAllData(myDB);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Target player not found![/]");
                        }
                        break;

                    case "2": // Simulate taking damage
                        AnsiConsole.MarkupLine("[yellow]Enter the amount of damage to take:[/]");
                        int damage = int.Parse(Console.ReadLine()!);
                        player.TakeDamage(damage);
                        SaveAllData(myDB);
                        break;

                    case "3": // Add a weapon
                        AnsiConsole.MarkupLine("[yellow]Enter the name of the weapon to add:[/]");
                        string weapon = Console.ReadLine()!;
                        player.AddWeapon(weapon);
                        SaveAllData(myDB);
                        break;

                    case "4": // Collect coins
                        AnsiConsole.MarkupLine("[yellow]Enter the number of coins to collect:[/]");
                        int coins = int.Parse(Console.ReadLine()!);
                        player.CollectCoins(coins);
                        SaveAllData(myDB);
                        break;

                    case "5": // Display stats
                        player.DisplayStats();
                        break;

                    case "6": // Exit player mode
                        AnsiConsole.MarkupLine($"[bold red]Exiting player mode for {player.Name}. Goodbye![/]");
                        return;

                    default:
                        AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                        break;
                }
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
