using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapVisualizer mapVisualizer;

    public MapBuilder mapBuilder;

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

    void Awake()
    {
        this.waterObstacles = SimpleNoiseGenerator.GenerateMap(MapSettingsManager.Instance.waterObstacleSettings);
        this.map = new Map(SimpleNoiseGenerator.GenerateMap(MapSettingsManager.Instance.baseMapSettings), this.waterObstacles);
        this.map.InitMap();

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


        this.mapBuilder.BuildMap(this.map);
    }

    public Vector3 GetRandomSpawn()
    {
        int size = this.map.Size;

        MapTile tile = new MapTile();

        while(tile.Type != TileType.land)
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);

            tile = this.map.GetTile(new Vector2Int(x, y));

        }

        return new Vector3(tile.Position.x, 0, tile.Position.y);
    }
}
