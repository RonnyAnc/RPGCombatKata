namespace RPGCombatKata
{
    public class LeaveFactionRequest : GameEvent
    {
        public Character Character { get; }
        public Faction Faction { get; }

        public LeaveFactionRequest(Character character, Faction faction)
        {
            Character = character;
            Faction = faction;
        }
    }
}