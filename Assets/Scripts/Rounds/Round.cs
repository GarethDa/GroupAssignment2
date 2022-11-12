using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Round
{
    protected int roundTime = 10;
    protected roundType type = roundType.DODGEBALL;
    protected string mapPrefabName = "";
    public bool hasMap
    {
        get;set;

    } = false;

   

    public void load()
    {
        setWinCondition();
        
       

        //players will get teleported to one area where all the levels will be played in. by loading the map, you modify the area around the teleportaion zone
    }

    public roundType getType()
    {
        return type;
    }


    public int getRoundTime()
    {
        return roundTime;
    }
    protected abstract void setWinCondition();//time running out will always be a win condition for all remaining players

    public abstract void unload();

  
    
}

public enum roundType { 
        NONE = 1,
       INTERMISSION = 2,
       FALLING_PLATFORMS = 4,
       PACHINKO_BALL = 8,
       BUMPER = 16,
       DODGEBALL = 32
}

