using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;
    public Vector2Int pos;

    public int gCost;
    public int hCost;

    public Node(Vector2Int pos)
    {
        this.pos = pos;
    }
    public int fCost { get => hCost + gCost; }

}
