using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
public class ConstHMap : HMapGen
{    
    //new public float[] genes = new float[3];

    public override float GetValue(float x, float y) {
        //Debug.Log(this.GetConst());
        return this.GetConst();
    }

    private float GetConst(){
        return genes[2];
    }
    
    public override HMapGen Copy() {
        ConstHMap newMap = new ConstHMap();
        newMap.SetGenes(this.genes);
        return newMap;
    }

    public override HMapGen Mutate() {
        float change;
        HMapGen mutatedLeaf = this.Copy();
        float[] mutatedGenes = this.GetGenes().Clone() as float[];
        float min = 0f, max = 0f;
        min = ConstParameters.minChangeConst;
        max = ConstParameters.maxChangeConst;
        change = Random.Range(min, max);
        mutatedGenes[2] += change;
        mutatedLeaf.SetGenes(mutatedGenes);
        return mutatedLeaf;
    }

    public override void SetRandomGenes() {
        float[] randGenes = new float[3];
        randGenes[0] = Random.Range(ConstParameters.minOffset, ConstParameters.maxOffset);
        randGenes[1] = Random.Range(ConstParameters.minOffset, ConstParameters.maxOffset);
        randGenes[2] = Random.Range(ConstParameters.minConst, ConstParameters.maxConst);
        this.SetGenes(randGenes);
    }

    public override string ToString() {
        //3,456
        //return genes[2].ToString();
        return "const";
    }

    public override string PrintGenes() {
        return "[" + genes[2].ToString() + "]";
    }
}
