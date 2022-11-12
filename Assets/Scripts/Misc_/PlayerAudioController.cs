using GameAudioScriptingEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public float timeBetweenFootsteps = 0.5f;
    float soundCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        soundCounter += Time.deltaTime;
        if (soundCounter > timeBetweenFootsteps)
        {
            soundCounter = 0;
            if(GetComponent<Rigidbody>().velocity.magnitude > 0.5)
            {
                GetComponent<AudioClipRandomizer>().PlaySFX();
            }
            
        }
    }
}
