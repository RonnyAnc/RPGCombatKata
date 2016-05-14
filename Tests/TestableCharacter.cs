using RPGCombatKata;
using RPGCombatKata.Characters;

namespace Tests
{
    internal class TestableCharacter : Character
    {
        public TestableCharacter(decimal life = 0, decimal damage = 0, int level = 0, int range = 0) : base(life, damage, level)
        {
            AttackRange = range;
        }

        public override int AttackRange { get; } = 1000;
    }
}