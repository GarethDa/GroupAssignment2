using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager 
{
    public static EventHandler onSecondTickEvent;
    public static EventHandler onRoundEnd;
    public static EventHandler onRoundStart;
    public static EventHandler onPlayerDeath;//TODO: remove player from the pool of players that can get rewards in some new manager class we have to make
    public static EventHandler onPlayerFell;
    public static EventHandler<CollectableArgs> onPlayerCollect;
    public static EventHandler<CollectableArgs> onPlayerUsePowerup;
}

public class CollectableArgs : EventArgs
{
    Collectable collectableObject;
    //TODO: add player to this
    public CollectableArgs(Collectable c)
    {
        collectableObject = c;
    }
}
