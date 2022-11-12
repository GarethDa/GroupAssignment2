using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    [SerializeField] string ignoreTag = "Environment";
    [SerializeField] float bounceForce = 500;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        if(collision.transform.tag != ignoreTag)
        {
            Debug.Log("BOOM!");
            Rigidbody otherRB = collision.rigidbody;
            otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
        }
    }
}
