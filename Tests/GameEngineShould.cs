using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using NSubstitute;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class GameEngineShould
    {
        private const decimal FullLife = 1000;
        public static RangeCalculator rangeCalculator;
        public static Game game;

        public void LoadGame()
        {
            if (game == null)
            {
                rangeCalculator = Substitute.For<RangeCalculator>();
                game = new Game(rangeCalculator);
            }
        }

        [SetUp]
        public void SetUp()
        {
            LoadGame();
        }

        [Test]
        public async Task send_damage_to_a_player_that_is_attacked_by_other_inside_his_range()
        {
            var attacker = new MeleeFigther();
            var victim = new MeleeFigther();
            rangeCalculator.CalculateDistanceBetween(attacker, victim)
                .Returns(1);
            var attack = new Attack(source: attacker, target: victim);
             
            attack.Raise();

            victim.Life.Should().Be(900);
        }

        [Test]
        public async Task die_when_its_life_arrives_to_zero()
        {
            var attacker = ACharacterWithDamage(FullLife);
            var damagedCharacter = ACharacter();
            rangeCalculator.CalculateDistanceBetween(attacker, damagedCharacter)
                .Returns(1);
            var attack = new Attack(target: damagedCharacter, source: attacker);

            attack.Raise();

            ShouldBeDead(damagedCharacter);
        }

        private Character ACharacterWith(int level, int life = 1000, int damage = 0)
        {
            return new TestableCharacter(
                life: life,
                damage: damage,
                level: level);
        }

        private Character ACharacterWithLife(int life)
        {
            return new TestableCharacter(life);
        }

        private static void ShouldBeDead(Character damagedCharacter)
        {
            damagedCharacter.Life.Should().Be(0);
            damagedCharacter.IsAlive().Should().BeFalse();
        }

        private TestableCharacter ACharacterWithDamage(decimal damage)
        {
            return new TestableCharacter(life: 100,
                damage: damage);
        }

        private Character ACharacter()
        {
            return new MeleeFigther();
        }
    }
}