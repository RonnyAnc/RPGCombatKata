﻿using FluentAssertions;
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

        private static Character ACharacter()
        {
            return new Character();
        }
    }
}