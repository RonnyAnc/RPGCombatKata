using RPGCombatKata.Characters;

namespace RPGCombatKata.Events
{
    public class Attack : GameEvent
    {
        private const int SignificantLevelDifference = 5;
        public Attackable Target { get; set; }
        public Character Source { get; set; }

        public decimal Damage {
            get {
                if(IsTheSourceLevelSignificantlyHigher()) return Source.Damage * 2;
                if(IsTheTargetLevelSignificantlyHigher()) return Source.Damage / 2;
                return Source.Damage;
            }
        }

        public int Range => Source.AttackRange;

        public Attack(Character source, Attackable target)
        {
            Source = source;
            Target = target;
        }

        private bool IsTheTargetLevelSignificantlyHigher()
        {
            return Target.Level - Source.Level >= SignificantLevelDifference;
        }

        public bool IsTheSourceLevelSignificantlyHigher()
        {
            return Source.Level - Target.Level >= SignificantLevelDifference;
        }

        public bool IsInRange(int distance)
        {
            return distance <= Range;
        }

        public bool CanSourceAttackToTarget()
        {
            return Target.IsAttackableBy(Source);
        }
    }
}