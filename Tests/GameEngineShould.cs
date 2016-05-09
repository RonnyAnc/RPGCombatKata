﻿using System.Threading.Tasks;
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
        public static GameEngine GameEngine;

        public void LoadGame()
        {
            if (GameEngine == null)
            {
                rangeCalculator = Substitute.For<RangeCalculator>();
                GameEngine = new GameEngine(rangeCalculator);
            }
        }

        [SetUp]
        public void SetUp()
        {
            LoadGame();
        }

        [Test]
        public async Task send_damage_to_a_player_that_is_in_source_enemy_attack_range()
        {
            var attacker = ACharacterWith(damage: 100, range: 5);
            var victim = ACharacterWith(life: FullLife);
            rangeCalculator.CalculateDistanceBetween(attacker, victim)
                .Returns(1);
            var attack = new Attack(source: attacker, target: victim);
             
            attack.Raise();

            victim.Life.Should().Be(900);
        }

        [Test]
        public async Task not_send_damage_to_a_player_that_is_not_in_source_enemy_attack_range()
        {
            var attacker = ACharacterWith(damage: 100, range: 5);
            var victim = ACharacterWith(life: FullLife);
            rangeCalculator.CalculateDistanceBetween(attacker, victim)
                .Returns(6);
            var attack = new Attack(source: attacker, target: victim);

            attack.Raise();

            victim.Life.Should().Be(1000);
        }

        [Test]
        public async Task die_when_its_life_arrives_to_zero()
        {
            var attacker = ACharacterWith(damage: FullLife, range: 1);
            var damagedCharacter = ACharacterWith(life: FullLife);
            rangeCalculator.CalculateDistanceBetween(attacker, damagedCharacter)
                .Returns(1);
            var attack = new Attack(target: damagedCharacter, source: attacker);

            attack.Raise();

            ShouldBeDead(damagedCharacter);
        }

        [Test]
        public void avoid_players_attack_themselves()
        {
            var player = ACharacterWith(life: FullLife, damage: FullLife);
            rangeCalculator.CalculateDistanceBetween(player, player)
                .Returns(0);
            var attack = new Attack(source: player, target: player);

            attack.Raise();

            player.Life.Should().Be(FullLife);
        }

        [Test]
        public void reduce_a_50_percent_the_damage_when_target_is_5_or_more_levels_above()
        {
            var attacker = ACharacterWith(level: 10, damage: 50);
            var damagedCharacter = ACharacterWith(level: 20, life: 500);
            var attack = new Attack(source: attacker, target: damagedCharacter);

            attack.Raise();

            damagedCharacter.Life.Should().Be(475);
        }

        [Test]
        public void boost_a_50_percent_the_damage_when_target_is_5_or_more_levels_below()
        {
            var attacker = ACharacterWith(level: 10, damage: 50);
            var damagedCharacter = ACharacterWith(level: 5, life: 500);
            var attack = new Attack(source: attacker, target: damagedCharacter);

            attack.Raise();

            damagedCharacter.Life.Should().Be(400);
        }

        [Test]
        public void avoid_attacks_between_partners()
        {
            var attacker = ACharacterWith(damage: 100);
            var partner = ACharacterWith(life: FullLife);
            var faction = new Faction();
            GameEngine.RegistFaction(faction);
            new JoinToFactionRequest(attacker, faction).Raise();
            new JoinToFactionRequest(partner, faction).Raise();

            new Attack(source: attacker, target: partner).Raise();

            partner.Life.Should().Be(FullLife);
        }

        private static Character ACharacterWith(int level = 1, decimal life = 1000, decimal damage = 0, int range = 0)
        {
            return new TestableCharacter(
                life: life,
                damage: damage,
                level: level,
                range: range);
        }

        private static void ShouldBeDead(Character damagedCharacter)
        {
            damagedCharacter.Life.Should().Be(0);
            damagedCharacter.IsAlive().Should().BeFalse();
        }
    }
}