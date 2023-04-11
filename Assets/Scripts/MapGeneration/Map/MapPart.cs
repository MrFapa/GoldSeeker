using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPart 
{
    protected List<MapTile> tiles;
    public List<MapTile> Tiles { get => tiles; }

    public int Size { get => tiles.Count; }

    public MapPart (List<MapTile> tiles)
    {
        this.tiles = tiles;
    }
}
