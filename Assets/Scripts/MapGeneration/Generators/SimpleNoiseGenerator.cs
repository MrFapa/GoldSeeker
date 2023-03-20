using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseGenerator
{

    private static float[] noiseAmplitudes = { 1, 2, 3, 4 };
    private static float[] noiseFrequency = { 1, 0.8f, 0.6f, 0.4f };

    public static bool[,] GenerateMap()
    {
        int mapSize = MapSettingsManager.Instance.mapSize;
        int noiseOctaves = MapSettingsManager.Instance.noiseOctaves;
        float noiseScale = MapSettingsManager.Instance.noiseScale;
        float waterTh = MapSettingsManager.Instance.waterTh;

        bool[,] map = new bool[mapSize, mapSize];

        for(int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                float noise = 0;
                for(int octaveIndex = 0; octaveIndex < noiseOctaves; octaveIndex++)
                {
                    noise += Mathf.PerlinNoise(i * noiseScale * noiseFrequency[octaveIndex], j * noiseScale * noiseFrequency[octaveIndex]) * noiseAmplitudes[octaveIndex];
                }
                map[i, j] =  noise > waterTh;
            }
        }

        return map;
    }
}
