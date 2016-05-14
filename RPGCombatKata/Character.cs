using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public abstract class Character : Attackable
    {
        private const int FullLife = 1000;

        private readonly IDisposable healSubscription;
        public List<string> Factions { get; } = new List<string>();
        public decimal Damage { get; protected set; }
        public abstract int AttackRange { get; }

        protected Character(decimal initialLife, decimal damage, int initialLevel) : base(initialLife)
        {
            Damage = damage;
            Level = initialLevel;

            healSubscription = EventBus.AsObservable<LifeIncrement>()
                .Where(AmITheTarget)
                .Where(_ => HasNotFullLife())
                .Subscribe(increment => Heal(increment.Points));
        }

        private bool AmITheTarget(LifeIncrement lifeIncrement)
        {
            return lifeIncrement.Target == this;
        }

        protected override void Unsubscribe()
        {
            healSubscription.Dispose();
        }

        private bool HasNotFullLife()
        {
            return Life < FullLife;
        }

        public bool IsAlive()
        {
            return Life != 0;
        }

        public override bool IsAttackableBy(Character source)
        {
            return !Factions.Any(f => Factions.Contains(f));
        }

        public void Heal(int healPoints)
        {
            Life += healPoints;
        }

        public void JoinTo(string faction)
        {
            Factions.Add(faction);
        }

        public void Leave(string faction)
        {
            Factions.Remove(faction);
        }
    }
}