using System;
using System.Reactive.Linq;
using RPGCombatKata.Characters;

namespace RPGCombatKata
{
    public class House : Attackable
    {
        public House(int life) : base(life) {}

        public override bool IsAttackableBy(Character source)
        {
            return true;
        }
    }
}