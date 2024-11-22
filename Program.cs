using AdventureGame;
using Spectre.Console;
using System.Text.Json;

using AdventureGame;
using Spectre.Console;
using System.Text.Json;
using Figgle;

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


        var welcomeMessage = FiggleFonts.Standard.Render("Welcome to the Game!");

        // Display the message with Spectre styling
        AnsiConsole.MarkupLine($"[bold red]{welcomeMessage}[/]");

        while (true)
        {
            // Display menu to the user and get the selected choice
            string choice = displayMenu();

            // Handle user choice
            switch (choice)
            {
                case "1. Add Player":
                    adventurGameSystem.AddPlayer(myDB);
                    break;

                case "2. Remove Player":
                    adventurGameSystem.RemovePlayer(myDB);
                    break;

                case "3. Update Player":
                    adventurGameSystem.UpdatePlayer(myDB);
                    break;

                case "4. Show All Players":
                    adventurGameSystem.ViewPlayers(myDB);
                    break;

                case "5. Show Player by ID":
                    adventurGameSystem.ShowAPlayerById(myDB);
                    break;

                case "6. Play As Player":
                    adventurGameSystem.PlayAsPlayer(myDB);
                    break;

                case "7. Exit":
                    return; // Exit the program

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static string displayMenu()
    {
        // Display a menu using Spectre.Console's SelectionPrompt
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Select an option from the Player Management Menu:[/]")  // Title of the menu
                .PageSize(6)  // Number of items to show at once
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")  // Text when more options exist
                .AddChoices(new[]  // Add the menu options
                {
                    "1. Add Player",
                    "2. Remove Player",
                    "3. Update Player",
                    "4. Show All Players",
                    "5. Show Player by ID",
                    "6. Play As Player",
                    "7. Exit"
                })
        );
    }
}
