namespace RPGCombatKata
{
    public class Character
    {
        public int Life { get; private set; } = 1000;
        public int Level { get; } = 1;
        public int Damage { get; }

        public Character(int damage)
        {
            Damage = damage;
        }

        public bool IsAlive()
        {
            return Life != 0;
        }

        public void AttackTo(Character victim)
        {
            victim.ReceiveDamage(Damage);
        }

        private void ReceiveDamage(int damage)
        {
            Life -= damage;
        }
    }
}