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


    public static bool[,] Combine(bool[,] arrayA, bool[,] arrayB)
    {
        bool[,] newArray = new bool[arrayA.GetLength(0), arrayA.GetLength(0)];

        for (int i = 0; i < newArray.GetLength(0); i++)
        {
            for (int j = 0; j < newArray.GetLength(1); j++)
            {
                newArray[i, j] = (arrayA[i, j] || arrayB[i, j]);
            }
        }
        return newArray;
    }

    public static bool[,] Temp(bool[,] arrayA, bool[,] arrayB)
    {
        bool[,] newArray = new bool[arrayA.GetLength(0), arrayA.GetLength(0)];

        for (int i = 0; i < newArray.GetLength(0); i++)
        {
            for (int j = 0; j < newArray.GetLength(1); j++)
            {
               
                newArray[i, j] = (arrayA[i, j]) ? arrayA[i, j] : arrayB[i, j];
            }
        }
        return newArray;
    }
}
