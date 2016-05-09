using System;
using System.Collections.Generic;
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
            foreach (var faction in factions)
            {
                if (faction.Contains(attack.Source) && faction.Contains(attack.Target)) return false;
            }
            return true;
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