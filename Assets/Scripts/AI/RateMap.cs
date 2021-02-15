using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateMap : MonoBehaviour
{
    public static float Reward(HMapGen input, HMapGen wanted) {;
        float actStep = 1f / ConstParameters.stepSqrt;
        float ans = 0f;
        for(int i = 0; i < ConstParameters.stepSqrt; i++) {
            for(int j = 0; j < ConstParameters.stepSqrt; j++) {
                float inputVal = input.GetValue(i * actStep, j * actStep);
                float wantedVal = wanted.GetValue(i * actStep, j * actStep);
                ans += Mathf.Abs(inputVal - wantedVal);
            }
        }
        ans /= (float) ConstParameters.stepSqrt;
        return ans;
    }

    public static float MSE(HMapGen input, HMapGen wanted) {
        float actStep = 1f / ConstParameters.stepSqrt;
        float ans = 0f;
        for(int i = 0; i < ConstParameters.stepSqrt; i++) {
            for(int j = 0; j < ConstParameters.stepSqrt; j++) {
                float inputVal = Mathf.Max(0, input.GetValue(i * actStep, j *actStep));
                float wantedVal = Mathf.Max(0, wanted.GetValue(i * actStep, j * actStep));
                float diff = inputVal - wantedVal;
                ans += diff * diff;
            }
        }
        ans /= ConstParameters.stepSqrt * ConstParameters.stepSqrt;
        return ans;
    }

    public static float MCReward(HMapGen input, HMapGen wanted) {
        float end = Time.realtimeSinceStartup + ConstParameters.mcRewardDuration;
        float ans = 0f;
        int probNum = 0;
        float current = Time.realtimeSinceStartup;
        while(current < end) {
            float randI = Random.Range(0f, 1f);
            float randJ = Random.Range(0f, 1f);
            float inputVal = input.GetValue(randI, randJ);
            float wantedVal = wanted.GetValue(randI, randJ);
            ans += Mathf.Abs(inputVal - wantedVal);
            probNum++;
            current = Time.realtimeSinceStartup;
        }
        ans /= (float) probNum;       
        return ans;
    }

    public static float DSSIM(HMapGen input, HMapGen wanted) {
        float meanInput = 0f, meanWanted = 0f, varianceInput = 0f, varianceWanted = 0f, covariance = 0f;
        float k1 = 0.01f, k2 = 0.03f, max = 150f;
        float c1 = (max * k1) * (max * k1), c2 = (max * k2) * (max * k2);
        float actStep = 1f / ConstParameters.stepSqrt;
        
        meanInput = CountMean(input);
        meanWanted = CountMean(wanted);

        varianceInput = CountVariance(input, meanInput);
        varianceWanted = CountVariance(wanted, meanWanted);

        covariance = CountCoVariance(input, wanted, meanInput, meanWanted);
        
        float nom = (2f * meanInput * meanWanted + c1) * (2f * covariance + c2);
        float den = (meanInput * meanInput + meanWanted * meanWanted + c1) * (varianceInput * varianceInput + varianceWanted * varianceWanted + c2);

        return (float) (1f - (nom / den)) / 2f;
    }

    private static float CountMean(HMapGen map) {
        float actStep = 1f / ConstParameters.stepSqrt;
        float mean = 0f;
        
        for(int i = 0; i < ConstParameters.stepSqrt; i++) {
            for(int j = 0; j < ConstParameters.stepSqrt; j++) {
                float val = Mathf.Max(0,map.GetValue(i * actStep, j * actStep));
                mean += val;
            }
        }
        
        mean /= (float) ConstParameters.stepSqrt;
        return mean;
    }

    private static float CountVariance(HMapGen map, float mean) {
        float len = ConstParameters.stepSqrt - 1f;
        float actStep = 1f / ConstParameters.stepSqrt;
        float variance = 0f;
        
        for(int i = 0; i < ConstParameters.stepSqrt; i++) {
            for(int j = 0; j < ConstParameters.stepSqrt; j++) {
                float val = Mathf.Max(0, map.GetValue(i * actStep, j *actStep));
                variance += (val - mean) * (val - mean);
            }
        }

        variance /= len;
        return variance;
    }

    private static float CountCoVariance(HMapGen map1, HMapGen map2, float mean1, float mean2) {
        float len = ConstParameters.stepSqrt - 1f;
        float actStep = 1f / (float) ConstParameters.stepSqrt;
        float covariance = 0f;
        for(int i = 0; i < ConstParameters.stepSqrt-1; i++) {
            for(int j = i+1; j < ConstParameters.stepSqrt; j++) {
                float val1 = map1.GetValue(i * actStep, j *actStep);
                float val2 = map2.GetValue(i * actStep, j * actStep);  
                covariance += (val1 - mean1) * (val2 - mean2);
            }
        }
        covariance /= len;
        return covariance;
    }

    public static float FitnessFunc(HMapGen input, HMapGen wanted) {
        if(ConstParameters.useABS)
            return Reward(input, wanted) * TreeDepthPenalty(input);
        else if(ConstParameters.useMSE)
            return MSE(input, wanted) * TreeDepthPenalty(input);
        else if(ConstParameters.useMC)
            return MCReward(input, wanted) * TreeDepthPenalty(input);
        else if(ConstParameters.useDSSIM)
            return DSSIM(input, wanted) * TreeDepthPenalty(input);
        else //sanity check
            return MSE(input, wanted) * TreeDepthPenalty(input);
    }

    public static float TreeDepthPenalty(HMapGen tree)
    {
        return 1 + TreeStructure.TreeDepth(tree) * ConstParameters.depthPenalty;
    }


    void Start() {
        BinOpHMap map1 = TreeStructure.MakeRandomNode(2) as BinOpHMap;
        BinOpHMap map3 = TreeStructure.MakeRandomNode(3) as BinOpHMap;
        map1.SetRandomGenes();
        map3.SetRandomGenes();
        ElipseHMap el = new ElipseHMap();
        el.SetRandomGenes();
        /*
        string ans1 = "",  ans2 = "";
        float[] gen1 = map1.GetGenes();
        float[] gen2 = map2.GetGenes();
        for(int i = 0; i < 3; i++) {
            ans1 += gen1[i].ToString() + " ";
            ans2 += gen2[i].ToString() + " ";  
        }
        Debug.Log(ans1);
        Debug.Log(ans2);
        */
        Debug.Log("MSE: " + MSE(map1, map3));
        Debug.Log("Reward: " + Reward(map1, map3));
        Debug.Log("MC Reward: " + MCReward(map1, map3));
        Debug.Log("DSSIM: " + DSSIM(map1, map3));
        Debug.Log("DSSIM 0: " + DSSIM(map1, map1));
        Debug.Log(map3.ToString());
        // Debug.Log(map3.PrintGenes());
        Debug.Log(map1.ToString());
        // Debug.Log(map1.PrintGenes());
        Debug.Log(map3.GetValue(0f, 0f));
        Debug.Log(map1.GetValue(0f, 0f));
        // float[] t = el.GetGenes();
        // t[0] = 0f;
        // t[1] = 0f;
        // el.SetGenes(t);
        // Debug.Log(el.PrintGenes());
        // Debug.Log(el.GetValue(0f, 0f));
    }
}
