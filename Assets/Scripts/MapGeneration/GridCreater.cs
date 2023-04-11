using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridCreater
{
    public static bool[,] CreateGrid(TileType[] acceptableTypes, TileTopping[] acceptableToppings)
    {
        MapTile[,] tileMap = MapManager.Instance.Map.TilesMap;

        int size = tileMap.GetLength(0);
        bool[,] grid = new bool[size, size];

        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                MapTile currentTile = tileMap[i, j];
                bool typeAccepted = TileTypeFunctions.isAcceptableType(currentTile.Type, acceptableTypes);
                bool toppingAccepted = TileToppingFunctions.isAcceptableTopping(currentTile.Topping, acceptableToppings);

                bool isAccepted = (typeAccepted) ? toppingAccepted : false;
                grid[i, j] = isAccepted;
            }
        }

        return grid;
    } 

}
