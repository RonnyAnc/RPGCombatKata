namespace RPGCombatKata.Events
{
    public class Damage : GameEvent
    {
        public decimal Value { get; }
        public Attackable Target { get; }

        public Damage(decimal value, Attackable target)
        {
            Value = value;
            Target = target;
        }
    }
}