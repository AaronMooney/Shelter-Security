using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
 * UpdateDataEditor script that creates an update button to the inspector in order to update the terrain
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
[CustomEditor (typeof(UpdateableData), true)]
public class UpdateDataEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UpdateableData data = (UpdateableData)target;

        if (GUILayout.Button("Update"))
        {
            data.NotifyUpdate();
        }
    }
}
// ** SEBASTIAN LAGUE END ** //
