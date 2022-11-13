using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PatrolScript : MonoBehaviour
{
    //public Transform P1T;
    //public Transform P2T;
    
    //Old system stuff
    Vector3 patrol1;
    Vector3 patrol2;
    bool targettingP1 = true;
    Vector3 target;

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

    // Start is called before the first frame update
    void Start()
    {
        //patrol1 = transform.Find("Target 1").position;
        //patrol2 = transform.Find("Target 2").position;

        //patrol1 = P1T.position;
        //patrol2 = P2T.position;

        //target = patrol1;

        //rb = GetComponent<Rigidbody>();

        //SphereCollider[] colliders = GetComponents<SphereCollider>();
        //if (colliders[0].isTrigger)
        //{
        //    trigger = colliders[0];
        //}
        //else
        //{
        //    trigger = colliders[1];
        //}

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
        //if (points.Count < 4) mode = MovementMode.lerp;

        /*
        //Update Player Position
        if (trackingPlayer)
        {
            target = trackedPlayer.transform.position;
        }

        //Draw a vector to the target.
        //If we've reached out patrol point, then switch to next patrol point and redraw the target
        Vector3 thisToTarget = target - transform.position;
        if (!trackingPlayer && thisToTarget.magnitude < 0.4)
        {
            Debug.Log("Point Reached. Switching to next point");
            if (targettingP1)
            {
                target = patrol2;
                targettingP1 = false;
            }
            else
            {
                target = patrol1;
                targettingP1 = true;
            }
            thisToTarget = target - transform.position;
        }

        thisToTarget.y = 0;
        if (speed < speedMax)
        {
            speed = speed + speedDelta;
        }
        //rb.velocity = thisToTarget.normalized * speed;
        */

        /*
        switch (mode)
        {
            case MovementMode.lerp:
                {

                }
            default:
                break;
        }
        */

        //if (currentCooldown <= 0) spline.Spline.Resize(0);

        //If the object is tracking a player
        if (trackingPlayer)
        {
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
        }

        //If the object is returning and has traversed the entire spline one way (distance = time * velocity)
        else if (returning && (splineAnimator.elapsedTime * splineAnimator.maxSpeed) >= returnDistance)
        {
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
            returning = false;

            //Change the object to blue, the colour for normal spline traversal
            gameObject.GetComponent<Renderer>().material.color = blueColour;
        }

        //Else, update the timer
        else
        {
            currentCooldown -= Time.deltaTime;
        }

        //If we haven't reached max speed, increase the speed
        if (splineAnimator.maxSpeed < speedMax) splineAnimator.maxSpeed += speedDelta;

        //If the speed ever surpasses the maximum, set it back
        else if (splineAnimator.maxSpeed > speedMax) splineAnimator.maxSpeed = speedMax;
        
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //Start Tracking Player
        if(other.tag == "Player" && currentCooldown < 0 && trackedPlayer != other.gameObject)
        {
            //Set tracking to true and grab the player to track
            trackingPlayer = true;
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
            if (!returning)
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
            if (trackingPlayer)
            {
                //Grab the current position of the object in relation to the spline's local space
                Vector3 thisPos = spline.transform.InverseTransformPoint(transform.position);

                //No longer tracking
                trackingPlayer = false;
                trackedPlayer = null;

                //Set the cooldown to full
                currentCooldown = playerTargettingCooldown;

                //The object returns at full speed, so set speed to max
                splineAnimator.maxSpeed = speedMax;

                //Set the loop mode to once since we don't want to loop back and forth through the return path
                splineAnimator.loopMode = SplineAnimate.LoopMode.Once;
                
                //Reset the animator, clear the spline
                splineAnimator.Restart(true);
                spline.Spline.Clear();

                //Add two knots: the current position and the first knot of the original spline.
                //This means the object will begin moving back towards the beginning of the original spline
                spline.Spline.Add(new BezierKnot(thisPos));
                spline.Spline.Add(originalKnots[0]);

                //Object is returning
                returning = true;

                //Set the colour to yellow, the returning colour
                gameObject.GetComponent<Renderer>().material.color = yellowColour;

                //Calculate the distance that the object needs to traverse in order to be back to the beginning of the original spline
                returnDistance = Vector3.Distance(spline.Spline.ToArray()[0].Position, spline.Spline.ToArray()[1].Position);
            }
            
        }
    }
}
