﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode { NoiseMap, ColorMap, Mesh, FalloffMap };
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public bool autoUpdate;
    public bool useFalloff;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public float av;
    public float bv;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;
    public bool isIsland;

    public TerrainType[] regions;
    float[,] falloffMap;

    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, av, bv);
        GenerateMap();
    }

    public void GenerateMap()
    {

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (useFalloff)
                {
                    if (isIsland)
                    {
                        noiseMap[x, y] = noiseMap[x, y] * (1 - falloffMap[x, y]);
                    } else
                    {
                        noiseMap[x, y] = falloffMap[x, y] * (1 - noiseMap[x, y]);
                    }
                }
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        } else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        } else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        } else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapWidth, av, bv), isIsland));
        }

    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        if (av < 0.01f)
        {
            av = 0.01f;
        }
        if (bv < 0.01f)
        {
            bv = 0.01f;
        }
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, av, bv);
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }

}
