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
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0; // Ensure health doesn't go below 0
                Console.WriteLine($"{Name} has been defeated!");
            }
            else
            {
                Console.WriteLine($"{Name} took {damage} damage, remaining health: {Health}");
            }
        }

        // Method to attack another player
        public void Attack(Player target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} for {Damage} damage!");
            target.TakeDamage(Damage);
        }

        // Method to add a weapon to the player's arsenal
        public void AddWeapon(string weapon)
        {
            Weapons.Add(weapon);
            Console.WriteLine($"{Name} obtained a new weapon: {weapon}");
        }

        // Method to collect coins
        public void CollectCoins(int amount)
        {
            Coins += amount;
            Console.WriteLine($"{Name} collected {amount} coins. Total coins: {Coins}");
        }

        // Display player details
        public void DisplayStats()
        {
            Console.WriteLine($"Player: {Name} (ID: {Id})");
            Console.WriteLine($"Health: {Health}, Points: {Points}, Damage: {Damage}, Coins: {Coins}");
            Console.WriteLine($"Weapons: {string.Join(", ", Weapons)}");
        }

       


    }
}
