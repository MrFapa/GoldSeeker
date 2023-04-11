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
        List<MapTile> tiles = new List<MapTile>();
        
        List<Vector2Int> path = Construct(start, end);
        if (path.Count > 0)
        {
            tiles.Add(start);
            foreach (Vector2Int pos in path)
            {
                tiles.Add(new MapTile(pos));
            }
        }
        this.tiles = tiles;
    }


    public List<Vector2Int> Construct(MapTile start, MapTile end)
    {

        bool[,] passableGrid = GridCreater.CreateGrid(ACCEPTABLE.bridgeTypes, ACCEPTABLE.bridgeToppings);
        List<Vector2Int> bridgePath = AStar.GeneratePath(start.Position, end.Position, passableGrid);
        
        return bridgePath;
    }
}
