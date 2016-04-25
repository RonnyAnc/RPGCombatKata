using System;
using System.Reactive.Linq;
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
        public Subject<Character> Team = new Subject<Character>();

        public Character(int damage)
        {
            Damage = damage;
            Enemies.Where(e => e != this)
                    .Subscribe(e => e.ReceiveDamage(Damage));
            Team.Where(IsNotAnEnemy)
                    .Where(IsAlive)
                    .Subscribe(p => p.Heal());
        }

        private static bool IsAlive(Character character)
        {
            return character.IsAlive();
        }

        private static bool IsNotAnEnemy(Character c)
        {
            return !(c is Enemy);
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

        public void Heal(Character character)
        {
            Team.OnNext(character);
        }

        public void Heal()
        {
            if (Life == FullLife) return;
            Life += Heals;
        }

        private bool IsDead()
        {
            return !IsAlive();
        }
    }

    public class Enemy : Character
    {
        public Enemy(int damage) : base(damage) {}
    }
}