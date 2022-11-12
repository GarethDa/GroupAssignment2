using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatBehaviour : MonoBehaviour
{
    [Range(0.5f, 3.0f)] public float wobbleTime = 1.5f;
    [Range(1.0f, 10.0f)] public float fallSpeed = 3.0f;

    public GameObject platformObject;

    bool m_SwitchedStates = false;
    float m_StateTime = 0.0f;

    Animator m_Animator;
    Renderer m_Renderer;

    Color m_YellowColor;
    Color m_RedColour;

    //Platform states
    enum platStates
    {
        untouched = 0,
        shaking = 1,
        falling = 2
    }

    platStates m_ThisState = platStates.untouched;


    // Start is called before the first frame update
    void Start()
    {
        //Grab the renderer and animators
        m_Renderer = platformObject.GetComponent<Renderer>();
        m_Animator = platformObject.GetComponent<Animator>();

        //Variables for holding colours
        m_RedColour = new Color(0.80f, 0.48f, 0.45f);
        m_YellowColor = new Color(0.88f, 0.89f, 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        //For switching states
        switch(m_ThisState)
        {
            //If untouched, do nothing
            case platStates.untouched:
                {
                    break;
                }

            //If shaking
            case platStates.shaking:
                {
                    //If we just switched states, start animating and change the colour
                    if (m_SwitchedStates)
                    {
                        m_SwitchedStates = false;
                        m_StateTime = 0.0f;
                        m_Animator.SetBool("IsWobbling", true);

                        //Grab the renderer, set the material colour to yellow now that the platform was started to wobble
                        m_Renderer.material.color = m_YellowColor;
                    }

                    m_StateTime += Time.deltaTime;

                    //If we have reached the maximum wobble time, then move to falling
                    if (m_StateTime >= wobbleTime)
                    {
                        m_ThisState = platStates.falling;
                        m_SwitchedStates = true;
                    }

                    break;
                }
            
            //If shaking
            case platStates.falling:
                {
                    //If we just switched states, start animating and change the colour
                    if (m_SwitchedStates)
                    {
                        m_SwitchedStates = false;
                        m_Animator.SetBool("IsWobbling", false);

                        //Grab the renderer, set the material colour to red now that the platform has started to fall
                        m_Renderer.material.color = m_RedColour;
                    }

                    //Fall
                    transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * fallSpeed, transform.position.z);

                    break;
                }

            default: break;

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //If the player touches a platform and said platform isn't already shaking or falling
        if (other.gameObject.tag == "Player" && m_ThisState == platStates.untouched)
        {
            //Start shaking state
            m_ThisState = platStates.shaking;
            m_SwitchedStates = true;
        }
    }
}
