using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class DIRECTIONS
{
    public static Vector2Int top = new Vector2Int(0, -1);
    public static Vector2Int right = new Vector2Int(1, 0);
    public static Vector2Int bottom = new Vector2Int(0, 1);
    public static Vector2Int left = new Vector2Int(-1, 0);
    public static Vector2Int[] allDirections = { top, right, bottom, left };
};