using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlighter : MonoBehaviour
{
    [SerializeField]
    private Transform highligter;


    private Dictionary<TileType, TileTypeInfo> tileInfos;

    private Vector2Int currentTilePos;
    private Vector2Int lastTilePos;

    private Ray ray;
    private void Start()
    {
        this.tileInfos = TilePropertyInfosManager.Instance.tileTypeInfoDictonary;
        this.currentTilePos = new Vector2Int();
        this.lastTilePos = new Vector2Int();
    }

    void Update()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            this.currentTilePos = new Vector2Int(Mathf.RoundToInt(objectHit.position.x), Mathf.RoundToInt(objectHit.position.z));
            MapTile currentTile = MapManager.Instance.Map.GetTile(this.currentTilePos);
            TileTypeInfo info;
            this.tileInfos.TryGetValue(currentTile.Type, out info);

            Vector3 heightOffset = (info.ruletile != null) ? new Vector3(0, info.height, 0) : Vector3.zero;
            this.highligter.position = objectHit.position + heightOffset;
        }
    }
}
