using System;
using System.Reactive.Linq;

namespace RPGCombatKata
{
    public class House : Attackable
    {
        public House(int life) : base(life, 1) {}

        public override bool IsAttackableBy(Character source)
        {
            return true;
        }
    }
}