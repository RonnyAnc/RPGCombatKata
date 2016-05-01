using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class RangedFighterShould
    {
        [Test]
        public void attack_when_enemy_is_closer_than_21_meters()
        {
            var rangeCalculator = Substitute.For<RangeCalculator>();
            var rangedFighter = new RangedFigther(rangeCalculator);
            var victim = new TestableCharacter(rangeCalculator, life: 1000);
            rangeCalculator
                .CalculateDistanceBetween(rangedFighter, victim)
                .Returns(20);
            var attack = new Attack(source: rangedFighter, target: victim);

            attack.Raise();

            victim.Life.Should().Be(900);
        }
    }

    public class RangedFigther : Character
    {
        public RangedFigther(RangeCalculator rangeCalculator) : base(rangeCalculator) {}
        protected override int AttackRange { get; } = 20;
    }
}