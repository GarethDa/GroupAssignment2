using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectRespawnManager : MonoBehaviour
{
    [Serializable]
    private struct ObjectRespawnType
    {
        public string objectTag;

        public List<Transform> spawnPoints;

        public Material origMat;
    }

    [SerializeField] private List<ObjectRespawnType> respawnObjects = new List<ObjectRespawnType>();

    // Start is called before the first frame update
    void Start()
    {
        /*
        //To start, transfer all of the transforms in each object type's spawn point list to a queue,
        //since a queue is more useful for what we want
        foreach (ObjectRespawnType objectType in respawnObjects)
        {
            //objectType.spawnQueue = new Queue<Transform>();

            foreach(Transform spawnPoint in objectType.spawnPoints)
            {
                Debug.Log(objectType.spawnQueue);

                //objectType.spawnQueue.Enqueue(spawnPoint);
            }
        }

        for (int i = 0; i < respawnObjects.Count; i++)
        {
            //respawnObjects[i].spawnPoints = new List<Transform>();
        }
        */
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (ObjectRespawnType objectType in respawnObjects)
        {
            if (other.gameObject.tag.Equals(objectType.objectTag))
            {
                if (objectType.spawnPoints.Count > 0)
                {
                    Debug.Log("here");

                    Transform tempTrans = objectType.spawnPoints[0];

                    GameObject newObj = Instantiate(other.gameObject);

                    newObj.transform.position = tempTrans.position;

                    if (newObj.GetComponent<Renderer>() == null) newObj.transform.GetChild(0).GetComponent<Renderer>().material = objectType.origMat;

                    else newObj.GetComponent<Renderer>().material = objectType.origMat;

                    Destroy(other.gameObject);

                    //other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                    //other.gameObject.transform.position = tempTrans.position;

                    objectType.spawnPoints.RemoveAt(0);
                    objectType.spawnPoints.Add(tempTrans);
                }
                break;
            }
        }
    }

    /*
    private void O
    {
        Debug.Log("Here");
        foreach(ObjectRespawnType objectType in respawnObjects)
        {
            if (collision.gameObject.tag.Equals(objectType.objectTag))
            {
                if (objectType.spawnPoints.Count > 0)
                {
                    Debug.Log("here");

                    Transform tempTrans = objectType.spawnPoints[0];

                    collision.gameObject.transform.position = tempTrans.position;

                    objectType.spawnPoints.RemoveAt(0);
                    objectType.spawnPoints.Add(tempTrans);
                }
                break;
            }
        }    
    }
    */

}
