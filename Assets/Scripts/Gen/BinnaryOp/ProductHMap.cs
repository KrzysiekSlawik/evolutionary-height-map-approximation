using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProductHMap : BinOpHMap
{
    //new public float[] genes = new float[2];
    
    public override float GetValue(float x, float y)
    {
        return a.GetValue(x + OffsetX, y + OffsetY) * b.GetValue(x + OffsetX, y + OffsetY);
    }
    public override HMapGen Copy()
    {
        ProductHMap newHMap = new ProductHMap();
        newHMap.a = a;
        newHMap.b = b;
        newHMap.SetGenes(this.genes);
        return newHMap;
    }

    public override string ToString() {
        return "{ " + a.ToString() + " } * { " + b.ToString()+ " }";
    }
}
