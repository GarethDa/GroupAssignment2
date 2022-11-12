using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformRound : Round
{
    public FallingPlatformRound()
    {
        hasMap = true;
        roundTime = 60;
        mapPrefabName = "FallingPlatforms";
        type = roundType.FALLING_PLATFORMS;
    }

    protected override void setWinCondition()
    {
        //none
        EventManager.onPlayerFell += playerFell;
    }

    public void playerFell(object sender, System.EventArgs e)
    {
        PlayerManager.instance.playerDied();
    }

    public override void unload()
    {
        EventManager.onPlayerFell -= playerFell;
    }
}
