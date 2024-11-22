using AdventureGame;
using System.Text.Json;

public class Program
{
    public static void Main()
    {
        // Initialize the generics class to manage Player objects
        GeneriskaClass<Player> playerManager = new GeneriskaClass<Player>();

        AdventurGameSystem adventurGameSystem = new AdventurGameSystem();

        string dataJsonFilePath = "AdventureGameData.json";
        string allDataAsJson = File.ReadAllText(dataJsonFilePath);
        MyDB myDB = JsonSerializer.Deserialize<MyDB>(allDataAsJson)!;
        var me = new Program();


        while (true)
        {
            // Display menu to the user
            Console.WriteLine("\n1. Add Player");
            Console.WriteLine("2. Remove Player");
            Console.WriteLine("3. Update Player");
            Console.WriteLine("4. Show All Players");
            Console.WriteLine("5. Show Player by ID");
            Console.WriteLine("6. Play As Player");
            Console.WriteLine("7. Exit");

            // Get user choice
            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    adventurGameSystem.AddPlayer(myDB);
                    break;

                case "2":
                    adventurGameSystem.RemovePlayer(myDB);
                    break;

                case "3":
                    adventurGameSystem.UpdatePlayer(myDB);
                    break;

                case "4":
                    adventurGameSystem.ShowAllPlayers(myDB);
                    break;

                case "5":
                    adventurGameSystem.ShowPlayerById(myDB);
                    break;


                case "6":
                    PlayAsPlayer(myDB);
                    break;

                case "7":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }


    public static void PlayAsPlayer(MyDB myDB) 
    {
        AdventurGameSystem adventurGameSystem = new AdventurGameSystem();

        Console.WriteLine("Enter Player ID:");
       int Id = Convert.ToInt32( Console.ReadLine());
        var playerManager = new GeneriskaClass<Player>();
        foreach (var p in myDB.AllPlayersDatafromAdventureGameData)
        {

            playerManager.AddTo(p);

        }

        Player player = playerManager.GetByID(Id);
        if (player == null) 
        {

            Console.WriteLine("Player not found.");
            return;
        }

        Console.WriteLine($"You are now playing as {player.Name}!");

        while (true)
        {
            Console.WriteLine("\n--- Player Actions ---");
            Console.WriteLine("1. Attack Another Player");
            Console.WriteLine("2. Take Damage (simulate)");
            Console.WriteLine("3. Add a Weapon");
            Console.WriteLine("4. Collect Coins");
            Console.WriteLine("5. View Player Stats");
            Console.WriteLine("6. Exit Player Mode");
            Console.WriteLine("Choose an option:");

            string action = Console.ReadLine()!;

            switch (action)
            {
                case "1": // Attack another player
                    Console.WriteLine("Enter the target Player ID to attack:");
                    int targetId = int.Parse(Console.ReadLine()!);
                    var targetPlayer = playerManager.GetByID(targetId);

                    if (targetPlayer != null)
                    {
                        player.Attack(targetPlayer);
                        adventurGameSystem.SaveAllData(myDB);

                    }
                    else
                    {
                        Console.WriteLine("Target player not found.");
                    }
                    break;

                case "2": // Simulate taking damage
                    Console.WriteLine("Enter the amount of damage:");
                    int damage = int.Parse(Console.ReadLine()!);
                    player.TakeDamage(damage);
                    break;

                case "3": // Add a weapon
                    Console.WriteLine("Enter the name of the weapon to add:");
                    string weapon = Console.ReadLine()!;
                    player.AddWeapon(weapon);
                    break;

                case "4": // Collect coins
                    Console.WriteLine("Enter the number of coins to collect:");
                    int coins = int.Parse(Console.ReadLine()!);
                    player.CollectCoins(coins);
                    break;

                case "5": // Display stats
                    player.DisplayStats();
                    break;

                case "6": // Exit player mode
                    Console.WriteLine($"Exiting player mode for {player.Name}.");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

    }

}
