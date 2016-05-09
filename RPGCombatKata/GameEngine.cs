using System;
using System.Linq;
using System.Reactive.Linq;

namespace RPGCombatKata
{
    public class GameEngine
    {
        private const int SignificantLevelDifference = 5;
        private readonly RangeCalculator rangeCalculator;
        private readonly GameFactions gameFactions;

        public GameEngine(RangeCalculator rangeCalculator, GameFactions gameFactions)
        {
            this.rangeCalculator = rangeCalculator;
            this.gameFactions = gameFactions;
            SubscribeToAttacks();
            SubscribeToHeals();
        }

        private void SubscribeToHeals()
        {
            var selfHeals = EventBus.AsObservable<Heal>().Where(IsASelfHeal);
            var partnerHeals = EventBus.AsObservable<Heal>().Where(CharactersArePartners);
            selfHeals
                .Merge(partnerHeals)
                .Subscribe(SendLifeIncrement);
        }

        private void SubscribeToAttacks()
        {
            EventBus.AsObservable<Attack>()
                .Where(IsInRange)
                .Where(IsNotASelfAttack)
                .Where(CharactersAreEnemies)
                .Subscribe(SendDamage);
        }
        
        public bool IsASelfHeal(Heal heal)
        {
            return heal.Target == heal.Healer;
        }

        public bool CharactersArePartners(Heal heal)
        {
            return gameFactions.AreInSameFaction(heal.Target, heal.Healer);
        }

        private static void SendLifeIncrement(Heal heal)
        {
            new LifeIncrement(points: heal.Points, target: heal.Target).Raise();
        }

        private bool CharactersAreEnemies(Attack attack)
        {
            if (!(attack.Target is Character)) return false;
            return !attack.Source.Factions.Any(f => f.Contains((Character)attack.Target));
        }

        private bool IsInRange(Attack attack)
        {
            var distance = ObtainDistanceBetween(attack.Source, attack.Target);
            return attack.IsInRange(distance);
        }

        private int ObtainDistanceBetween(Character attacker, Attackable victim)
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

    internal class LifeIncrement : GameEvent
    {
        public int Points { get; }
        public Character Target { get; }

        public LifeIncrement(int points, Character target)
        {
            Points = points;
            Target = target;
        }
    }
}