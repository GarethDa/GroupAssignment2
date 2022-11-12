using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpinnerBehaviour : MonoBehaviour
{
    GameObject spinnerObject;
    Color spinnerColor;
    float speedDifferenceFactor;
    float colorDifferenceFactor;
    float currentSpeed;
    float currentTime = 0.0f;

    [Range(10.0f, 70.0f)] public float startSpinSpeed = 30.0f;
    [Range(70.0f, 150.0f)] public float endSpinSpeed = 90.0f;

    public bool isClockwise = true;

    // Start is called before the first frame update
    void Start()
    {
        spinnerObject = GetComponent<GameObject>();
        spinnerColor = GetComponent<Renderer>().material.color;

        currentSpeed = startSpinSpeed;

        speedDifferenceFactor = (endSpinSpeed - startSpinSpeed) / 10.0f;
        colorDifferenceFactor = spinnerColor.g / 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 5.0f && currentSpeed < endSpinSpeed)
        {
            currentTime = 0.0f;
            currentSpeed += speedDifferenceFactor;

            spinnerColor.g -= colorDifferenceFactor;
            GetComponent<Renderer>().material.color = spinnerColor;
            
            if (spinnerColor.g < 0) spinnerColor.g = 0;
        }

        if (isClockwise) transform.Rotate(currentSpeed * Time.deltaTime, 0f, 0f);

        else transform.Rotate(-currentSpeed * Time.deltaTime, 0f, 0f);

        //print(currentSpeed);
    }
}
