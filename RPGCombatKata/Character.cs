using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public abstract class Character : Attackable
    {
        public List<string> Factions { get; } = new List<string>();
        private const int FullLife = 1000;
        private const int InitialLevel = 1;
        public decimal Damage { get; protected set; } = 100;
        public abstract int AttackRange { get; }

        protected Character() : base(FullLife, InitialLevel)
        {
            EventBus.AsObservable<Damage>()
                .Where((damage) => AmI(damage.Target))
                .Subscribe(damage => ReceiveDamage(damage.Value));

            EventBus.AsObservable<LifeIncrement>()
                .Where((heal) => AmI(heal.Target))
                .Subscribe(increment => Heal(increment.Points));
        }

        private bool AmI(Attackable character)
        {
            return character == this;
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
            if (IsDead()) return;
            if (Life == FullLife) return;
            Life += healPoints;
        }

        private bool IsDead()
        {
            return !IsAlive();
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