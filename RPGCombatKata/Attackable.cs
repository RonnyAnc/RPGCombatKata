using System;
using System.Reactive.Linq;

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

            EventBus.AsObservable<Damage>()
                .Where(damage => IsItMe(damage.Target))
                .Subscribe(damage => ReceiveDamage(damage.Value));
        }

        private bool IsItMe(Attackable character)
        {
            return character == this;
        }

        public abstract bool IsAttackableBy(Character source);

        protected void ReceiveDamage(decimal damage)
        {
            Life -= damage;
            if (Life == 0) HealSubscription.Dispose();
        }
    }
}