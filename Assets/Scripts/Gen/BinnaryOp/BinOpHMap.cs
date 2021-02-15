using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using Random = UnityEngine.Random;

[System.Serializable]
[XmlInclude(typeof(MinHMap))]
[XmlInclude(typeof(MinHMap))]
[XmlInclude(typeof(SumHMap))]
[XmlInclude(typeof(ProductHMap))]
public class BinOpHMap : HMapGen
{
    public HMapGen a;
    public HMapGen b;
    public override HMapGen Mutate()
    {
        BinOpHMap mutatedNode;
        float rand = Random.Range(0f,1f);
        if(rand < 0.25f)
        {
            mutatedNode = Copy() as BinOpHMap;
            mutatedNode.a = a.Mutate();
            return mutatedNode;
        }
        if(rand < 0.5f)
        {
            mutatedNode = Copy() as BinOpHMap;
            mutatedNode.b = b.Mutate();
            return mutatedNode;
        }
        if(rand < 0.75f)
        {
            rand = Random.Range(0f, 1f);
            if (rand < 0.25f)
            {
                mutatedNode = new SumHMap();
            }
            else if (rand < 0.5f)
            {
                mutatedNode = new ProductHMap();
            }
            else if (rand < 0.75f)
            {
                mutatedNode = new MinHMap();
            }
            else
            {
                mutatedNode = new MaxHMap();
            }
            mutatedNode.a = this.a;
            mutatedNode.b = this.b;
            return mutatedNode;
        }
        return base.Mutate();
    }

    public override string ToString() {
        return "bin op";
    }

    public override string PrintGenes() {
        return "{" + a.PrintGenes() + ", " + b.PrintGenes() + "}";
    }
}
