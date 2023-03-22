using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{

    private bool[,] primitiveMap;
    public bool[,] PrimitiveMap { get => primitiveMap; set => primitiveMap = value; }

    private MapTile[,] tilesMap;
    public MapTile[,] TilesMap { get => tilesMap; set => tilesMap = value; }

    private List<Island> islands;
    public List<Island> Islands { get => islands; }

    public int Size { get => primitiveMap.GetLength(0); }

    public Map(bool[,] map)
    {
        this.primitiveMap = map;
        this.tilesMap = new MapTile[Size, Size];
        InitMap();

        this.islands = new List<Island>();
        InitIslands();
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
                this.tilesMap[i, j] = new MapTile(new Vector2(i,j), this.primitiveMap[i, j]);
            }
        }
        return this.tilesMap;
    }

    public void InitIslands()
    {
        List<MapTile> unoberserved = new List<MapTile>();


        // All land tiles into unobserved, ignore water tiles
        for (int i = 0; i < this.Size; i++)
        {
            for (int j = 0; j < this.Size; j++)
            {
                if(this.tilesMap[i, j].IsLand)
                {
                    unoberserved.Add(this.tilesMap[i, j]);
                } 
            }
        }

        List<Island> newIslands = new List<Island>();

        while (unoberserved.Count > 0)
        {
            MapTile newIslandTile = unoberserved[0];
            List<MapTile> newIslandTiles = new List<MapTile>();
            Vector2 islandCenterPoint = new Vector2();

            Stack<MapTile> toCheck = new Stack<MapTile>();
            toCheck.Push(newIslandTile);

            while(toCheck.Count > 0)
            {
                MapTile currentTile = toCheck.Pop();
                MapTile[] neighbours = GetNeighbouringTiles(currentTile);

                for(int i = 0; i < neighbours.Length; i++)
                {
                    MapTile currentNeighbour = neighbours[i];
                    if (!currentNeighbour.IsLand || toCheck.Contains(currentNeighbour) || newIslandTiles.Contains(currentNeighbour))
                    {
                        continue;
                    }
                    toCheck.Push(currentNeighbour);
                }
                islandCenterPoint += currentTile.Position;
                newIslandTiles.Add(currentTile);
                unoberserved.Remove(currentTile);
            }

            if (newIslandTiles.Count > MapSettingsManager.Instance.islandTh)
            {
                islandCenterPoint = islandCenterPoint / newIslandTiles.Count;
                Island newIsland = new Island(newIslandTiles, islandCenterPoint);
                newIslands.Add(newIsland);
            }

        }

        this.islands = newIslands;
    }


    #region Helpers

    public MapTile GetNeighbourTile(MapTile tile, Vector2 direction) 
    {
        int x = (int) (tile.Position.x + direction.x);
        int y = (int) (tile.Position.y + direction.y);

        MapTile neighbour = (x >= Size || x < 0 || y >= Size || y < 0) ? new MapTile(new Vector2(x, y), false) : this.tilesMap[x, y];
        return neighbour;
    }

    public MapTile[] GetNeighbouringTiles (MapTile tile)
    {
        MapTile[] neighbours = new MapTile[4];
        for(int i = 0; i < DIRECTIONS.allDirections.Length; i++)
        {
            Vector2 direction = DIRECTIONS.allDirections[i];
            neighbours[i] = GetNeighbourTile(tile, direction);
        }

        return neighbours;
    }


    public static class DIRECTIONS
    {
        public static Vector2 top = new Vector2(0, -1);
        public static Vector2 right = new Vector2(1, 0);
        public static Vector2 bottom = new Vector2(0, 1);
        public static Vector2 left = new Vector2(-1, 0);
        public static Vector2[] allDirections = { top, right, bottom, left };
    };

    #endregion
}

