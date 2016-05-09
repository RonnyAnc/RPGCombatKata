using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace RPGCombatKata
{
    public class GameEngine
    {
        private readonly RangeCalculator rangeCalculator;
        private readonly List<Faction> factions = new List<Faction>(); 

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
            return factions.All(faction => !faction.AreMembers(attack.Source, attack.Target));
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

        public void RegistFaction(Faction faction)
        {
            factions.Add(faction);
        }
    }
}