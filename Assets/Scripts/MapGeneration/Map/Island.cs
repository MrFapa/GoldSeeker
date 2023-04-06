using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MapPart
{
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
            return new Vector2Int(Mathf.RoundToInt(CenterPoint.x), Mathf.RoundToInt(CenterPoint.y));
        }
    }

    public Island (List<MapTile> tiles, Vector2 centerPoint = default(Vector2)) : base(tiles) 
    {
        this.centerPoint = centerPoint;
    }

    private void CalculateCenterPoint()
    {
        Vector2 newCenterPoint = new Vector2();
        for(int i = 0; i < Size; i++)
        {
            newCenterPoint += tiles[i].Position;
        }
        newCenterPoint = newCenterPoint / Size;
        this.centerPoint = newCenterPoint;
    }

    public MapTile FindClosestMapTileTo(MapTile tile)
    {
        return FindClosestMapTileTo(tile.Position);
    }

    public MapTile FindClosestMapTileTo(Vector2Int dest)
    {
        MapTile closestTile = null;
        float t = 0;
        Vector2 shiftedCenter = this.centerPoint - dest + this.centerPoint;
        while (closestTile == null)
        {

            Vector2Int pos = MathLib.VectorToVectorInt(Vector2.Lerp((Vector2) dest, shiftedCenter, t));
            foreach(MapTile tile in this.tiles)
            {
                if (pos == tile.Position)
                {
                    return tile;
                }
            }

            t += 0.001f;
            if (t > 1.5f) break;
        }

        return default(MapTile);
    }
}
