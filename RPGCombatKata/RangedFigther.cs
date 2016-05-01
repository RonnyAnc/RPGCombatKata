namespace RPGCombatKata
{
    public class RangedFigther : Character
    {
        public RangedFigther(RangeCalculator rangeCalculator) : base(rangeCalculator) {}
        protected override int AttackRange { get; } = 20;
    }
}