namespace RPGCombatKata
{
    public class Heal : GameEvent
    {
        public Character Healer { get; }
        public Character Target { get; }

        public Heal(Character healer, Character target)
        {
            Healer = healer;
            Target = target;
        }
    }
}