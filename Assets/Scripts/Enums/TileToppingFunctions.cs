using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileToppingFunctions 
{
    public static bool isAcceptableTopping(TileTopping topping, TileTopping[] acceptedToppings)
    {
        if (acceptedToppings == null) return false;

        for (int i = 0; i < acceptedToppings.Length; i++)
        {
            if (topping == acceptedToppings[i])
            {
                return true;
            }
        }

        return false;
    }
}
