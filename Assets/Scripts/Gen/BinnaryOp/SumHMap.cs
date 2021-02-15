using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SumHMap : BinOpHMap
{ 
    public override float GetValue(float x, float y)
    {
        return a.GetValue(x+OffsetX,y+OffsetY) + b.GetValue(x + OffsetX, y + OffsetY);
    }
    public override HMapGen Copy()
    {
        SumHMap newHMap = new SumHMap();
        newHMap.a = a;
        newHMap.b = b;
        newHMap.SetGenes(this.genes);
        return newHMap;
    }

    public override string ToString() {
        return "{ " + a.ToString() + " } + { " + b.ToString() + " }";
    }
}
