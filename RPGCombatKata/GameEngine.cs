using System;
using System.Reactive.Linq;

namespace RPGCombatKata
{
    public class GameEngine
    {
        private readonly RangeCalculator rangeCalculator;
        private readonly GameFactions gameFactions;

        public GameEngine(RangeCalculator rangeCalculator, GameFactions gameFactions)
        {
            this.rangeCalculator = rangeCalculator;
            this.gameFactions = gameFactions;
            EventBus.AsObservable<Attack>()
                .Where(IsInRange)
                .Where(IsNotASelfAttack)
                .Where(CharactersAreEnemies)
                .Subscribe(SendDamage);
        }

        private bool CharactersAreEnemies(Attack attack)
        {
            return !gameFactions.AreInSameFaction(attack.Source, attack.Target);
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
            gameFactions.RegistFaction(faction);
        }
    }
}