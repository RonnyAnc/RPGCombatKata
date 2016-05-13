using System;

namespace RPGCombatKata
{
    public abstract class Attackable
    {
        protected IDisposable HealSubscription;
        public decimal Life { get; protected set; }
        public decimal Level { get; protected set; }

        protected Attackable(decimal life, int level)
        {
            Life = life;
            Level = level;
        }

        public abstract bool IsAttackableBy(Character source);

        protected void ReceiveDamage(decimal damage)
        {
            Life -= damage;
            if (Life == 0) HealSubscription.Dispose();
        }
    }
}