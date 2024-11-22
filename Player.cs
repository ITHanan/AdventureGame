
using Figgle;
using Spectre.Console;

namespace AdventureGame
{
    public class Player : IIdentifiable 
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public int Points { get; set; } = 10;
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Coins { get; set; } = 0;

        public List<string> Weapons { get; set; }

        public Player(int id, string name, int points, int health, int damage, int coins)
        {
            Id = id;
            Name = name;
            Points = points;
            Health = health;
            Damage = damage;
            Coins = coins;
        }


        // Method to take damage from an attack
        public void TakeDamage(int damage)
        {
            // Reduce health and check for defeat
            Health -= damage;

            if (Health <= 0)
            {
                Health = 0; // Ensure health doesn't go below 0

                // Display defeat message with Figgle ASCII art and Spectre styling
                var defeatMessage = FiggleFonts.Standard.Render($"{Name} has been defeated!");
                AnsiConsole.MarkupLine($"[bold red]{defeatMessage}[/]");
            }
            else
            {
                // Display remaining health after taking damage
                AnsiConsole.MarkupLine($"[yellow]{Name} took [bold red]{damage}[/] damage, remaining health: [bold green]{Health}[/][/]");
            }
        }

        // Method to attack another player
        public void Attack(Player target)
        {
            // Print attack action
            // ASCII Art for attack message
            var attackMessage = FiggleFonts.Standard.Render($"{Name} attacks {target.Name}!");

            // Display the attack message in bold green with Figgle ASCII art and Spectre styling
            AnsiConsole.MarkupLine($"[bold green]{attackMessage}[/]");
            AnsiConsole.MarkupLine($"[yellow]Damage:[/] [bold red]{Damage}[/] [yellow]for {target.Name}![/]");

            // Call target's TakeDamage method
            target.TakeDamage(Damage);
        }


        // Method to add a weapon to the player's arsenal
        public void AddWeapon(string weapon)
        {
            // Add weapon to player's arsenal
            Weapons.Add(weapon);
            Console.WriteLine($"[bold blue]{Name} obtained a new weapon: {weapon}[/]");
        }


        // Method to collect coins
        public void CollectCoins(int amount)
        {
            // Increase coin count and display
            Coins += amount;
            Console.WriteLine($"[bold yellow]{Name} collected {amount} coins. Total coins: {Coins}[/]");
        }


        // Display player details
        public void DisplayStats()
        {
            // Use Spectre.Console to style the output with a table
            var table = new Table();
            table.AddColumn("[bold yellow]Attribute[/]");
            table.AddColumn("[bold green]Value[/]");

            // Add rows for player details
            table.AddRow("Player Name", Name);
            table.AddRow("ID", Id.ToString());
            table.AddRow("Health", Health.ToString());
            table.AddRow("Points", Points.ToString());
            table.AddRow("Damage", Damage.ToString());
            table.AddRow("Coins", Coins.ToString());
            table.AddRow("Weapons", string.Join(", ", Weapons));

            // Render the table
            AnsiConsole.Render(table);
        }





    }
}
