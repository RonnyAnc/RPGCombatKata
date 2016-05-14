using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RPGCombatKata;
using RPGCombatKata.Characters;

namespace Tests
{
    [TestFixture]
    public class FightersShould
    {
        [Test]
        public void have_an_attack_range_of_2_metters_when_is_a_melee_fighter()
        {
            AMeleeFigther().AttackRange.Should().Be(2);
        }

        [Test]
        public void have_an_attack_range_of_20_metters_when_is_a_ranged_fighter()
        {
            ARangedFigther().AttackRange.Should().Be(20);
        }

        private static RangedFigther ARangedFigther()
        {
            return new RangedFigther(int.MaxValue, decimal.MaxValue);
        }

        private static MeleeFigther AMeleeFigther()
        {
            return new MeleeFigther(int.MaxValue, decimal.MaxValue);
        }
    }
}