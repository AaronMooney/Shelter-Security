using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
/*
 * MapGenerator Script that is attached to an object in the scene to create the terrain
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
public class MapGenerator : MonoBehaviour {

    public enum DrawMode { NoiseMap, Mesh, FalloffMap };
    public DrawMode drawMode;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public TextureData textureData;

    public int mapWidth;
    public int mapHeight;
    public Material terrainMaterial;

    public bool autoUpdate;

    float[,] falloffMap;

#if UNITY_EDITOR
    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, terrainData.av, terrainData.bv);

        // adding material with custom shader to mesh for texturing but this only worked in the editor so it is unused.

        //terrainMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/MeshMaterial.mat", typeof(Material));
        //Shader shader = Shader.Find("Custom/TerrainShader");
        //Debug.Log(shader.name);
        //terrainMaterial.shader = shader;
        GenerateMap();
    }
#endif

    // Generate the terrain and draw in the scene according to the selected enum
    public void GenerateMap()
    {

        MapData mapData = GenerateMapData();

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        } else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            // ** AARON MOONEY ** //
            // This line was changed to use the method I modified in the TextureGenerator script so that the falloff map can be used to create a flat area in the center of the map as opposed to an island
            // where the flat areas are on the outside
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapWidth, terrainData.av, terrainData.bv), terrainData.isIsland));
            // ** AARON MOONEY END ** //
        }

    }

    // Generate the noise map and determine if the falloff is used or not
    MapData GenerateMapData()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (terrainData.useFalloff)
                {
                    // ** AARON MOONEY ** //
                    // Reverse the order of noiseMap and falloffMap depending on if the terrain is to be an island or not
                    if (terrainData.isIsland)
                    {
                        noiseMap[x, y] = noiseMap[x, y] * (1 - falloffMap[x, y]);
                    }
                    else
                    {
                        noiseMap[x, y] = falloffMap[x, y] * (1 - noiseMap[x, y]);
                    }
                    // ** AARON MOONEY END ** //
                }
            }
        }

        textureData.ApplyToMaterial(terrainMaterial);
        textureData.UpdateMeshHeights(terrainMaterial, terrainData.MinHeight, terrainData.MaxHeight);
        return new MapData(noiseMap);
    }

    // Called on load and when a value is changed in the inspector
    void OnValidate()
    {
        
        if (terrainData != null)
        {
            terrainData.OnValuesUpdated -= OnValuesUpdated;
            terrainData.OnValuesUpdated += OnValuesUpdated;
            falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, terrainData.av, terrainData.bv);
        }
        if (noiseData != null)
        {
            noiseData.OnValuesUpdated -= OnValuesUpdated;
            noiseData.OnValuesUpdated += OnValuesUpdated;
        }
        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnValuesUpdated;
            textureData.OnValuesUpdated += OnValuesUpdated;
        }
    }

    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);
    }

    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            GenerateMap();
        }
    }


    public struct MapData
    {
        public float[,] heightMap;


        public MapData(float[,] heightMap)
        {
            this.heightMap = heightMap;
        }
    }

}
// ** SEBASTIAN LAGUE END ** //
