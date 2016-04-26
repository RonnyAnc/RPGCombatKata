using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class MeleeFighterShould
    {
        [Test]
        public void not_attack_when_enemy_is_further_than_2_meters()
        {
            var rangeCalculator = Substitute.For<RangeCalculator>();
            var meleeFighter = new MeleeFigther(rangeCalculator);
            var victim = new TestableCharacter(rangeCalculator, life: 1000);
            rangeCalculator
                .CalculateDistanceBetween(meleeFighter, victim)
                .Returns(3);

            meleeFighter.AttackTo(victim);

            victim.Life.Should().Be(1000);
        }
    }

    public class MeleeFigther : Character
    {
        public MeleeFigther(RangeCalculator rangeCalculator) : base(rangeCalculator) {}
    }
}