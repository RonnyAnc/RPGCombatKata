namespace RPGCombatKata
{
    public class Damage
    {
        public decimal Value { get; }
        public Character Target { get; }

        public Damage(decimal value, Character target)
        {
            Value = value;
            Target = target;
        }
    }
}