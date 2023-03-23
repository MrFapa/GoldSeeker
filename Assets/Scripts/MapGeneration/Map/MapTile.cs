using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    private Vector2Int position;
    public Vector2Int Position { get => position; }

    private bool isLand;
    public bool IsLand { get => isLand; set => isLand = value; }

    private bool hasObstacle;
    public bool HasObstacle { get => hasObstacle; set => hasObstacle = value; }
    public MapTile(Vector2Int position, bool isLand = false, bool hasObstacle = false)
    {
        this.position = position;
        this.isLand = isLand;
        this.hasObstacle = hasObstacle;
    }
}
