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

            var selfHeals = EventBus.AsObservable<Heal>().Where(IsASelfHeal);
            var partnerHeals = EventBus.AsObservable<Heal>().Where(CharactersArePartners);
            selfHeals
                .Merge(partnerHeals)
                .Subscribe(h => h.Target.Heal());
        }

        public bool IsASelfHeal(Heal heal)
        {
            return heal.Target == heal.Healer;
        }

        public bool CharactersArePartners(Heal heal)
        {
            return gameFactions.AreInSameFaction(heal.Target, heal.Healer);
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