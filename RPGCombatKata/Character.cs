namespace RPGCombatKata
{
    public class Character
    {
        private const int Heals = 100;
        public int Life { get; protected set; } = 1000;
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

        public void Heal(Character character)
        {
            character.Heal();
        }

        private void Heal()
        {
            Life += Heals;
        }
    }
}