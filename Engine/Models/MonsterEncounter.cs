using Engine.Models;

namespace Engine.Models
{
    public class MonsterEncounter
    {
        public int MonsterID { get; }
        public int ChanceOfEncountering { get; set; }

        public MonsterEncounter(int monsterID, int chanceofEncountering)
        {
            MonsterID = monsterID;
            ChanceOfEncountering = chanceofEncountering;
        }
    }
}
