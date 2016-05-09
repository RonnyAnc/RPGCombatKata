using System.Collections.Generic;
using System.Linq;

namespace RPGCombatKata
{
    public class GameFactions
    {
        private List<Faction> factions = new List<Faction>();

        public void RegistFaction(Faction faction)
        {
            factions.Add(faction);
        }

        public void UnregistFaction(Faction faction)
        {
            factions.Remove(faction);
        }

        public bool AreInSameFaction(Character source, Character target)
        {
            return factions.Any(faction => faction.AreMembers(source, target));
        }
    }
}