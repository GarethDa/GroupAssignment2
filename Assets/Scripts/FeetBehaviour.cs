using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetBehaviour : MonoBehaviour
{
    private Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = transform.parent.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Finish"))
        {
            //rBody.velocity = new Vector3(rBody.velocity.x, 0f, rBody.velocity.z);

            //gameObject.GetComponent<SphereCollider>().GetComponent<PhysicMaterial>().dynamicFriction = 0f;

            Debug.Log("here");
        }
    }
}
