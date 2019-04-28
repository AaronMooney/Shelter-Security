using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * TerrainData Scriptable object that stores all the data regarding the terrain itself
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
public class TerrainData : UpdateableData
{

    public bool useFalloff;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public float av;
    public float bv;

    public bool isIsland;

    public float MinHeight { get { return meshHeightMultiplier * meshHeightCurve.Evaluate(0); } }

    public float MaxHeight { get { return meshHeightMultiplier * meshHeightCurve.Evaluate(1); } }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        
        if (av < 0.01f)
        {
            av = 0.01f;
        }
        if (bv < 0.01f)
        {
            bv = 0.01f;
        }
        base.OnValidate();
    }
#endif
}
// ** SEBASTIAN LAGUE END ** //
