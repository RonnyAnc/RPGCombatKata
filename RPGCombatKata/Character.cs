using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public abstract class Character
    {
        public string FactionName { get; private set; }
        private const int Heals = 100;
        private const int FullLife = 1000;
        public decimal Life { get; protected set; } = FullLife;
        public int Level { get; protected set; } = 1;
        public decimal Damage { get; protected set; } = 100;
        public abstract int AttackRange { get; }

        protected Character()
        {
            EventBus.AsObservable<Damage>()
                .Where(IAmTheTarget())
                .Subscribe(damage => ReceiveDamage(damage.Value));
        }

        private Func<Damage, bool> IAmTheTarget()
        {
            return damage => damage.Target == this;
        }

        public bool IsAlive()
        {
            return Life != 0;
        }

        private void ReceiveDamage(decimal damage)
        {
            Life -= damage;
        }

        public void Heal()
        {
            if (IsDead()) return;
            if (Life == FullLife) return;
            Life += Heals;
        }

        private bool IsDead()
        {
            return !IsAlive();
        }

        public void JoinToFaction(string factionName)
        {
            FactionName = factionName;
        }
    }
}