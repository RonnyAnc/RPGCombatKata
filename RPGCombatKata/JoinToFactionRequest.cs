﻿namespace RPGCombatKata
{
    public class JoinToFactionRequest : GameEvent
    {
        public Character Character { get; }
        public Faction Faction { get; }

        public JoinToFactionRequest(MeleeFigther character, Faction faction)
        {
            Character = character;
            Faction = faction;
        }
    }
}