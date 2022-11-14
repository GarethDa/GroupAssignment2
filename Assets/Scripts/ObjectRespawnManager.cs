using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Profiling;

public class ObjectRespawnManager : MonoBehaviour
{
    /*
    [Serializable]
    private struct ObjectRespawnType
    {
        public string objectTag;

        public List<Transform> spawnPoints;

        public Material origMat;
    }
    */

    //[SerializeField] private List<ObjectRespawnType> respawnObjects = new List<ObjectRespawnType>();

    public List<Transform> spawnPoints;

    ObjectPool poolInstance;

    float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        poolInstance = ObjectPool.poolManager;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= 7.0f )
        {
            Profiler.BeginSample("Pooling");
            poolInstance.SpawnFromPool("Dodgeball", spawnPoints[0].transform.position, false);

            spawnPoints.Add(spawnPoints[0]);
            spawnPoints.RemoveAt(0);

            spawnTimer = 0f;

            Profiler.EndSample();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
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
        */

        other.gameObject.SetActive(false);

        /*
        Profiler.BeginSample("Ball instantiate");
        GameObject obj = Instantiate(other.gameObject);

        obj.transform.position = spawnPoints[0].transform.position;

        Destroy(other.gameObject);

        spawnPoints.Add(spawnPoints[0]);
        spawnPoints.RemoveAt(0);

        Profiler.EndSample();
        */
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
