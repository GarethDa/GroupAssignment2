using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject playerManager;
    public GameObject levelManager;

    public int playersConnected = 2;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }

    }

    
}
