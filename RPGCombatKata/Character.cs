using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public abstract class Character : Attackable
    {
        public List<Faction> Factions { get; } = new List<Faction>();
        private const int FullLife = 1000;
        private const int InitialLevel = 1;
        public decimal Damage { get; protected set; } = 100;
        public abstract int AttackRange { get; }

        protected Character() : base(FullLife, InitialLevel)
        {
            EventBus.AsObservable<Damage>()
                .Where(IAmTheTarget)
                .Subscribe(damage => ReceiveDamage(damage.Value));

            EventBus.AsObservable<LifeIncrement>()
                .Where(IAmTheTarget)
                .Subscribe(increment => Heal(increment.Points));
        }

        private bool IAmTheTarget(LifeIncrement damage)
        {
            return damage.Target == this;
        }

        private bool IAmTheTarget(Damage damage)
        {
            return damage.Target == this;
        }

        public bool IsAlive()
        {
            return Life != 0;
        }

        public override bool IsAttackableBy(Character source)
        {
            return !Factions.Any(f => f.Contains(source));
        }

        public void Heal(int healPoints)
        {
            if (IsDead()) return;
            if (Life == FullLife) return;
            Life += healPoints;
        }

        private bool IsDead()
        {
            return !IsAlive();
        }

        public void JoinTo(Faction faction)
        {
            Factions.Add(faction);
        }

        public void Leave(Faction faction)
        {
            Factions.Remove(faction);
        }
    }
}