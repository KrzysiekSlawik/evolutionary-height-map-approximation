using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
[XmlInclude(typeof(ConstHMap))]
[XmlInclude(typeof(YParamHMap))]
[XmlInclude(typeof(XParamHMap))]
[XmlInclude(typeof(PerlinHMap))]
[XmlInclude(typeof(ElipseHMap))]
[XmlInclude(typeof(SumHMap))]
[XmlInclude(typeof(MaxHMap))]
[XmlInclude(typeof(MinHMap))]
[XmlInclude(typeof(ProductHMap))]
[XmlInclude(typeof(BinOpHMap))]         //możliwe że ten do wywalenia
public class HMapGen
{
    public float[] genes = {0f, 0f};
    
    public virtual float GetValue(float x, float y){
        return 1f;
    }
    
    public float[] GetGenes() {
        return genes;
    }
    
    public void SetGenes(float[] genes_) {
        if(genes.Length <  genes_.Length) {
            genes = new float[genes_.Length];
        }
        Array.Copy(genes_, genes, genes_.Length);
    }
    
    public void XTranslate(float value) {
        OffsetX += value; 
    }
    
    public void YTranslate(float value) {
        OffsetY += value;
    }

    public virtual HMapGen Copy() {
        return null;
    }

    public virtual HMapGen Mutate() {
        int randElem = Random.Range(0, genes.Length);
        float change = Random.Range(ConstParameters.minChangeOffset, ConstParameters.maxChangeOffset);
        HMapGen mutatedNode = this.Copy();
        float[] mutatedGenes = this.GetGenes().Clone() as float[];;
        mutatedGenes[randElem] += change;   
        mutatedNode.SetGenes(mutatedGenes);
        //Debug.Log("org: " + genes[randElem].ToString());
        //Debug.Log("mut: " + mutatedGenes[randElem].ToString());
        //Debug.Log("change: " + change.ToString());
        return mutatedNode;
    }

    public virtual void SetRandomGenes() {
        float[] randGenes = new float[2];
        randGenes[0] = Random.Range(ConstParameters.minOffset, ConstParameters.maxOffset);
        randGenes[1] = Random.Range(ConstParameters.minOffset, ConstParameters.maxOffset);
        this.SetGenes(randGenes);
    }

    protected float OffsetX {get => genes[0]; set => genes[0] = value;}
    
    protected float OffsetY {get => genes[1]; set => genes[1] = value;}

    public override string ToString() {
        return "hmapgen";
    }

    public virtual string PrintGenes() {
        return "[" + OffsetX.ToString() + ", " + OffsetY.ToString() + "]";
    }
}