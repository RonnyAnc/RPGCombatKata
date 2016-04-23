using FluentAssertions;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class CharacterShould
    {
        /*
			- start with 1000 point as health
            - start at level 1
            - characters have two states live or dead
            - deal damage
            - heal characters
		 */

        [Test]
        public void start_with_1000_points_as_health()
        {
            ACharacter().Life.Should().Be(1000);
        }

        private static Character ACharacter()
        {
            return new Character();
        }
    }
}