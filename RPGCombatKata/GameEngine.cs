using System;
using System.Linq;
using System.Reactive.Linq;
using RPGCombatKata.Characters;
using RPGCombatKata.Events;

namespace RPGCombatKata
{
    public class GameEngine
    {
        private readonly RangeCalculator rangeCalculator;

        public GameEngine(RangeCalculator rangeCalculator)
        {
            this.rangeCalculator = rangeCalculator;
            SubscribeToAttacks();
            SubscribeToHeals();
        }

        private void SubscribeToHeals()
        {
            EventBus.AsObservable<Heal>()
                .Where(CharactersArePartners)
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
            return heal.ArePartnersTheInvolvedCharacters();
        }

        private static void SendLifeIncrement(Heal heal)
        {
            new LifeIncrement(points: heal.Points, target: heal.Target).Raise();
        }

        private static bool CharactersAreEnemies(Attack attack)
        {
            return attack.CanSourceAttackToTarget();
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
    }
}