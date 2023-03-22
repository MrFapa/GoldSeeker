using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island 
{
    private List<MapTile> tiles;
    public List<MapTile> Tiles { get => tiles; }

    public int IslandSize { get => tiles.Count; }

    private Vector2 centerPoint;
    public Vector2 CenterPoint
    {
        get
        {
            if(centerPoint == null)
            {
                CalculateCenterPoint();
            }
            return centerPoint;
        }
    }

    public Vector2Int CenterPointRounded
    {
        get
        {
            if (centerPoint == null)
            {
                CalculateCenterPoint();
            }

            return new Vector2Int(Mathf.RoundToInt(centerPoint.x), Mathf.RoundToInt(centerPoint.y));
        }
    }

    public Island (List<MapTile> tiles)
    {
        this.tiles = tiles;
    }

    private void CalculateCenterPoint()
    {
        Vector2 newCenterPoint = new Vector2();
        for(int i = 0; i < IslandSize; i++)
        {
            newCenterPoint += tiles[i].Position;
        }
        newCenterPoint = newCenterPoint / IslandSize;
        this.centerPoint = newCenterPoint;
    }
}
