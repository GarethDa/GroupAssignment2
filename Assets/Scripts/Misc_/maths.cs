using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class maths 
{
    public static float nfmod(float a, float b)
    {
        return (Mathf.Abs(a * b) + a) % b;
    }

    public static int nfmod(int a, int b)
    {
        return (Mathf.Abs(a * b) + a) % b;
    }
}
