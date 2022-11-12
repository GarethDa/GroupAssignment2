using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    private bool isHeld = false;

    private GameObject playerCam;
    private GameObject playerObject;
    //private GameObject playerModel;

    //[SerializeField] [Range(500.0f, 1500.0f)] private float ThrowForce = 800.0f;

    void Start()
    {
        //Find the player camera and player objects
        playerCam = GameObject.Find("Main Camera");
        playerObject = GameObject.Find("Player");
        //playerModel = GameObject.Find("PlayerObj");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If the ball is being held
        if (isHeld)
        {
            //Reset velocity and angular velocity to zero
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            //GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            //Update the position to be in front of the player
            //transform.position = playerModel.transform.position + 2 * playerModel.transform.forward.normalized + new Vector3(0f, 1f, 0f);

            //transform.rotation = Quaternion.Euler(new Vector3(0f, playerCam.transform.rotation.eulerAngles.y, 0f));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //If the player touches a ball that isn't being held, and the player isn't already holding a ball
        if (!isHeld && collision.gameObject.tag == "Player" && !playerObject.GetComponent<CharacterAiming>().IsHoldingProj())
        {
            //Update the position of the ball to be in front of the players
            //transform.position = playerModel.transform.position + 2 * playerModel.transform.forward.normalized + new Vector3(0f, 1f, 0f);
            
            //Update the ball to be held and the player to be holding a ball
            isHeld = true;
            //playerObject.GetComponent<CharacterAiming>().SetIsHoldingBall(true);

            //Disable gravity while being held
            //GetComponent<Rigidbody>().isKinematic = true;

            playerObject.GetComponent<CharacterAiming>().SetProjectile(this.gameObject);

            //Set the camera as a parent to the ball, ensuring it maintains the same rotation as the camera
            //transform.SetParent(playerModel.transform);
        }
    }

    public void SetIsHeld(bool held)
    {
        isHeld = held;
    }

    public bool GetIsHeld()
    {
        return isHeld;
    }

    /*
    void OnFire()
    {
        if (isHeld)
        {
            //Throw the ball forward, multiplied by the throwing force
            GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * ThrowForce);

            //The ball is no longer held, and the player is no longer holding a ball
            isHeld = false;
            playerObject.GetComponent<CharacterAiming>().SetIsHoldingBall(false);

            //The ball shouldn't be a child of the camera anymore
            transform.SetParent(null);
        }
    }
    */

    //Old input system
    /*
    void CheckBallThrow()
    {
        //If the player clicks the LMB
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Throw the ball forward, multiplied by the throwing force
            GetComponent<Rigidbody>().AddForce(PlayerCam.transform.forward * ThrowForce);

            //The ball is no longer held, and the player is no longer holding a ball
            isHeld = false;
            PlayerObject.SetIsHoldingBall(false);

            //The ball shouldn't be a child of the camera anymore
            transform.SetParent(null);
        }
    }
    */
}
