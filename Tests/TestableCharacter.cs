using RPGCombatKata;

namespace Tests
{
    internal class TestableCharacter : Character
    {
        public TestableCharacter(RangeCalculator rangeCalculator, int life, int damage = 0, int level = 0) : base(rangeCalculator)
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
    }
}