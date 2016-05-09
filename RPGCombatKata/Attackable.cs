namespace RPGCombatKata
{
    public abstract class Attackable
    {
        public decimal Life { get; protected set; }
        public decimal Level { get; protected set; }

        protected Attackable(decimal life, int level)
        {
            Life = life;
            Level = level;
        }
    }
}