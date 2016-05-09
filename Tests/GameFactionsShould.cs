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

        [Test]
        public void accept_a_player_only_once()
        {
            var character = new MeleeFigther();
            var faction = new Faction();
            var request = new JoinToFactionRequest(character: character, faction: faction);

            request.Raise();
            request.Raise();

            faction.CharactersCount().Should().Be(1);
        }

        [Test]
        public void accept_requests_to_leave_a_faction()
        {
            var character = new MeleeFigther();
            var faction = new Faction();
            var joinRequest = new JoinToFactionRequest(character: character, faction: faction);
            var leaveRequest = new LeaveFactionRequest(character: character, faction: faction);

            joinRequest.Raise();
            leaveRequest.Raise();

            faction.Contains(character).Should().BeFalse();
        }
    }
}