using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseGenerator
{

    private static float[] noiseAmplitudes = { 1, 2, 3, 4 };
    private static float[] noiseFrequency = { 1, 0.8f, 0.6f, 0.4f };

    public static bool[,] GenerateMap(SimpleNoiseSettings noiseSettings)
    {
        int mapSize = MapSettingsManager.Instance.mapSize;

        bool[,] map = new bool[mapSize, mapSize];

        for(int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                float noise = 0;
                for(int octaveIndex = 0; octaveIndex < noiseSettings.octaves; octaveIndex++)
                {
                    noise += Mathf.PerlinNoise( i * noiseSettings.scale * noiseFrequency[octaveIndex], 
                                                j * noiseSettings.scale * noiseFrequency[octaveIndex]) * noiseAmplitudes[octaveIndex];
                }
                map[i, j] =  noise > noiseSettings.threshold;
            }
        }

        return map;
    }
}
