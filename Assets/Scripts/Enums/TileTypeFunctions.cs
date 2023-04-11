using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileTypeFunctions 
{
   public static bool isAcceptableType(TileType type, TileType[] acceptedTypes)
   {
        if (acceptedTypes == null) return false;

        for(int i = 0; i < acceptedTypes.Length; i++)
        {
            if(type == acceptedTypes[i])
            {
                return true;
            }
        }

        return false;
   }
}
