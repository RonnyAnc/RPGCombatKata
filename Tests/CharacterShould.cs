using FluentAssertions;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class CharacterShould
    {
        /*
            - characters have two states live or dead
            - deal damage
            - heal characters
		 */

        [Test]
        public void start_with_1000_points_as_health()
        {
            ACharacter().Life.Should().Be(1000);
            ACharacter().IsAlive().Should().BeTrue();
        }

        [Test]
        public void start_at_level_1()
        {
            ACharacter().Level.Should().Be(1);
        }

        [Test]
        public void attack_to_others()
        {
            var attacker = ACharacter();
            var damagedCharacter = ACharacter();

            attacker.AttackTo(damagedCharacter);

            damagedCharacter.Life.Should().Be(900);
        }

        private static Character ACharacter()
        {
            return new Character();
        }
    }
}