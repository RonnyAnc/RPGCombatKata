namespace RPGCombatKata
{
    public class MeleeFigther : Character
    {
        public MeleeFigther(RangeCalculator rangeCalculator) : base(rangeCalculator) {}
        protected override int AttackRange { get; } = 2;
    }
}