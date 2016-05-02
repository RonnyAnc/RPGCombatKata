using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public class Game
    {
        private readonly RangeCalculator rangeCalculator;

        public Game(RangeCalculator rangeCalculator)
        {
            this.rangeCalculator = rangeCalculator;
            EventBus.AsObservable<Attack>()
                .Where(IsInRange)
                .Where(IsNotASelfAttack)
                .Subscribe(SendDamage);
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