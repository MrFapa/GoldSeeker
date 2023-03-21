using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    private bool isLand;
    public bool IsLand { get => isLand; set => isLand = value; }

    public MapTile(bool isLand)
    {
        this.isLand = isLand;
    }
}
