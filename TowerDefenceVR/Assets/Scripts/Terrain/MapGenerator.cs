using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

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
        //terrainMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/MeshMaterial.mat", typeof(Material));
        //Shader shader = Shader.Find("Custom/TerrainShader");
        //Debug.Log(shader.name);
        //terrainMaterial.shader = shader;
        GenerateMap();
    }
#endif

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
        } else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapWidth, terrainData.av, terrainData.bv), terrainData.isIsland));
        }

    }

    MapData GenerateMapData()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (terrainData.useFalloff)
                {
                    if (terrainData.isIsland)
                    {
                        noiseMap[x, y] = noiseMap[x, y] * (1 - falloffMap[x, y]);
                    }
                    else
                    {
                        noiseMap[x, y] = falloffMap[x, y] * (1 - noiseMap[x, y]);
                    }
                }
            }
        }

        textureData.ApplyToMaterial(terrainMaterial);
        textureData.UpdateMeshHeights(terrainMaterial, terrainData.MinHeight, terrainData.MaxHeight);
        return new MapData(noiseMap);
    }

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
