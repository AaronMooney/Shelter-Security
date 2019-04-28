using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
/*
 * TextureGenerator Script that generates textures for from a heightmap or color map
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
public static class TextureGenerator {

	public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();

        return texture;
    }

    // ** AARON MOONEY ** //
    // I duplicated the textureFromHeightMap method to take a boolean as well as the heightmap
    public static Texture2D TextureFromHeightMap(float[,] heightmap, bool isIsland)
    {
        int width = heightmap.GetLength(0);
        int height = heightmap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // This chooses the colormap colors depending on whether the terrain is an island or not
                if (isIsland)
                {
                    colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightmap[x, y]);
                } else
                {
                    colorMap[y * width + x] = Color.Lerp(Color.white, Color.black, heightmap[x, y]);
                }
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }
    // ** AARON MOONEY END ** //

    public static Texture2D TextureFromHeightMap(float[,] heightmap)
    {
        int width = heightmap.GetLength(0);
        int height = heightmap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightmap[x, y]);
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }
}
// ** SEBASTIAN LAGUE END ** //
