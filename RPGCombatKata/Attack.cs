using System;

namespace RPGCombatKata
{
    public class Attack : GameEvent
    {
        public Character Target { get; set; }
        public Character Source { get; set; }

        public decimal Damage => Source.Damage;

        public Attack(Character source, Character target)
        {
            Source = source;
            Target = target;
        }

        public bool ThereIsNotImportantLevelDifference()
        {
            return Math.Abs(Source.Level - Target.Level) < 5;
        }

        public bool IsTheTargetLevelSignificantlyHigher()
        {
            return Target.Level - Source.Level >= 5;
        }

        public bool IsTheSourceLevelSignificantlyHigher()
        {
            return Source.Level - Target.Level >= 5;
        }
    }
}