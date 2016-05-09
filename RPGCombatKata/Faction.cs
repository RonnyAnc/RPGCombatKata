using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                .Where(r => !r.Faction.Contains(r.Character))
                .Subscribe(AcceptJoinment);

            EventBus.AsObservable<LeaveFactionRequest>()
                .Where(request => request.Faction == this)
                .Subscribe(AcceptAbandonment);
        }

        public int CharactersCount()
        {
            return characters.Count;
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

        public bool AreMembers(params Character[] characters)
        {
            return characters.All(AreMembers);
        }

        private bool AreMembers(Character character)
        {
            return this.characters.Contains(character);
        }
    }
}