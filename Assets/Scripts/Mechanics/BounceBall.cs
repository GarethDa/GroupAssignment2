using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)//player layer
        {
            //KILL THEM
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.onSecondTickEvent += updateVelocity;
    }

    private void updateVelocity(object sender, EventArgs e)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.VelocityChange);
    }
}
