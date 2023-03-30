using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map
{

    private bool[,] primitiveMap;
    public bool[,] PrimitiveMap { get => primitiveMap; set => primitiveMap = value; }

    private bool[,] obstacleMap;
    public bool[,] ObstacleMap { get => obstacleMap; set => obstacleMap = value; }

    private MapTile[,] tilesMap;
    public MapTile[,] TilesMap { get => tilesMap; set => tilesMap = value; }


    private List<Island> islands;
    public List<Island> Islands { get => islands; }

    private List<Bridge> bridges;
    public List<Bridge> Bridges { get => bridges; }


    public int Size { get => primitiveMap.GetLength(0); }

    public Map(bool[,] map, bool[,] obstacleMap)
    {
        this.primitiveMap = map;
        this.obstacleMap = obstacleMap;
        this.tilesMap = new MapTile[Size, Size];

        this.islands = new List<Island>();
        this.bridges = new List<Bridge>();

    }

    public void InitMap()
    {
        InitTiles();
        InitIslands();
        InitBridges();
    }

    public MapTile[,] InitTiles()
    {
        if(this.primitiveMap == null)
        {
            return null;
        }

        for(int i = 0; i < this.Size; i++)
        {
            for (int j = 0; j < this.Size; j++)
            {
                bool waterObstacle = obstacleMap[i, j] && !primitiveMap[i, j];
                this.tilesMap[i, j] = new MapTile(new Vector2Int(i,j), this.primitiveMap[i, j], waterObstacle);
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

    public void InitBridges()
    {
        this.bridges = IslandConnector.ConnectIslands(this.islands);
    }


    #region Helpers

    public MapTile GetNeighbourTile(MapTile tile, Vector2Int direction) 
    {
        int x = (int) (tile.Position.x + direction.x);
        int y = (int) (tile.Position.y + direction.y);

        MapTile neighbour = (x >= Size || x < 0 || y >= Size || y < 0) ? new MapTile(new Vector2Int(x, y), false) : this.tilesMap[x, y];
        return neighbour;
    }

    public MapTile[] GetNeighbouringTiles (MapTile tile)
    {
        MapTile[] neighbours = new MapTile[4];
        for(int i = 0; i < DIRECTIONS.allDirections.Length; i++)
        {
            Vector2Int direction = DIRECTIONS.allDirections[i];
            neighbours[i] = GetNeighbourTile(tile, direction);
        }

        return neighbours;
    }

    #endregion
}

