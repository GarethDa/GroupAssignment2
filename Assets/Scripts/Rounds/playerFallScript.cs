using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFallScript : MonoBehaviour
{
    public static playerFallScript instance;
   
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            //its a player
            //playerDied();
            EventManager.onPlayerFell?.Invoke(null, System.EventArgs.Empty);

        }
    }

    
}
