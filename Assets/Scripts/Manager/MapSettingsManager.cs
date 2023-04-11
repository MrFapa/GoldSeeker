using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettingsManager : MonoBehaviour
{
    [Header("Seed")]

    public bool randomSeed;
    public int seed;

    [Header("Overall Settings")]
    public int mapSize = 64;
    public int islandTh = 6;
    public Vector2Int[] islandSizes;

    [Header("Noise Generation")]
    public SimpleNoiseSettings baseMapSettings;
    public SimpleNoiseSettings waterObstacleSettings;


    private static MapSettingsManager _instance;
    public static MapSettingsManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MapSettingsManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        if (randomSeed)
        {
            this.seed = Random.Range(0, 100000);
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
    }
}

[System.Serializable]
public struct SimpleNoiseSettings
{
    public float threshold;
    public float scale;
    [Range(1, 4)]
    public int octaves;
}
