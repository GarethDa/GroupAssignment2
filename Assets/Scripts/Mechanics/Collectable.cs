using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class Collectable : MonoBehaviour
{

    protected bool canPickup = true;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7 && canPickup)//7 is PlayerLayer
        {
            pickup();
            EventManager.onPlayerCollect?.Invoke(null, new CollectableArgs(this));
        }
    }

   

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)//7 is PlayerLayer
        {
            canPickup = true;   //makes sure that when it gets dropped from a player, they dont immediately pick the same item up again
            gameObject.layer = 0;
        }
    }


    //should be different for each item that gets picked up
    //some items get added to the inventory, some trigger special events, etc

    public abstract void drop(Vector3 origin);//when you want to drop, just instanciate a prefab from the resources folder and call the .drop() function to throw it form a location

    public abstract void pickup();


}
