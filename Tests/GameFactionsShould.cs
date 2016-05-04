using FluentAssertions;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class GameFactionsShould
    {
        [Test]
        public void accept_requests_to_join_players_to_a_faction()
        {
            var character = new MeleeFigther();
            var faction = new Faction();
            var request = new JoinToFactionRequest(character: character, faction: faction);

            request.Raise();

            faction.Contains(character).Should().BeTrue();
        }
    }
}