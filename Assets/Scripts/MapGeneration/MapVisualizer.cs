using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    public GameObject cubePrefab;

    public void Visualize(MapTile[,] map)
    {
        if (map == null) return;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                GameObject cube = Instantiate(cubePrefab, new Vector3(i, 0, j), Quaternion.identity, this.transform);
                cube.SetActive(map[i, j].IsLand);
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
                GameObject cube = Instantiate(cubePrefab, new Vector3(i, 0, j), Quaternion.identity, this.transform);
                cube.SetActive(map[i, j]);
            }
        }
    }


}
