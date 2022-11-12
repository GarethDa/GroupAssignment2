
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermission : Round
{

    public Intermission()
    {
        type = roundType.INTERMISSION;
        roundTime = 60;
        hasMap = true;
    }

    public override void unload()
    {
        //didnt load anything
        
    
        
    }

    protected override void setWinCondition()
    {
        //do nothing, wait for time to expire
    }
}
