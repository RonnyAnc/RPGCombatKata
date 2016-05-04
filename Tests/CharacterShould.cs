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

            character.Heal();

            character.Life.Should().Be(FullLife);
        }

        [Test]
        public void not_be_healed_when_it_is_dead()
        {
            var deadCharacter = ACharacterWith(life: 0);

            deadCharacter.Heal();

            deadCharacter.Life.Should().Be(0);
        }

        [Test]
        public void be_able_to_heal_himself()
        {
            var damagedCharacter = ACharacterWith(life: 500);

            damagedCharacter.Heal();

            damagedCharacter.Life.Should().Be(600);
        }
        
    
        private Character ACharacterWith(int level = 1, int life = 1000, int damage = 0)
        {
            return new TestableCharacter(life: life, 
                damage: damage, 
                level: level);
        }

        private Character ACharacter()
        {
            return new MeleeFigther();
        }
    }
}