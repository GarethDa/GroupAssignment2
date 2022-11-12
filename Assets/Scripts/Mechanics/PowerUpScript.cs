using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private GameObject playerObject;
    private Rigidbody rb; //player rigidboy

    //Super Jump Vairables
    private float initialJumpForce; //grabbed value at start
    [SerializeField] [Range(1.0f, 40.0f)] private float superjumpForce = 15.0f;

    //Slowfall variables
    private bool slowfallEnabled = false;
    [SerializeField] [Range(1.0f, 20.0f)] private float slowfallForce = 10.0f;
    [SerializeField] [Range(-1.0f, -10.0f)] private float maxFallSpeed = -4.0f;
    private bool playerGrounded = true;

    private bool usedPowerUp = false; //if we have used our powerup
    public float powerUpDuration = 3f; //total duration of ability in seconds
    private float currentPowerUpDuration = 0f; //internal clock for powerup duration

    enum powerUpList { None, SuperJump, SlowFall}
    [SerializeField] powerUpList selectedPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
        initialJumpForce = playerObject.GetComponent<TpMovement>().GetJumpForce();
    }

    public void OnPowerUp()
    {
        //in the future this should probably be part of a game manager, but for now it's on the player//
        //there should probably also be an observer to check when the player actually uses their boosted ablility and then resets
        //their stats and removes their powerup
        //for now, we'll just have it on a timer
        if (!usedPowerUp)
        {
            usedPowerUp = true;
            currentPowerUpDuration = 0;

            if (selectedPowerUp == powerUpList.SuperJump)
            {
                playerObject.GetComponent<TpMovement>().SetJumpForce(superjumpForce);
            }
            else if (selectedPowerUp == powerUpList.SlowFall)
            {
                slowfallEnabled = true;
            }
        }
    }

    private void Update()
    {
        //this one goes in update instead of fixed update; causes freezes otherwise
        if (slowfallEnabled)
        {
            playerGrounded = playerObject.GetComponent<TpMovement>().GetIsGrounded();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //constantly adds up INGAME time, in case we want to use slow motion or something
        if (usedPowerUp)
        {
            currentPowerUpDuration += Time.fixedDeltaTime;
        }

        //only apply the force when the player is in the air
        if (!playerGrounded)
        {
            //only apply forec if the player is actually falling
            if (rb.velocity.y < maxFallSpeed)
            {
                rb.AddForce(Vector3.up * (rb.velocity.y * rb.velocity.y + maxFallSpeed) / 10 * slowfallForce); //apply force as a function of current downward velocity
            }
        }

        //if we're over our timer and we actually used a powerup
        if (currentPowerUpDuration >= powerUpDuration && usedPowerUp)
        {
            //reset our used state and our duration
            usedPowerUp = false;
            currentPowerUpDuration = 0;

            if (selectedPowerUp == powerUpList.SuperJump)
            {
                playerObject.GetComponent<TpMovement>().SetJumpForce(initialJumpForce);

            }
            else if (selectedPowerUp == powerUpList.SlowFall)
            {
                slowfallEnabled = false;
                playerGrounded = true;
            }
            Debug.Log("Powerup done");
            //selectedPowerUp = powerUpList.None; //"Consume" powerup when done
        }
    }
}
