using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    private Vector2Int position;
    public Vector2Int Position { get => position; }

    private TileType type;
    public TileType Type { get => type; set => type = value; }

    private TileTopping topping;
    public TileTopping Topping { get => topping; set => topping = value; }

    private int islandID;

    public int IslandID { get => islandID; set => islandID = value; }
    public MapTile(Vector2Int position, TileType type = TileType.undefined, TileTopping topping = TileTopping.nothing)
    {
        this.position = position;
        this.type = type;
        this.topping = topping;
    }

    public MapTile() : this(new Vector2Int(0, 0)) { }
}
