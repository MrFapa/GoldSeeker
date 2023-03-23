using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathLib
{
   
    public static Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }

    public static Vector2Int VectorToVectorInt(Vector2 vector)
    {
        return new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }

    public static Vector3 RoundVector(Vector3 vector)
    {
        return new Vector3(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }
}
