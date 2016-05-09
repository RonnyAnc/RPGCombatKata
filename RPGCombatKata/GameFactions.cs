using System.Collections.Generic;
using System.Linq;

namespace RPGCombatKata
{
    public class GameFactions
    {
        protected List<Faction> Factions = new List<Faction>();

        public void RegistFaction(Faction faction)
        {
            Factions.Add(faction);
        }

        public void UnregistFaction(Faction faction)
        {
            Factions.Remove(faction);
        }

        public bool AreInSameFaction(Character source, Character target)
        {
            return Factions.Any(faction => faction.AreMembers(source, target));
        }
    }
}