using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IslandConnector 
{

    // Prims Algorithm
    public static List<Vector2Int> CreateConnectionTree(List<Island> islands, List<int>[] oceanIslands)
    {
        List<Vector2Int> connectionTree = new List<Vector2Int>();

        for(int i = 0; i < oceanIslands.Length; i++)
        {
            List<int> islandsToConnect = oceanIslands[i];

            if (islandsToConnect.Count < 2) continue;


            List<Vector2Int> connections = new List<Vector2Int>();
            List<int> connectedIslands = new List<int>();

            int currentIslandId = islandsToConnect[0];
            islandsToConnect.Remove(islandsToConnect[0]);

            int closestIslandId = FindClosestIsland(islands, currentIslandId, islandsToConnect);
            connections.Add(new Vector2Int(currentIslandId, closestIslandId));

            connectedIslands.Add(currentIslandId);
            connectedIslands.Add(closestIslandId);
            islandsToConnect.Remove(currentIslandId);
            islandsToConnect.Remove(closestIslandId);


            while ( islandsToConnect.Count != 0)
            {
                currentIslandId = islandsToConnect[0];

                closestIslandId = FindClosestIsland(islands, currentIslandId, connectedIslands);
                connections.Add(new Vector2Int(currentIslandId, closestIslandId));

                connectedIslands.Add(currentIslandId);
                islandsToConnect.Remove(currentIslandId);
            }

            connectionTree.AddRange(connections);
        }

        return connectionTree;
    }


    private static int FindClosestIsland(List<Island> islands, int currentIsland, List<int> islandsToCompare)
    {
        float closestDistance = float.PositiveInfinity;
        int closestIslandId = 0;

        Vector2Int currentIslandCenter = islands[currentIsland].CenterPointRounded;

        for(int i = 0; i < islandsToCompare.Count; i++)
        {
            int idToCompare = islandsToCompare[i];
            Vector2Int centerToCompare = islands[idToCompare].CenterPointRounded;

            float dist = Vector2.Distance(currentIslandCenter, centerToCompare);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestIslandId = idToCompare;
            }
        }

        return closestIslandId;
    }
}
