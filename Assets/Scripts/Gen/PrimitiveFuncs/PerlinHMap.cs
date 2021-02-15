using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
public class PerlinHMap : HMapGen
{
    //new public float[] genes = new float[5];

    public override float GetValue(float x, float y) {
        float scaleX = GetScaleX();
        float scaleY = GetScaleY();
        float scaleV = GetScaleVal();
        return Mathf.PerlinNoise((x + OffsetX) * scaleX, (y + OffsetY) * scaleY) * scaleV;
    }

    private float GetScaleX() {
        return genes[2];
    }

    private float GetScaleY() {
        return genes[3];
    }

    private float GetScaleVal() {
        return genes[4];
    }

    public override HMapGen Copy() {
        PerlinHMap newMap = new PerlinHMap();
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
                min = ConstParameters.minChangeScaleX;
                max = ConstParameters.maxChangeScaleX;
                break;                    
            case 3:
                min = ConstParameters.minChangeScaleY;
                max = ConstParameters.maxChangeScaleY;
                break;
            case 4:
                min = ConstParameters.minChangeScaleVal;
                max = ConstParameters.maxChangeScaleVal;
                break;
        }

        change = Random.Range(min, max);
        mutatedGenes[randElem] += change;
        mutatedLeaf.SetGenes(mutatedGenes);
        return mutatedLeaf;
    }   

    public override void SetRandomGenes() {
        float[] randGenes = new float[5];
        randGenes[0] = Random.Range(ConstParameters.minOffset, ConstParameters.maxOffset);
        randGenes[1] = Random.Range(ConstParameters.minOffset, ConstParameters.maxOffset);
        randGenes[2] = Random.Range(ConstParameters.minScaleX, ConstParameters.maxScaleX);
        randGenes[3] = Random.Range(ConstParameters.minScaleY, ConstParameters.maxScaleY);
        randGenes[4] = Random.Range(ConstParameters.minScaleVal, ConstParameters.maxScaleVal);
        this.SetGenes(randGenes);
    }

    public override string ToString() {
        //Perlin((x + 2,34) * 2,5, (y + 1.5) * 2,3) * 5
        string x = "(x + " + OffsetX.ToString() + ") * " + genes[2].ToString();
        string y = "(y + " + OffsetY.ToString() + ") * " + genes[3].ToString();
        string scale = genes[4].ToString();
        //return "Perlin(" + x + ", " + y + ") * " + scale;
        return "perlin";
    }

    public override string PrintGenes() {
        string ans = "[";
        for(int i = 2; i < 5; i++) {
            if(i != 4)
                ans += genes[i].ToString() + ", ";
            else
                ans += genes[i].ToString() + "]";
        }
        return ans;
    }
}
