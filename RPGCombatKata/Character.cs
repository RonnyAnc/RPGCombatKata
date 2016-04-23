namespace RPGCombatKata
{
    public class Character
    {
        public int Life { get; private set; } = 1000;
        public int Level { get; } = 1;

        public bool IsAlive()
        {
            return true;
        }

        public void AttackTo(Character victim)
        {
            victim.ReceiveDamage(100);
        }

        private void ReceiveDamage(int damage)
        {
            Life -= damage;
        }
    }
}