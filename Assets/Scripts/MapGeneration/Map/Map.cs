using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{

    private bool[,] primitiveMap;
    public bool[,] PrimitiveMap { get => primitiveMap; set => primitiveMap = value; }

    private MapTile[,] tilesMap;
    public MapTile[,] TilesMap { get => tilesMap; set => tilesMap = value; }
    public int Size { get => primitiveMap.GetLength(0); }

    public Map(bool[,] map)
    {
        this.primitiveMap = map;
        this.tilesMap = new MapTile[Size, Size];
        InitMap();
    }

    public MapTile[,] InitMap()
    {
        if(this.primitiveMap == null)
        {
            return null;
        }

        for(int i = 0; i < this.Size; i++)
        {
            for (int j = 0; j < this.Size; j++)
            {
                this.tilesMap[i, j] = new MapTile(this.primitiveMap[i, j]);
            }
        }
        return this.tilesMap;
    }
}
