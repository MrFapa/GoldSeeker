using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MapPart
{

    private List<int> islandIDs;

    public List<int> IslandIDs { get => this.islandIDs; }

    public Ocean(List<MapTile> tiles, List<int> islands) : base(tiles) 
    {
        this.islandIDs = islands;
    }
}
