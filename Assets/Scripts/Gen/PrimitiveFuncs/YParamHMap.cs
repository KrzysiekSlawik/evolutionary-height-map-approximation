using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
public class YParamHMap : HMapGen
{    
    //new public float[] genes = new float[3];

    public override float GetValue(float x, float y) {
        float param = GetParam();
        return param * (y + OffsetX);
    }

    private float GetParam() {
        return genes[2];
    }

    public override HMapGen Copy() {
        YParamHMap newMap = new YParamHMap();
        newMap.SetGenes(this.genes);
        return newMap;
    }

    public override HMapGen Mutate() {
        int randElem = Random.Range(0, genes.Length);
        float change;
        HMapGen mutatedLeaf = this.Copy();
        float[] mutatedGenes = this.GetGenes().Clone() as float[];
        float min = 0f, max = 0f;

        switch(randElem) {
            case 0:
                min = ConstParameters.minChangeOffset;
                max = ConstParameters.maxChangeOffset;
                break;
            case 1:
                min = ConstParameters.minChangeOffset;
                max = ConstParameters.maxChangeOffset;
                break;
            case 2:
                min = ConstParameters.minChangeParam;
                max = ConstParameters.maxChangeParam;
                break;
        }

        change = Random.Range(min, max);
        mutatedGenes[randElem] += change;
        mutatedLeaf.SetGenes(mutatedGenes);
        return mutatedLeaf;
    }

    public override void SetRandomGenes() {
        float[] randGenes = new float[3];
        randGenes[0] = Random.Range(ConstParameters.minOffset, ConstParameters.maxOffset);
        randGenes[1] = Random.Range(ConstParameters.minOffset, ConstParameters.maxOffset);
        randGenes[2] = Random.Range(ConstParameters.minParam, ConstParameters.maxParam);
        this.SetGenes(randGenes);
    }

    public override string ToString() {
        //f(y) = 3,45 * (y + 34);
        //return "f(y) = " + genes[2].ToString() + " * (y + " + OffsetY.ToString() + ")";
        return "y param";
    }

    public override string PrintGenes() {
        return "[py" + genes[2].ToString() + "]";
    }
}
