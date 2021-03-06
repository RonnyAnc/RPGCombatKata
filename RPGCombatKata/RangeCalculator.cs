using RPGCombatKata.Characters;

namespace RPGCombatKata
{
    public interface RangeCalculator
    {
        int CalculateDistanceBetween(Character source, Attackable target);
    }
}