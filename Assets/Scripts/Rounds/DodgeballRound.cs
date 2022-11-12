using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballRound : Round
{
    public DodgeballRound()
    {
        hasMap = false;
        roundTime = 60;
        mapPrefabName = "Dodgeball";
        type = roundType.DODGEBALL;
    }

   

    protected override void setWinCondition()
    {
        //none
       
    }


    public override void unload()
    {
       
    }
}
