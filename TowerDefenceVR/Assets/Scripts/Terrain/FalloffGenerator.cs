using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * FalloffGenerator Script that generates a noisemap that creates a square gradient that can be used to create an island or a flat area in the center
 * 
 * This script is from the tutorial "Procedural Terrain Generation" by Sebastian Lague
 * link: https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3
 * 
 *  * parts taken from the tutorial are marked with 
 * // ** SEBASTIAN LAGUE ** //
 *       his code here...
 *       any modifications within his code are marked with
 *       // ** AARON MOONEY ** //
 *       my code here
 *       // ** AARON MOONEY END ** //
 * // ** SEBASTIAN LAGUE END ** //
 * */

// ** SEBASTIAN LAGUE ** //
public static class FalloffGenerator {

	public static float[,] GenerateFalloffMap(int size, float av, float bv)
    {
        float[,] map = new float[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));

                map[i, j] = Evaluate(value, av,bv);
            }
        }
        return map;
    }

    // This method is used to determine how far from the edge the color starts to change
    static float Evaluate(float value, float av, float bv)
    {
        float a = av;
        float b = bv;

        // This formula is for a curve between 0 and 1 that Sebastian creates in the series
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}
// ** SEBASTIAN LAGUE END ** //
