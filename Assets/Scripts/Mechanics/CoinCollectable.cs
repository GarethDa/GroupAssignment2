using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : Collectable
{

    

    public override void drop(Vector3 origin)
    {
        Vector3 throwDirection = new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f));
        
        
        float throwSpeed = 10;
        transform.position = origin;

        canPickup = false;
        gameObject.layer = 8;
        GetComponent<Rigidbody>().AddForce(throwDirection.normalized * throwSpeed);
        Debug.Log(throwDirection * throwSpeed);
    }

    public override void pickup()
    {
        //TODO: add to a scoreboard or something when the player is done
        Destroy(gameObject);
    }
}
