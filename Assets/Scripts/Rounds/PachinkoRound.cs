using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PachinkoRound : Round
{

    public PachinkoRound()
    {
        roundTime = 10;
        mapPrefabName = "PachinkoBall";
        hasMap = false;
        type = roundType.PACHINKO_BALL;
    }
    protected override void setWinCondition()
    {
        
    }

    public override void unload()
    {
        
    }
}
