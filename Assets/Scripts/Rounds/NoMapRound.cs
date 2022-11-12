using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMapRound : Round
{

    public NoMapRound()
    {
        roundTime = 0;
        mapPrefabName = "Default";
        hasMap = true;
        type = roundType.NONE;
    }
    protected override void setWinCondition()
    {
        //throw new System.NotImplementedException();
    }

    public override void unload()
    {
       // throw new System.NotImplementedException();
    }
}
