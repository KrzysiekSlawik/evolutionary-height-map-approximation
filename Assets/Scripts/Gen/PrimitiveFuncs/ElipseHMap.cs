using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
public class ElipseHMap : HMapGen
{
    //new public float[] genes = new float[6];
    
    public override float GetValue(float x, float y) {
        x = x + OffsetX;
        y = y + OffsetY;
        float a = GetA();
        float b = GetB();
        float c = GetC();

        float X = (x*x) / (a*a);
        float Y = (y*y) / (b*b);
        //X + Y + z^2 / c^2 = 1
        float val = c * Mathf.Sqrt(1 - X - Y);

        return float.IsNaN(val) ? 0 : val;
    }

    private float GetA() {
        return this.genes[2];
    }

    private float GetB() {
        return this.genes[3];
    }

    private float GetC() {
        return this.genes[4];
    }

    public override HMapGen Copy() {
        ElipseHMap newMap = new ElipseHMap();
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
                min = ConstParameters.minChangeA;
                max = ConstParameters.maxChangeA;
                break;
            case 3:
                min = ConstParameters.minChangeB;
                max = ConstParameters.maxChangeB;
                break;
            case 4:
                min = ConstParameters.minChangeC;
                max = ConstParameters.maxChangeC;
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
        randGenes[2] = Random.Range(ConstParameters.minA, ConstParameters.maxA);
        randGenes[3] = Random.Range(ConstParameters.minB, ConstParameters.maxB);
        randGenes[4] = Random.Range(ConstParameters.minC, ConstParameters.maxC);
        this.SetGenes(randGenes);
    }

    public override string ToString() {
        string x = "(x + " + OffsetX.ToString() + ")^2/(" + genes[2].ToString() + ")";
        string y = "(y + " + OffsetY.ToString() + ")^2/(" + genes[3].ToString() + ")";
        string z = "(z)^2/(" + genes[4].ToString() + ")";
        //(x + 3)^2/(1)^2 + (y + 2)^2/(3)^2 + (z)^2/(4)^2 = 1
        //return x + " + " + y + " + " + z + " = 1"; 
        return "elipse";
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
