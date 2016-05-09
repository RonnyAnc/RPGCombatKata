namespace RPGCombatKata
{
    public class Heal : GameEvent
    {
        private const int DefaultHealPoints = 100;
        public Character Healer { get; }
        public Character Target { get; }
        public int Points => DefaultHealPoints;

        public Heal(Character healer, Character target)
        {
            Healer = healer;
            Target = target;
        }
    }
}