using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public abstract class Character
    {
        private const int Heals = 100;
        private const int FullLife = 1000;
        public decimal Life { get; protected set; } = FullLife;
        public int Level { get; protected set; } = 1;
        public decimal Damage { get; protected set; } = 100;
        public Subject<Character> Enemies = new Subject<Character>();
        protected abstract int AttackRange { get; }

        protected Character(RangeCalculator rangeCalculator)
        {
            var attacksToMe = EventBus.AsObservable<Attack>()
                .Where(a => a.Target == this)
                .Where(a => rangeCalculator
                        .CalculateDistanceBetween(a.Source, this) <= a.Source.AttackRange);

            attacksToMe
                .Where(attack => attack.ThereIsNotImportantLevelDifference())
                .Subscribe(a => ReceiveDamage(a.Damage));

            attacksToMe
                .Where(a => a.IsTheTargetLevelSignificantlyHigher())
                .Subscribe(a => ReceiveDamage(a.Damage / 2));

            attacksToMe
                .Where(a => a.IsTheSourceLevelSignificantlyHigher())
                .Subscribe(a => ReceiveDamage(a.Damage * 2));
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
    }
}