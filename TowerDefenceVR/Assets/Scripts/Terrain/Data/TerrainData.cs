using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
