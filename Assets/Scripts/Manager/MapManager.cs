using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapVisualizer mapVisualizer;

    private Map map;

    public Map Map
    {
        get => map;
    }

    private bool[,] waterObstacles;

    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MapManager>();
            return _instance;
        }
    }
    void Start()
    {
        if (_instance != null)
        {
            if (_instance != this)
                Destroy(this);
        }
        else
        {
            _instance = this;
        }

        this.waterObstacles = SimpleNoiseGenerator.GenerateMap(MapSettingsManager.Instance.waterObstacleSettings);
        this.map = new Map(SimpleNoiseGenerator.GenerateMap(MapSettingsManager.Instance.baseMapSettings), this.waterObstacles);
        this.map.InitMap();

        mapVisualizer.Visualize(this.map.TilesMap);
        mapVisualizer.VisualizeIslandCenters(this.map.Islands);
        mapVisualizer.VisualizeBridges(this.map.bridge);
    }
}
