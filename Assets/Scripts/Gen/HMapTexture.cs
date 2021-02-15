using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class HMapTexture : HMapGen
{
    private float[,] heightMap;
    private int size;
    public HMapTexture(Texture2D hmap)
    {
        size = Mathf.Min(hmap.width, hmap.height);
        heightMap = new float[size, size];
        float[] wildMap = hmap.GetPixels(0, 0, size, size).Select(col => col.grayscale).ToArray();
        for (int xi = 0; xi < size; xi++)
        {
            for (int yi = 0; yi < size; yi++)
            {
                heightMap[xi, yi] = wildMap[xi + (int)(yi * size)];
            }
        }
    }
    public HMapTexture()
    {

    }
    public HMapTexture(HMapGen hmap)
    {
        size = 500;
        heightMap = new float[size, size];
        for (int xi = 0; xi < size; xi++)
        {
            for (int yi = 0; yi < size; yi++)
            {
                heightMap[xi, yi] = hmap.GetValue((float)xi/(float)size, (float)yi / (float)size);
            }
        }
    }
    public override float GetValue(float x, float y)
    {
        int xi = (int)(size * (x + OffsetX));
        int yi = (int)(size * (y + OffsetX));
        if (xi < size && xi >= 0 && yi < size && yi >= 0) return heightMap[xi, yi];
        return 0;
    }
    public override HMapGen Copy()
    {
        HMapTexture newHMap = new HMapTexture();
        newHMap.size = size;
        newHMap.heightMap = heightMap.Clone() as float[,];
        newHMap.genes = genes.Clone() as float[];
        return newHMap;
    }
}
