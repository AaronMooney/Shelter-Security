using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * NoiseData Scriptable object that stores all the data regarding noise for the terrain
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
[CreateAssetMenu()]
public class NoiseData : UpdateableData
{

    public float noiseScale;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        base.OnValidate();
    }
#endif
}
// ** SEBASTIAN LAGUE END ** //
