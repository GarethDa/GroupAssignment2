using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool poolManager;

    [Serializable]
    public class Pool
    {
        public string objectName;
        public GameObject objectPrefab;
        public int size;
    }

    public List<Pool> objectPools = new List<Pool>();

    public Dictionary<string, Queue<GameObject>> poolDict;

    private void Awake()
    {
        if (poolManager == null)
        {
            poolManager = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in objectPools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.objectPrefab);
                obj.SetActive(false);

                objPool.Enqueue(obj);
            }

            poolDict.Add(pool.objectName, objPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFromPool(string poolName, Vector3 position, bool overMax)
    {
        //Grab the object pool of name poolName
        Queue<GameObject> objectPool; 
        
        poolDict.TryGetValue(poolName, out objectPool);

        //Create a temp pool variable
        Pool thisPool = new Pool();

        //Look through each pool in the list
        foreach (Pool pool in objectPools)
        {
            //If we find a pool that has the desired name, grab it and break
            if (pool.objectName.Equals(poolName))
            {
                thisPool = pool;
                break;
            }
        }

        //If there are objects in the pool
        if (objectPool.Count > 0)
        {
            //Dequeue the object at the front
            GameObject spawnedObject = objectPool.Dequeue();

            //If the object isn't active, activate it and set its position to the input position
            if (!spawnedObject.activeInHierarchy)
            {
                spawnedObject.SetActive(true);
                spawnedObject.transform.position = position;

                if (spawnedObject.GetComponent<Rigidbody>() != null)
                {
                    spawnedObject.GetComponent<Rigidbody>().velocity = new Vector3();
                }
            }

            //Else, the object at the front is active
            else
            {
                bool objectUsed = false;

                //Look through the whole pool
                for (int i = 0; i < objectPool.Count; i++)
                {
                    GameObject tempObj = objectPool.Dequeue();

                    //If we find an inactive element, activate it and set its position to the input position
                    if (!tempObj.activeInHierarchy)
                    {
                        tempObj.transform.position = position;
                        tempObj.SetActive(true);

                        if (tempObj.GetComponent<Rigidbody>() != null)
                        {
                            tempObj.GetComponent<Rigidbody>().velocity = new Vector3();
                        }

                        //Add the object back to the pool and break, since we found what we needed
                        objectPool.Enqueue(tempObj);
                        objectUsed = true;
                        break;
                    }

                    //Make sure to add each object back to the pool after looking at it
                    objectPool.Enqueue(tempObj);
                }

                //If we didn't find an inactive element previously
                if (!objectUsed)
                {

                    if (overMax)
                    {
                        //Instantiate a new object of the type in the pool, and add one to the size
                        GameObject obj = Instantiate(thisPool.objectPrefab);
                        thisPool.size++;

                        //Set the object position to the input position
                        obj.transform.position = position;

                        if (obj.GetComponent<Rigidbody>() != null)
                        {
                            obj.GetComponent<Rigidbody>().velocity = new Vector3();
                        }

                        //Add the object to the pool
                        objectPool.Enqueue(obj);
                    }
                }
            }

            //Make sure to add the original object we grabbed back to the pool
            objectPool.Enqueue(spawnedObject);
        }
    }
}
