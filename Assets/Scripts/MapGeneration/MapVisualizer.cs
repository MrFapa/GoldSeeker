using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    public GameObject landPrefab;
    public GameObject waterPrefab;
    public GameObject islandCenterPrefab;

    public void Visualize(MapTile[,] map)
    {
        if (map == null) return;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                GameObject tilePrefab = (map[i, j].IsLand) ? landPrefab : waterPrefab;
                GameObject cube = Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity, this.transform);
            }
        }
    }

    public void Visualize(bool[,] map)
    {
        if (map == null) return;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                GameObject tilePrefab = (map[i, j]) ? landPrefab : waterPrefab;
                GameObject cube = Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity, this.transform);
            }
        }
    }

    public void VisualizeIslandCenters(List<Island> islands)
    {
        foreach (Island island in islands)
        {
            Vector3 position = new Vector3(island.CenterPoint.x, 1, island.CenterPoint.y);
            Instantiate(islandCenterPrefab, position, Quaternion.identity);
        }
    }
}
