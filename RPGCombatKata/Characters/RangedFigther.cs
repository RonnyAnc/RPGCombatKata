namespace RPGCombatKata.Characters
{
    public class RangedFigther : Character
    {
        public RangedFigther(int life, decimal damage, int level = 1) : base(life, damage, level) {}

        public override int AttackRange { get; } = 20;
    }
}