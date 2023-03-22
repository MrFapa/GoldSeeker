using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    private Vector2 position;
    public Vector2 Position { get => position; }

    private bool isLand;
    public bool IsLand { get => isLand; set => isLand = value; }

    public MapTile(Vector2 position, bool isLand)
    {
        this.position = position;
        this.isLand = isLand;
    }
}
