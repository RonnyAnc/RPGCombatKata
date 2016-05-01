﻿using System;

namespace RPGCombatKata
{
    public class Attack : GameEvent
    {
        public Character Target { get; set; }
        public Character Source { get; set; }

        public decimal Damage {
            get {
                if(IsTheSourceLevelSignificantlyHigher()) return Source.Damage * 2;
                if(IsTheTargetLevelSignificantlyHigher()) return Source.Damage / 2;
                return Source.Damage;
            }
        }

        public Attack(Character source, Character target)
        {
            Source = source;
            Target = target;
        }

        private bool IsTheTargetLevelSignificantlyHigher()
        {
            return Target.Level - Source.Level >= 5;
        }

        public bool IsTheSourceLevelSignificantlyHigher()
        {
            return Source.Level - Target.Level >= 5;
        }
    }
}