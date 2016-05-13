using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public abstract class Character : Attackable
    {
        private IDisposable healSubscription;
        public List<string> Factions { get; } = new List<string>();
        private const int FullLife = 1000;
        private const int InitialLevel = 1;
        public decimal Damage { get; protected set; } = 100;
        public abstract int AttackRange { get; }

        protected Character() : base(FullLife, InitialLevel)
        {
            healSubscription = EventBus.AsObservable<LifeIncrement>()
                .Where(heal => IsItMe(heal.Target))
                .Where(_ => HasNotFullLife())
                .Subscribe(increment => Heal(increment.Points));
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