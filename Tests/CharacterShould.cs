using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RPGCombatKata;

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
        public void start_with_full_life_points_as_health()
        {
            ACharacter().Life.Should().Be(FullLife);
            ACharacter().IsAlive().Should().BeTrue();
        }

        [Test]
        public void start_at_level_1()
        {
            ACharacter().Level.Should().Be(1);
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