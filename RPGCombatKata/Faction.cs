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
                .Subscribe(Accept);
        }

        private void Accept(JoinToFactionRequest request)
        {
            Add(request.Character);
        }

        private void Add(Character character)
        {
            characters.Add(character);
        }

        public bool Contains(Character character)
        {
            return characters.Contains(character);
        }
    }
}