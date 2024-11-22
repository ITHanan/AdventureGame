

using System;
using System.Text.Json.Serialization;

namespace AdventureGame
{
    public class MyDB
    {
        [JsonPropertyName("Players")]
        public List<Player> AllPlayersDatafromAdventureGameData { get; set; } = new List<Player>();

        [JsonPropertyName("Enemy")]
        public List<Enemy> AllEnemyDatafromAdventureGameData { get; set; } = new List<Enemy>();

    }
}
