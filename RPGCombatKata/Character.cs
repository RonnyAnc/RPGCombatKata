using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public abstract class Character
    {
        private readonly RangeCalculator rangeCalculator;
        private const int Heals = 100;
        private const int FullLife = 1000;
        public int Life { get; protected set; } = FullLife;
        public int Level { get; protected set; } = 1;
        public int Damage { get; protected set; } = 100;
        public Subject<Character> Enemies = new Subject<Character>();
        public Subject<Character> Team = new Subject<Character>();
        protected abstract int AttackRange { get; }

        public Character(RangeCalculator rangeCalculator)
        {
            this.rangeCalculator = rangeCalculator;
            
            var enemiesInRange = Enemies
                .Where(e => rangeCalculator.CalculateDistanceBetween(this, e) <= AttackRange);

            enemiesInRange.Where(e => e != this)
                    .Where(e => e.Level - Level < 5 && Level - e.Level < 5)
                    .Subscribe(e => e.ReceiveDamage(Damage));

            enemiesInRange.Where(e => e != this)
                    .Where(e => e.Level - Level >= 5)
                    .Subscribe(e => e.ReceiveDamage(Damage / 2));

            enemiesInRange.Where(e => e != this)
                .Where(e => Level - e.Level >= 5)
                .Subscribe(e => e.ReceiveDamage(Damage * 2));
        }

        private static bool IsAlive(Character character)
        {
            return character.IsAlive();
        }

        public bool IsAlive()
        {
            return Life != 0;
        }

        public void AttackTo(Character victim)
        {
            Enemies.OnNext(victim);
        }

        private void ReceiveDamage(int damage)
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