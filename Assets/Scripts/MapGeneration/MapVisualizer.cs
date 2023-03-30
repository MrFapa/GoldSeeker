using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    public GameObject landPrefab;
    public GameObject waterPrefab;
    public GameObject rockPrefab;
    public GameObject islandCenterPrefab;
    public GameObject bridgePrefab;

    public void Visualize(MapTile[,] map)
    {
        if (map == null) return;

        GameObject tileHolder = new GameObject("Tiles");
        tileHolder.transform.parent = this.transform;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                GameObject tilePrefab = (map[i, j].IsLand) ? landPrefab : waterPrefab;
                GameObject cube = Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity, tileHolder.transform);

                if (map[i, j].HasObstacle)
                {
                    GameObject rock = Instantiate(rockPrefab, new Vector3(i, 1, j), Quaternion.identity, tileHolder.transform);
                }
            }
        }
    }

    public void Visualize(bool[,] map)
    {
        if (map == null) return;

        GameObject tileHolder = new GameObject("Tiles");
        tileHolder.transform.parent = this.transform;


        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                GameObject tilePrefab = (map[i, j]) ? landPrefab : waterPrefab;
                GameObject cube = Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity, tileHolder.transform);
            }
        }
    }

    public void VisualizeIslandCenters(List<Island> islands)
    {
        GameObject islandHolder = new GameObject("Islands");
        islandHolder.transform.parent = this.transform;
        //islandHolder = Instantiate(islandHolder, Vector3.zero, Quaternion.identity, this.transform);

        foreach (Island island in islands)
        {
            Vector3 position = new Vector3(island.CenterPoint.x, 1, island.CenterPoint.y);
            Instantiate(islandCenterPrefab, position, Quaternion.identity, islandHolder.transform);
        }
    }

    public void VisualizeBridges(List<Bridge> bridges)
    {
        GameObject bridgeHolder = new GameObject("Bridges");
        bridgeHolder.transform.parent = this.transform;
        foreach (Bridge bridge in bridges)
        {
            GameObject singleBridge = new GameObject("Bridge");
            singleBridge.transform.parent = bridgeHolder.transform;
            for (int i = 0; i < bridge.Tiles.Count; i++)
            {
                MapTile currentTile = bridge.Tiles[i];
                Vector3 position = new Vector3(currentTile.Position.x, 1, currentTile.Position.y);
                Instantiate(bridgePrefab, position, Quaternion.identity, singleBridge.transform);
            }
        }
        
    }
}
