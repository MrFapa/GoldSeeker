using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    public RuleTile landTile;
    public Tilemap landTilemap;
    public RuleTile rockTile;
    public Tilemap rockTilemap;
    public RuleTile bridgeTile;
    public Tilemap bridgeTilemap;

    public void BuildMap(Map map)
    {
        int size = map.Size;
        
        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (map.TilesMap[i, j].Type == TileType.land)
                {
                    landTilemap.SetTile(new Vector3Int(i, j, 0), landTile);
                }
                if (map.TilesMap[i, j].Topping == TileTopping.stone)
                {
                    rockTilemap.SetTile(new Vector3Int(i, j, 0), rockTile);
                }
                if (map.TilesMap[i, j].Topping == TileTopping.bridge)
                {
                    rockTilemap.SetTile(new Vector3Int(i, j, 0), bridgeTile);
                }
            }
        }
    }
}
