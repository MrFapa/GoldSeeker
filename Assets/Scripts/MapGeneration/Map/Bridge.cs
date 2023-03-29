using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge 
{
    private List<MapTile> tiles;
    public List<MapTile> Tiles { get => tiles; }
    public int Length { get => tiles.Count; }

    public MapTile Start
    {
        get
        {
            return (this.tiles != null) ? tiles[0] : default(MapTile);
        }
    }
    public MapTile End
    {
        get
        {
            return (this.tiles != null) ? tiles[Length - 1] : default(MapTile);
        }
    }

    public Bridge(MapTile start, MapTile end)
    {
        
        List<Vector2Int> path = Construct(start, end);
        List<MapTile> tiles = new List<MapTile>();
        tiles.Add(start);
        foreach(Vector2Int pos in path)
        {
            tiles.Add(new MapTile(pos));
        }
        this.tiles = tiles;
    }


    public List<Vector2Int> Construct(MapTile start, MapTile end)
    {
        List<Vector2Int> bridgePath = AStar.GeneratePath(start.Position, end.Position, ArrayHandler.Invert(MapManager.Instance.Map.ObstacleMap));
        
        return bridgePath;
    }
}
