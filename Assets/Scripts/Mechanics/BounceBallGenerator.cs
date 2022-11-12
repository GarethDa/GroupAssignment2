using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBallGenerator : MonoBehaviour
{

    float timeBetweenBalls = 2;// 10 in seconds
    float timeElapsed = 0f;
    float timeWaited = 0f;
    float timeToWait = 2f;
    [SerializeField]float angle = 0;
    float anglePerSecond = 2.5f;
    public GameObject arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float control(float a)
    {
        return Mathf.Sin(a / Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeWaited != 0f)
        {
            timeWaited += Time.deltaTime;
            if(timeWaited > timeToWait)
            {
                timeWaited = 0f;
            }
            return;
        }
        
        float angleAdder = control(Mathf.Lerp(anglePerSecond, 0, timeElapsed / timeBetweenBalls));
        angle += angleAdder;//can feed the equation into another function
        if(angle > 360)
        {
            angle -= 360;
        }
       // arrow.transform.RotateAround(transform.position, Vector3.up, angleAdder);
        arrow.transform.forward = Vector3.Cross(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)),-Vector3.up);
        if (timeElapsed > timeBetweenBalls)
        {
            timeBetweenBalls = Random.Range(10, 15);//(10,15)
            timeElapsed = 0;
            
            shootBall();
            timeWaited += Time.deltaTime;
        }
    }

    public void shootBall(Vector3 dir = default(Vector3))
    {
        float ballSpeed = 1;
        Vector3 direction;
        if(dir == default(Vector3))
        {
            
            direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),0, Mathf.Sin(angle * Mathf.Deg2Rad)) * ballSpeed;
           // Debug.Log(angle + " " + direction);
        }
        else
        {
            
            direction = dir;
        }

        GameObject newBall = Instantiate(Resources.Load<GameObject>("Projectiles/PachinkoBall"));
        newBall.transform.position = transform.position;
        newBall.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
    }
}
