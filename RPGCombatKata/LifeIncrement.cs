namespace RPGCombatKata
{
    public class LifeIncrement : GameEvent
    {
        public int Points { get; }
        public Character Target { get; }

        public LifeIncrement(int points, Character target)
        {
            Points = points;
            Target = target;
        }
    }
}