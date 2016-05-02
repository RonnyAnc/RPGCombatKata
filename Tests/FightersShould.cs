using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class FightersShould
    {
        [Test]
        public void have_an_attack_range_of_2_metters_when_is_a_melee_fighter()
        {
            new MeleeFigther().AttackRange.Should().Be(2);
        }

        [Test]
        public void have_an_attack_range_of_20_metters_when_is_a_ranged_fighter()
        {
            new RangedFigther().AttackRange.Should().Be(20);
        }
    }
}