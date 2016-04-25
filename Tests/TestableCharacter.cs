using RPGCombatKata;

namespace Tests
{
    internal class TestableCharacter : Character
    {
        public TestableCharacter(int life, int damage = 0, int level = 0) : base(damage)
        {
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