namespace RPGCombatKata
{
    public class MeleeFigther : Character
    {
        public MeleeFigther(int life, decimal damage, int level = 1): base(life, damage, level){}

        public override int AttackRange { get; } = 2;
    }
}