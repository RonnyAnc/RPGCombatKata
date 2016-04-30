using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RPGCombatKata;

namespace Tests
{
    [TestFixture]
    public class CharacterShould
    {
        private const int FullLife = 1000;
        private RangeCalculator rangeCalculator;

        [SetUp]
        public void SetUp()
        {
            rangeCalculator = Substitute.For<RangeCalculator>();
        }

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
            var targetCharacter = ACharacter();
            var attack = new Attack(target : targetCharacter, source: attacker);
            
            attack.Raise();

            targetCharacter.Life.Should().Be(900);
        }

        [Test]
        public void die_when_its_life_arrives_to_zero()
        {
            var attacker = ACharacterWithDamage(FullLife);
            var damagedCharacter = ACharacter();
            var attack = new Attack(target: damagedCharacter, source: attacker);

            attack.Raise();

            ShouldBeDead(damagedCharacter);
        }

        [Test]
        public void not_be_healed_when_it_has_full_life()
        {
            var character = ACharacterWithLife(FullLife);

            character.Heal();

            character.Life.Should().Be(FullLife);
        }

        [Test]
        public void not_be_healed_when_it_is_dead()
        {
            var deadCharacter = ACharacterWithLife(0);

            deadCharacter.Heal();

            deadCharacter.Life.Should().Be(0);
        }

        [Test]
        public void be_able_to_heal_himself()
        {
            var damagedCharacter = ACharacterWithLife(500);

            damagedCharacter.Heal();

            damagedCharacter.Life.Should().Be(600);
        }
    
//        TODO
        [Test, Ignore("Study how to do with the new implementation")]
        public void not_attack_himself()
        {
            var character = ACharacterWithLife(500);

            var damagedCharacter = ACharacter();
            var attack = new Attack(target: damagedCharacter, damage: FullLife);

            attack.Raise();
            character.AttackTo(character);

            character.Life.Should().Be(500);
        }

        [Test]
        public void the_damage_applied_will_be_reduced_50_percent_when_target_is_5_or_more_levels_above()
        {
            var attacker = ACharacterWith(level: 10, damage: 50);
            var damagedCharacter = ACharacterWith(level: 20, life: 500);
            var attack = new Attack(source: attacker, target: damagedCharacter);

            attack.Raise();

            damagedCharacter.Life.Should().Be(475);
        }

        [Test]
        public void the_damage_applied_will_be_boosted_50_percent_when_target_is_5_or_more_levels_below()
        {
            var attacker = ACharacterWith(level: 10, damage: 50);
            var damagedCharacter = ACharacterWith(level: 5, life: 500);
            var attack = new Attack(source: attacker, target: damagedCharacter);

            attack.Raise();

            damagedCharacter.Life.Should().Be(400);
        }

        private Character ACharacterWith(int level, int life = 1000, int damage = 0)
        {
            return new TestableCharacter(
                rangeCalculator: rangeCalculator,
                life: life, 
                damage: damage, 
                level: level);
        }

        private Character ACharacterWithLife(int life)
        {
            return new TestableCharacter(rangeCalculator, life);   
        }

        private static void ShouldBeDead(Character damagedCharacter)
        {
            damagedCharacter.Life.Should().Be(0);
            damagedCharacter.IsAlive().Should().BeFalse();
        }

        private TestableCharacter ACharacterWithDamage(int damage)
        {
            return new TestableCharacter(
                rangeCalculator: rangeCalculator,
                life: 100, 
                damage: damage);
        }

        private Character ACharacter()
        {
            return new MeleeFigther(rangeCalculator);
        }
    }
}