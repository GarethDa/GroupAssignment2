using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public enum BumperState
{
    BALL_PATROL,
    BALL_COOLDOWN,
    BALL_CHASE,
    BALL_RETURN
}

public class PatrolScript : MonoBehaviour
{

    GameObject trackedPlayer;
    bool trackingPlayer = false;

    public Rigidbody rb;
    //SphereCollider trigger;

    [Header("Movement Values")]
    [SerializeField] float initialSpeed = 1.0f;
    [SerializeField] float speedMax = 15.0f;
    [Range(0.001f, 0.1f)] [SerializeField] float speedDelta = 0.05f;
    [SerializeField] float playerTargettingCooldown = 10.0f;

    [Header("Spline Values")]
    [SerializeField] SplineContainer spline;

    SplineAnimate splineAnimator;
    BezierKnot[] originalKnots;

    float movementTimer = 0.0f;
    float currentCooldown = 0f;

    bool returning = false;

    float returnDistance;

    Color blueColour = new Color(0, 0.933f, 0.894f);
    Color redColour = new Color(0.933f, 0, 0.059f);
    Color yellowColour = new Color(0.933f, 0.933f, 0);

    BumperState _bumperState = BumperState.BALL_PATROL;

    // Start is called before the first frame update
    void Start()
    {
        //Grab the spline animator
        splineAnimator = transform.parent.GetComponent<SplineAnimate>();
        
        //Set the animator's speed to the initial speed
        splineAnimator.maxSpeed = initialSpeed;

        //Store all of the original knots of the spline, since are are going to be altering the knots
        originalKnots = spline.Spline.ToArray();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_bumperState)
        {
            case BumperState.BALL_PATROL:
                currentCooldown -= Time.deltaTime;
                break;
            case BumperState.BALL_CHASE:
                //Perform this every half a second
                if (movementTimer >= 0.5f)
                {
                    //Remove all spline knots
                    spline.Spline.Clear();

                    //Create a vector representing the object's position local to the spline
                    Vector3 thisPos = spline.transform.InverseTransformPoint(transform.position);

                    //Make a vector representing a point ahead of the object in the direction of the player local to the spline
                    Vector3 targetPos = thisPos + 30 * (spline.transform.InverseTransformPoint(trackedPlayer.transform.position) - thisPos).normalized;

                    //For now, we aren't changing the object's y-value
                    targetPos.y = thisPos.y;

                    //Add the points to the spline
                    spline.Spline.Add(new BezierKnot(spline.transform.InverseTransformPoint(transform.position)));
                    spline.Spline.Add(new BezierKnot(targetPos));

                    //Reset the timer
                    movementTimer = 0f;

                    //Make sure elapsed time is 0, meaning the object starts at the beginning of the spline
                    splineAnimator.elapsedTime = 0f;
                }

                //Update the timer
                else movementTimer += Time.deltaTime;
                
                currentCooldown -= Time.deltaTime;

                break;
            case BumperState.BALL_COOLDOWN:
                currentCooldown -= Time.fixedDeltaTime;
                if(currentCooldown <= 0f)
                {
                    Debug.Log("returning");

                    //Grab the current position of the object in relation to the spline's local space
                    Vector3 thisPos = spline.transform.InverseTransformPoint(transform.position);

                    _bumperState = BumperState.BALL_RETURN;

                    //The object returns at full speed, so set speed to max
                    splineAnimator.maxSpeed = 0.1f;

                    //Set the loop mode to once since we don't want to loop back and forth through the return path
                    splineAnimator.loopMode = SplineAnimate.LoopMode.Once;

                    //Reset the animator, clear the spline
                    splineAnimator.Restart(true);
                    spline.Spline.Clear();

                    //Add two knots: the current position and the first knot of the original spline.
                    //This means the object will begin moving back towards the beginning of the original spline
                    spline.Spline.Add(new BezierKnot(thisPos));
                    spline.Spline.Add(originalKnots[0]);

                    //Set the colour to yellow, the returning colour
                    gameObject.GetComponent<Renderer>().material.color = yellowColour;

                    //Calculate the distance that the object needs to traverse in order to be back to the beginning of the original spline
                    returnDistance = Vector3.Distance(spline.Spline.ToArray()[0].Position, spline.Spline.ToArray()[1].Position);

                }
                break;
            case BumperState.BALL_RETURN:
                //Reset the speed back to initial
                splineAnimator.maxSpeed = initialSpeed;

                //Set the loop mode back to looping and restart the spline animation
                splineAnimator.loopMode = SplineAnimate.LoopMode.Loop;
                splineAnimator.Restart(true);

                //Remove all of the spline's knots
                spline.Spline.Clear();

                //Go through each of the original knots and add them back to the spline, effectively
                //returning the spline back to its original state
                foreach (BezierKnot knot in originalKnots)
                {
                    spline.Spline.Add(knot);
                }

                //Since the object is back to the beginning, it isn't return anymore
                _bumperState = BumperState.BALL_PATROL;

                //Change the object to blue, the colour for normal spline traversal
                gameObject.GetComponent<Renderer>().material.color = blueColour;
                break;
        }

        //If we haven't reached max speed, increase the speed
        if (splineAnimator.maxSpeed < speedMax) splineAnimator.maxSpeed += speedDelta;

        //If the speed ever surpasses the maximum, set it back
        else if (splineAnimator.maxSpeed > speedMax) splineAnimator.maxSpeed = speedMax;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Start Tracking Player
        //Debug.Log(currentCooldown);
        if(other.tag == "Player" && currentCooldown < 0 && trackedPlayer != other.gameObject && _bumperState != BumperState.BALL_RETURN)
        {
            //Set tracking to true and grab the player to track
            _bumperState = BumperState.BALL_CHASE;
            trackedPlayer = other.gameObject;

            //Set the targeting cooldown to half
            currentCooldown = playerTargettingCooldown / 2;

            //Set movement timer to 0.5, meaning the spline will immediately update to aim towards the player
            movementTimer = 0.5f;

            //spline.Spline.EditType = SplineType.Linear;

            //Change the colour to red, the tracking colour
            gameObject.GetComponent<Renderer>().material.color = redColour;

            //transform.parent.GetComponent<SplineAnimate>().maxSpeed = 3f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //rb.velocity = Vector3.zero;

        //If you hit the player, stop tracking it
        if (collision.gameObject.tag == "Player")
        {
            //If the object isn't returning
            if (_bumperState != BumperState.BALL_RETURN)
            {
                //Grab the current normalized time (whole number is number of times looped, decimal is % of way through current loop)
                float timeRatio = splineAnimator.normalizedTime;
                
                //Restart the animator
                splineAnimator.Restart(true);

                //Reset the speed to initial
                splineAnimator.maxSpeed = initialSpeed;

                //Update the time to maintain the same ratio as before, meaning the object won't jolt back/forward when its speed is changed
                splineAnimator.elapsedTime = timeRatio * splineAnimator.duration;
            }

            Debug.Log("Hit Player");

            //If the object is tracking a player, we want it to start returning
            if (_bumperState == BumperState.BALL_CHASE)
            {
                //No longer tracking
                _bumperState = BumperState.BALL_COOLDOWN;

                spline.Spline.Clear();
                spline.Spline.Add(new BezierKnot(spline.transform.InverseTransformPoint(transform.position)));

                trackedPlayer = null;

                //Set the cooldown to full
                currentCooldown = playerTargettingCooldown;
            }
            
        }
    }
}
