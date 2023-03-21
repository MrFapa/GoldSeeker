using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapVisualizer mapVisualizer;

    private Map map;

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

        this.map = new Map(SimpleNoiseGenerator.GenerateMap());

        mapVisualizer.Visualize(map.TilesMap);

    }
}
