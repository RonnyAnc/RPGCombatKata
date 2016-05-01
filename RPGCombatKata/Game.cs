using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public class Game
    {
        public Game(RangeCalculator rangeCalculator)
        {
            EventBus.AsObservable<Attack>()
                .Where(a => rangeCalculator
                    .CalculateDistanceBetween(a.Source, a.Target) <= a.Range)
                .Subscribe(SendDamage);
        }

        private static void SendDamage(Attack attack)
        {
            EventBus.Send(new Damage(attack.Damage, attack.Target));
        }
    }
}