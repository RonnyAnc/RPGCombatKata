using RPGCombatKata;

namespace Tests
{
    internal class TestableCharacter : Character
    {
        public TestableCharacter(int life, decimal damage = 0, int level = 0)
        {
            Damage = damage;
            Life = life;
            Level = level;
        }

        public Character WithLevel(int level)
        {
            Level = level;
            return this;
        }

        public override int AttackRange { get; } = 1000;
    }
}