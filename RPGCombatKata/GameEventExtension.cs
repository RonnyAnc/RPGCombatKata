﻿namespace RPGCombatKata
{
    public static class GameEventExtension 
    {
        public static void Raise(this GameEvent gameEvent)
        {
            EventBus.Send(gameEvent);
        }
    }
}