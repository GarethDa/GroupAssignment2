using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class CharacterAiming : MonoBehaviour
{
    [Header("Required Objects")]
    [SerializeField] Camera playerCam;
    [SerializeField] Transform playerObj;
    [SerializeField] CinemachineVirtualCamera zoomCam;

    [Header("Throwing Projectiles")]
    [SerializeField] [Range(100.0f, 2000.0f)] float throwForce = 500.0f;

    Image reticle;

    bool isAiming = false;
    bool holdingProjectile = false;

    GameObject heldProjectile = null;

    //UserInput inputAction;

    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //inputAction = InputController.controller.inputAction;

        //inputAction.Player.Aim.performed += cntxt => OnAim();
        //inputAction.Player.Aim.canceled += cntxt => OnAim();

        //inputAction.Player.Fire.performed += cntxt => OnFire();

        //Lock the cursor, make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        reticle = GameObject.Find("Reticle").GetComponent<Image>();

        //Hide the reticle
        reticle.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            //If the player is aiming, set the player object's rotation around the y-axis to that of the camera
            playerObj.transform.rotation = Quaternion.Euler(new Vector3(0f, playerCam.transform.rotation.eulerAngles.y, 0f));
        }
        
    }

    private void FixedUpdate()
    {
    }

	//Aiming is a value type, not a button type, meaning it can sense when aim is being held down and released
	public void OnAim(InputAction.CallbackContext cntxt)
	{
        //If the player is aiming, prioritize the zoomed in camera and enable to reticle
        if (cntxt.performed)
        {
            zoomCam.Priority += 10;

            reticle.enabled = true;

            isAiming = true;
        }

        //If the player isn't aiming, prioritize the zoomed out camera and disable the reticle
        else if (cntxt.canceled)
        {
            zoomCam.Priority -= 10;

            reticle.enabled = false;

            isAiming = false;
        }
	}

    public void OnFire()
    {
        //If the player is holding a projectile, then go through the steps to throw it
        if (holdingProjectile)
        {
            //Set the projectile back to non-kinematic
            heldProjectile.GetComponent<Rigidbody>().isKinematic = false;

            //Throw the ball forward, multiplied by the throwing force
            heldProjectile.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * throwForce);

            //The player is no longer holding a projectile
            holdingProjectile = false;

            //The ball shouldn't be a child of the player model anymore
            heldProjectile.transform.SetParent(null);

            //Tell the projectile that it isn't being held anymore
            heldProjectile.GetComponent<BallBehaviour>().SetIsHeld(false);
        }
    }

    //Function for telling whether the player is holding a projectile or not
    public bool IsHoldingProj()
    {
        return holdingProjectile;
    }

    //Function for locking a projectile to the player
    public void SetProjectile(GameObject projectile)
    {
        //Set holding projectile to true, and save the projectile being held
        holdingProjectile = true;
        heldProjectile = projectile;

        //Set the projectile's position to be in front of the player model, with some offset
        heldProjectile.transform.position = playerObj.transform.position 
            + playerObj.transform.forward.normalized 
            + (0.8f * playerObj.transform.right.normalized) 
            + (0.5f * playerObj.transform.up.normalized);

        //Parent the player model to the projectile
        heldProjectile.transform.SetParent(playerObj.transform);

        //Set the projectile to kinematic, ensuring it doesn't move while being held
        heldProjectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
