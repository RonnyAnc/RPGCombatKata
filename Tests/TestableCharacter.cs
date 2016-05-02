using RPGCombatKata;

namespace Tests
{
    internal class TestableCharacter : Character
    {
        public TestableCharacter(decimal life = 0, decimal damage = 0, int level = 0, int range = 0)
        {
            Damage = damage;
            Life = life;
            Level = level;
            AttackRange = range;
        }

        public Character WithLevel(int level)
        {
            Level = level;
            return this;
        }

        public override int AttackRange { get; } = 1000;
    }
}