using Spectre.Console;

namespace AdventureGame
{
    public class Character : IAttacker
    {
        public string Name { get; set; }
        public int Points { get; set; } = 10;
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Coins { get; set; } = 0;

        private int attackPower;
        private int healAmount;

        public bool IsAlive => Health > 0;

        public Character(string name, int health, int attackPower, int damage)
        {
            Name = name;
            Health = health;
            this.attackPower = attackPower;
            Damage = damage;
            Coins = 0;  // Default coins to 0
            Points = 10;  // Default points to 10
            healAmount = health;  // Amount to heal can be adjusted
        }


       

    }
}
