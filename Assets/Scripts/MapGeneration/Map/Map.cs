using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map
{

    private bool[,] primitiveMap;
    public bool[,] PrimitiveMap { get => primitiveMap; set => primitiveMap = value; }

    private bool[,] stoneMap;
    public bool[,] StoneMap { get => stoneMap; set => stoneMap = value; }

    private MapTile[,] tilesMap;
    public MapTile[,] TilesMap { get => tilesMap; set => tilesMap = value; }


    private List<Island> islands;
    public List<Island> Islands { get => islands; }

    private List<Ocean> oceans;
    public List<Ocean> Oceans { get => oceans; }

    private List<Bridge> bridges;
    public List<Bridge> Bridges { get => bridges; }


    public int Size { get => primitiveMap.GetLength(0); }

    public Map(bool[,] map, bool[,] obstacleMap)
    {
        this.primitiveMap = map;
        this.stoneMap = obstacleMap;
        this.tilesMap = new MapTile[Size, Size];

        this.islands = new List<Island>();
        this.bridges = new List<Bridge>();
        this.oceans = new List<Ocean>();
    }

    public void InitMap()
    {
        InitTiles();
        InitIslands();
        InitOceans();
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
                TileTopping topping = (stoneMap[i, j] && !primitiveMap[i, j]) ? TileTopping.stone : TileTopping.nothing;
                TileType type = this.primitiveMap[i, j] ? TileType.land : TileType.water;
                this.tilesMap[i, j] = new MapTile(new Vector2Int(i,j), type, topping);
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
               if(this.tilesMap[i, j].Type == TileType.land)
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
                    if (currentNeighbour.Type == TileType.water || currentNeighbour.Type == TileType.undefined || toCheck.Contains(currentNeighbour) || newIslandTiles.Contains(currentNeighbour))
                    {
                        continue;
                    }
                    toCheck.Push(currentNeighbour);
                }
                islandCenterPoint += currentTile.Position;
                newIslandTiles.Add(currentTile);

                // ID of Island is the count of the islands, count = 0 means no island and the current island is the first
                this.tilesMap[currentTile.Position.x, currentTile.Position.y].IslandID = newIslands.Count;
               
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


    // ------- TODO -------
    // reduce redundance
    // --------------------

    public void InitOceans()
    {
        List<MapTile> unoberserved = new List<MapTile>();

        // only check water tiles
        for (int i = 0; i < this.Size; i++)
        {
            for (int j = 0; j < this.Size; j++)
            {
                if (this.tilesMap[i, j].Type == TileType.water)
                {
                    unoberserved.Add(this.tilesMap[i, j]);
                }
            }
        }


        List<Ocean> newOceans = new List<Ocean>();

        while (unoberserved.Count > 0)
        {
            MapTile newOceanTile = unoberserved[0];
            List<MapTile> newOceanTiles = new List<MapTile>();
            List<int> islandIDs = new List<int>();

            Stack<MapTile> toCheck = new Stack<MapTile>();
            toCheck.Push(newOceanTile);

            while (toCheck.Count > 0)
            {
                MapTile currentTile = toCheck.Pop();
                MapTile[] neighbours = GetNeighbouringTiles(currentTile);

                for (int i = 0; i < neighbours.Length; i++)
                {
                    MapTile currentNeighbour = neighbours[i];

                    if (currentNeighbour.Type == TileType.land)
                    {
                        if (!islandIDs.Contains(currentNeighbour.IslandID))
                        {
                            islandIDs.Add(currentNeighbour.IslandID);
                        }
                        continue;
                    }

                    if (currentNeighbour.Type == TileType.undefined || toCheck.Contains(currentNeighbour) || newOceanTiles.Contains(currentNeighbour))
                    {


                        continue;
                    }
                    toCheck.Push(currentNeighbour);
                }
                newOceanTiles.Add(currentTile);

                unoberserved.Remove(currentTile);
            }

            Ocean newOcean = new Ocean(newOceanTiles, islandIDs);
            newOceans.Add(newOcean);

        }

        this.oceans = newOceans;
    }

    public void InitBridges()
    {
        List<int>[] oceanIslandsIds = new List<int>[this.oceans.Count];
        for (int i = 0; i < this.oceans.Count; i++)
        {
            oceanIslandsIds[i] = this.oceans[i].IslandIDs;
        }

        List<Vector2Int> connections = IslandConnector.CreateConnectionTree(this.islands, oceanIslandsIds);
        foreach (Vector2Int connection in connections)
        {
            Island islandA = this.islands[connection.x];
            Island islandB = this.islands[connection.y];
            
            MapTile start = islandA.FindClosestMapTileTo(this.tilesMap[islandB.CenterPointRounded.x, islandB.CenterPointRounded.y]);
            MapTile end = islandB.FindClosestMapTileTo(this.tilesMap[islandA.CenterPointRounded.x, islandA.CenterPointRounded.y]);
            Bridge newBridge = new Bridge(start, end);
            this.bridges.Add(newBridge);
        }

        foreach (Bridge bridge in this.bridges)
        {
            foreach(MapTile tile in bridge.Tiles)
            {
                this.tilesMap[tile.Position.x, tile.Position.y].Topping = TileTopping.bridge;
            }
        }
    }


    #region Helpers

    public MapTile GetNeighbourTile(MapTile tile, Vector2Int direction) 
    {
        int x = (int) (tile.Position.x + direction.x);
        int y = (int) (tile.Position.y + direction.y);

        MapTile neighbour = (x >= Size || x < 0 || y >= Size || y < 0) ? new MapTile(new Vector2Int(x, y), TileType.undefined) : this.tilesMap[x, y];
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

