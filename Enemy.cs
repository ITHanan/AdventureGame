

namespace AdventureGame
{
    public class Enemy : IIdentifiable
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Level { get; set; }
        public List<string> Abilities { get; set; }

        public Enemy(int id, string name, int health, int damage, int level, List<string> abilities)
        {
            Id = id;
            Name = name;
            Health = health;
            Damage = damage;
            Level = level;
            Abilities = abilities;
        }
    }

}
