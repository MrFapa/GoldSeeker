using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettingsManager : MonoBehaviour
{

    public int mapSize = 64;
    public int islandTh = 6;


    public float waterTh = 0.5f;
    public float noiseScale = 0.1f;
    [Range(1, 1)]
    public int noiseOctaves = 1;



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
