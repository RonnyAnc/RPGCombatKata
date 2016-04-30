using System;

namespace RPGCombatKata
{
    public class Attack : GameEvent
    {
        public Character Target { get; set; }
        public Character Source { get; set; }
        public decimal Damage { get; set; }

        public Attack(Character target, decimal damage)
        {
            Target = target;
            Damage = damage;
        }

        public Attack(Character source, Character target)
        {
            Source = source;
            Target = target;
        }
    }
}