using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MinHMap : BinOpHMap
{
    //new public float[] genes = new float[2];
    
    public override float GetValue(float x, float y)
    {
        return Mathf.Min(a.GetValue(x + OffsetX, y + OffsetY), b.GetValue(x + OffsetX, y + OffsetY));
    }
    public override HMapGen Copy()
    {
        MinHMap newHMap = new MinHMap();
        newHMap.a = a;
        newHMap.b = b;
        newHMap.SetGenes(this.genes);
        return newHMap;
    }

    public override string ToString() {
        return "Min({ " + a.ToString() + " }, { " + b.ToString() + " })";
    }
}
