using System;
using System.Reactive.Linq;
using RPGCombatKata.Characters;
using RPGCombatKata.Events;

namespace RPGCombatKata
{
    public abstract class Attackable
    {
        private const int InitialLevel = 1;
        public decimal Life { get; protected set; }
        public decimal Level { get; protected set; }

        protected Attackable(decimal life)
        {
            Life = life;
            Level = InitialLevel;

            EventBus.AsObservable<Damage>()
                .Where(damage => IsItMe(damage.Target))
                .Subscribe(damage => ReceiveDamage(damage.Value));
        }

        protected bool IsItMe(Attackable character)
        {
            return character == this;
        }

        public abstract bool IsAttackableBy(Character source);

        protected void ReceiveDamage(decimal damage)
        {
            Life -= damage;
            if (NoLife()) Unsubscribe();
        }

        protected bool NoLife()
        {
            return Life <= 0;
        }

        protected virtual void Unsubscribe() {}
    }
}