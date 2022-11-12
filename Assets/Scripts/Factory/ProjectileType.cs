using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileType : MonoBehaviour
{
    public abstract string Name { get; }

    public abstract GameObject Create(GameObject prefab);
}

public class Dodgeball : ProjectileType
{
    public override string Name => "dodgeball";

    public override GameObject Create(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab);
        //Debug.Log
        return newObject;
    }
}

public class Pachinko : ProjectileType
{
    public override string Name => "pachinko";

    public override GameObject Create(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab);
        //Debug.Log
        return newObject;
    }
}