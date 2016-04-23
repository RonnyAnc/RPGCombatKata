using RPGCombatKata;

namespace Tests
{
    internal class DamagedCharacter : Character
    {
        public DamagedCharacter(int life) : base(0)
        {
            Life = life;
        }
    }
}