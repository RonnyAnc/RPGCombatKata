using System;
using System.Reactive.Linq;

namespace RPGCombatKata
{
    public class House : Attackable
    {
        public House(int life) : base(life, 1)
        {
            EventBus.AsObservable<Damage>()
                .Where(d => d.Target == this)
                .Subscribe(d => ReceiveDamage(d.Value));
        }

        public override bool IsAttackableBy(Character source)
        {
            return true;
        }
    }
}