using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class IslandConnector 
{
   public static List<Bridge> ConnectIslands(List<Island> islands)
    {
        List<Bridge> bridges = new List<Bridge>();

        List<Island> sortedIslands = islands.OrderBy(obj => obj.IslandSize).ToList();
        int[] islandBridgeCount = new int[sortedIslands.Count];
        int[] islandMaxBridges = GetIslandBridgeCapacity(islands);

        for(int i = 0; i < sortedIslands.Count - 1; i++)
        {
            if (islandBridgeCount[i] >= islandMaxBridges[i])
            {
                continue;
            }

            MapTilePair[] bridgeEnds = new MapTilePair[sortedIslands.Count - 1 - i];
            Island currentIsland = sortedIslands[i];

            for(int j = 0; j < sortedIslands.Count - 1 - i; j++)
            {


                int islandIndexToMeasure = j + i + 1;

                if (islandBridgeCount[islandIndexToMeasure] >= islandMaxBridges[islandIndexToMeasure])
                {
                    continue;
                }

                Island islandToMeasure = sortedIslands[islandIndexToMeasure];
                MapTile currentIslandTile = currentIsland.FindClosestMapTileTo(islandToMeasure.CenterPointRounded);
                MapTile measueredIslandTile = islandToMeasure.FindClosestMapTileTo(currentIsland.CenterPointRounded);

                MapTilePair currentPair = new MapTilePair(currentIslandTile, measueredIslandTile);
                bridgeEnds[j] = currentPair;
            }

            bridgeEnds.OrderBy(obj => obj.Distance());

            for(int j = 0; j < islandMaxBridges[i] - islandBridgeCount[i]; j++)
            {
                if (j >= bridgeEnds.Length) break;
                MapTilePair currentPair = bridgeEnds[j];
                bridges.Add(new Bridge(currentPair.start, currentPair.end));
            }

        }

        return bridges;
    }


    private static int[] GetIslandBridgeCapacity(List<Island> islands)
    {
        int[] bridgeCapacities = new int[islands.Count];
        Vector2Int[] capacityAndThresholds = MapSettingsManager.Instance.islandSizes;

        for(int islandIndex = 0; islandIndex < bridgeCapacities.Length; islandIndex++)
        {
            Island currentIsland = islands[islandIndex];
            bridgeCapacities[islandIndex] = 1;
            for(int j = 0; j < capacityAndThresholds.Length; j++)
            {
                int minSize = capacityAndThresholds[j].x;
                if (currentIsland.IslandSize > minSize)
                {
                    bridgeCapacities[islandIndex] = capacityAndThresholds[j].y;
                }
            }
        }

        return bridgeCapacities;
    }
}

public class MapTilePair
{
    public MapTile start;
    public MapTile end;

    public MapTilePair(MapTile start, MapTile end)
    {
        this.start = start;
        this.end = end;
    }

    public float Distance()
    {
        return Vector2.Distance(start.Position, end.Position);
    }
}
