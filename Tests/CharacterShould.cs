using System;
using FluentAssertions;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class CharacterShould
    {
        private const int FullLife = 1000;

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
        public void attack_to_others()
        {
            var attacker = ACharacterWithDamage(100);
            var damagedCharacter = ACharacter();

            attacker.AttackTo(damagedCharacter);

            damagedCharacter.Life.Should().Be(900);
        }

        [Test]
        public void die_when_its_life_arrives_to_zero()
        {
            var attacker = ACharacterWithDamage(FullLife);
            var damagedCharacter = ACharacter();

            attacker.AttackTo(damagedCharacter);

            ShouldBeDead(damagedCharacter);
        }

        [Test]
        public void heal_other_characters()
        {
            var damagedCharacter = ACharacterWithLife(500);

            ACharacter().Heal(damagedCharacter);

            damagedCharacter.Life.Should().Be(600);
        }

        [Test]
        public void not_be_healed_when_it_has_full_life()
        {
            var character = ACharacterWithLife(FullLife);

            ACharacter().Heal(character);

            character.Life.Should().Be(FullLife);
        }

        [Test]
        public void not_be_healed_when_it_is_dead()
        {
            var deadCharacter = ACharacterWithLife(0);

            Action heal = () => ACharacter().Heal(deadCharacter);

            heal.ShouldThrow<HealDeadCharacterException>();
        }

        [Test]
        public void be_able_to_heal_himself()
        {
            var damagedCharacter = ACharacterWithLife(500);

            damagedCharacter.Heal();

            damagedCharacter.Life.Should().Be(600);
        }

        [Test]
        public void not_attack_himself()
        {
            var character = ACharacterWithLife(500);

            Action attackHimself = () => character.AttackTo(character);

            attackHimself.ShouldThrow<AttackHimselfException>();
        }

        [Test]
        public void not_heal_his_enemies()
        {
            var character = ACharacter();
            var enemy = ACharacterWithLife(100);

            Action healAnEnemy = () => character.Heal(enemy);

            healAnEnemy.ShouldThrow<HealAnEnemyException>();
        }

        [Test]
        public void reduce_its_attack_by_50_percent_when_its_enemy_has_5_or_more_levels_above()
        {
            var attacker = ACharacterWithDamage(FullLife);
            var damagedCharacter = ACharacter();

            attacker.AttackTo(damagedCharacter);

            ShouldBeDead(damagedCharacter);
        }

        private Character ACharacterWithLife(int life)
        {
            return new DamagedCharacter(life);   
        }

        private static void ShouldBeDead(Character damagedCharacter)
        {
            damagedCharacter.Life.Should().Be(0);
            damagedCharacter.IsAlive().Should().BeFalse();
        }

        private Character ACharacterWithDamage(int damage)
        {
            return new Character(damage);
        }

        private static Character ACharacter()
        {
            return new Character(0);
        }
    }
}