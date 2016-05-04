using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RPGCombatKata
{
    public class Faction
    {
        private List<Character> characters = new List<Character>(); 

        public Faction()
        {
            EventBus.AsObservable<JoinToFactionRequest>()
                .Where(r => r.Faction == this)
                .Subscribe(AcceptJoinment);

            EventBus.AsObservable<LeaveFactionRequest>()
                .Where(request => request.Faction == this)
                .Subscribe(AcceptAbandonment);
        }

        private void AcceptJoinment(JoinToFactionRequest request)
        {
            AddToFunction(request.Character);
        }

        private void AcceptAbandonment(LeaveFactionRequest request)
        {
            RemoveFromFaction(request.Character);
        }

        private void RemoveFromFaction(Character character)
        {
            characters.Remove(character);
        }

        private void AddToFunction(Character character)
        {
            characters.Add(character);
        }

        public bool Contains(Character character)
        {
            return characters.Contains(character);
        }
    }
}