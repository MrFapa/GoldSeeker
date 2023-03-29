using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayHandler 
{
   public static bool[,] Invert(bool[,] array)
    {
        for(int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = !array[i, j];
            }
        }
        return array;
    }
}
