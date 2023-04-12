using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePropertyInfosManager : MonoBehaviour
{

    [Header("Different Tile Types")]
    [Space(5)]
    [SerializeField]
    private TileTypeInfo landInfo;
    
    public Dictionary<TileType, TileTypeInfo> tileTypeInfoDictonary;

    [Space(10)]
    [Header("Different Tile Toppings")]
    [Space(5)]
    [SerializeField]
    private TileToppingInfo stoneInfo;
    [SerializeField]
    private TileToppingInfo bridgeInfo;

    public Dictionary<TileTopping, TileToppingInfo> tileToppingInfoDictonary;

    private static TilePropertyInfosManager _instance;
    public static TilePropertyInfosManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<TilePropertyInfosManager>();
            return _instance;
        }
    }
    private void Awake()
    {
        this.tileTypeInfoDictonary = new Dictionary<TileType, TileTypeInfo>();
        this.tileTypeInfoDictonary.Add(TileType.land, this.landInfo);

        this.tileToppingInfoDictonary = new Dictionary<TileTopping, TileToppingInfo>();
        this.tileToppingInfoDictonary.Add(TileTopping.stone, this.stoneInfo);
        this.tileToppingInfoDictonary.Add(TileTopping.bridge, this.bridgeInfo);
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
public struct TileTypeInfo
{
    public int voxelsInHeight;
    public float height { get => voxelsInHeight * 0.025f; }

    public RuleTile ruletile;
}

[System.Serializable]
public struct TileToppingInfo
{
    public RuleTile ruletile;

    public bool passable;
}