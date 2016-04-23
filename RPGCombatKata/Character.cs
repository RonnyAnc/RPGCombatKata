using System;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public class Character
    {
        private const int Heals = 100;
        private const int FullLife = 1000;
        public int Life { get; protected set; } = FullLife;
        public int Level { get; } = 1;
        public int Damage { get; }
        public Subject<Character> Enemies = new Subject<Character>();

        public Character(int damage)
        {
            Damage = damage;
            Enemies.Subscribe(e => e.ReceiveDamage(Damage));
        }

        public bool IsAlive()
        {
            return Life != 0;
        }

        public void AttackTo(Character victim)
        {
            if (this == victim) throw new AttackHimselfException();
            Enemies.OnNext(victim);
        }

        private void ReceiveDamage(int damage)
        {
            Life -= damage;
        }

        public void Heal(Character character)
        {
            //if (IsAnEnemy(character)) throw new HealAnEnemyException();
            character.Heal();
        }

        public void Heal()
        {
            if (IsDead()) throw new HealDeadCharacterException();

            if (Life == FullLife) return;
            Life += Heals;
        }

        private bool IsDead()
        {
            return !IsAlive();
        }
    }
}