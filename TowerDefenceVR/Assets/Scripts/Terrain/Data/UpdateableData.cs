using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * UpdatableData Scriptable object parent class
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
public class UpdateableData : ScriptableObject {

    public event System.Action OnValuesUpdated;
    public bool autoUpdate;

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (autoUpdate)
        {
            UnityEditor.EditorApplication.update += NotifyUpdate;
        }
    }
#endif

#if UNITY_EDITOR
    public void NotifyUpdate()
    {
        UnityEditor.EditorApplication.update -= NotifyUpdate;
        if (OnValuesUpdated != null)
        {
            OnValuesUpdated();
        }
    }
#endif	
}
// ** SEBASTIAN LAGUE END ** // 
