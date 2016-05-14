using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RPGCombatKata;
using RPGCombatKata.Characters;
using RPGCombatKata.Events;

namespace Tests
{
    [TestFixture]
    public class CharacterShould
    {
        private const int FullLife = 1000;
        private RangeCalculator rangeCalculator;

        [SetUp]
        public void SetUp()
        {
            rangeCalculator = Substitute.For<RangeCalculator>();
            rangeCalculator.CalculateDistanceBetween(Arg.Any<Character>(), Arg.Any<Character>())
                .Returns(0);
        }

        [Test]
        public void not_be_healed_when_it_has_full_life()
        {
            var character = ACharacterWith(life: FullLife);

            new LifeIncrement(100, character).Raise();

            character.Life.Should().Be(FullLife);
        }

        [Test]
        public void not_be_healed_when_it_is_dead()
        {
            var target = ACharacterWith(life: 100);

            new Damage(100, target).Raise();
            new LifeIncrement(100, target).Raise();

            target.Life.Should().Be(0);
        }

        [Test]
        public void can_be_healed()
        {
            var target = ACharacterWith(life: FullLife);

            new Damage(100, target).Raise();
            new LifeIncrement(100, target).Raise();

            target.Life.Should().Be(FullLife);
        }
    
        private Character ACharacterWith(int level = 1, int life = 1000, int damage = 0)
        {
            return new MeleeFigther(life, level, damage);
        }

        private Character ACharacter()
        {
            return new MeleeFigther(1000, 1);
        }
    }
}