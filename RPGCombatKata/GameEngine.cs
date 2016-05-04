using System;
using System.Reactive.Linq;

namespace RPGCombatKata
{
    public class GameEngine
    {
        private readonly RangeCalculator rangeCalculator;

        public GameEngine(RangeCalculator rangeCalculator)
        {
            this.rangeCalculator = rangeCalculator;
            EventBus.AsObservable<Attack>()
                .Where(IsInRange)
                .Where(IsNotASelfAttack)
                .Where(CharactersAreEnemies)
                .Subscribe(SendDamage);
        }

        private bool CharactersAreEnemies(Attack attack)
        {
            if (string.IsNullOrEmpty(attack.Source.FactionName)) return true;
            return attack.Source.IsAnEnemyOf(attack.Target);
        }

        private bool IsInRange(Attack attack)
        {
            var distance = ObtainDistanceBetween(attack.Source, attack.Target);
            return attack.IsInRange(distance);
        }

        private int ObtainDistanceBetween(Character attacker, Character victim)
        {
            return rangeCalculator
                .CalculateDistanceBetween(attacker, victim);
        }

        private static bool IsNotASelfAttack(Attack a)
        {
            return a.Source != a.Target;
        }

        private static void SendDamage(Attack attack)
        {
            EventBus.Send(new Damage(attack.Damage, attack.Target));
        }
    }
}