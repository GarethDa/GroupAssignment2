using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEvents : MonoBehaviour
{
    public static TimerEvents instance = null;
   
    float timeSinceLastSecond = 0;
    

    public void OnEnable()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSecond += Time.deltaTime;
        if(timeSinceLastSecond > 1)
        {
            EventManager.onSecondTickEvent?.Invoke(null,EventArgs.Empty);
            timeSinceLastSecond = 0;
        }
    }
}
