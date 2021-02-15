using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstParameters : MonoBehaviour
{
    //evolution params
    public static float mutationChance = 1f;
    public static int   populationSize = 30;
    public static int   maxInitDepth = 6;
    public static float evoLoopDuration = 5f;
    public static float mcRewardDuration = 1f;
    public static float errorThreshold = 0.05f;
    public static int   randoms = 10;
    public static int   crossings = 7;
    public static int   stepSqrt = 20;
    public static float depthPenalty = 0.003f;
    public static int   mutations = 20;
    //common params
    public static float minChangeOffset = -1f;
    public static float maxChangeOffset = 1f;
    //const params
    public static float minChangeConst = -0.5f;
    public static float maxChangeConst = 0.5f;
    // X/Y params
    public static float minChangeParam = -1f;
    public static float maxChangeParam = 1f;
    //elipse params
    public static float minChangeA = -1f;
    public static float maxChangeA = 1f;
    public static float minChangeB = -1f;
    public static float maxChangeB = 1f;
    public static float minChangeC = -1f;
    public static float maxChangeC = 1f;
    //perlin params
    public static float minChangeScaleX = -1f;
    public static float maxChangeScaleX = 1f;
    public static float minChangeScaleY = -1f;
    public static float maxChangeScaleY = 1f;
    public static float minChangeScaleVal = -1f;
    public static float maxChangeScaleVal = 1f;

    //init params
    public static float minOffset = -1f;
    public static float maxOffset = 1f;
    public static float minConst = -0.5f;
    public static float maxConst = 0.5f;
    public static float minA = 0f;
    public static float maxA = 0.9f;
    public static float minB = 0f;
    public static float maxB = 0.9f;
    public static float minC = 0.05f;
    public static float maxC = 1.5f;
    public static float minScaleX = 3f;
    public static float maxScaleX = 20f;
    public static float minScaleY = 3f;
    public static float maxScaleY = 20f;
    public static float minScaleVal = 0.5f;
    public static float maxScaleVal = 1f;
    public static float minParam = -1f;
    public static float maxParam = 1f;

    //falgs
    public static bool useTime = true;
    public static bool useError = false;
    public static bool useABS = false;
    public static bool useMSE = true;
    public static bool useMC = false;
    public static bool useDSSIM = false;

    public static void ChangeParam(int index, float value) {
        switch(index) {
            case 0:
                mutationChance = value;
                break;
            case 1:
                minChangeOffset = value;
                break;
            case 2:
                maxChangeOffset = value;
                break;
            case 3:
                minChangeConst = value;
                break;
            case 4:
                maxChangeConst = value;
                break;
            case 5:
                minChangeParam = value;
                break;
            case 6:
                maxChangeParam = value;
                break;
            case 7:
                populationSize = (int) value;
                break;
            case 8:
                maxInitDepth = (int) value;
                break;
            case 9:
                minChangeA = value;
                break;
            case 10:
                maxChangeA = value;
                break;
            case 11:
                minChangeB = value;
                break;
            case 12:
                maxChangeB = value;
                break;
            case 13:
                minChangeC = value;
                break;
            case 14:
                maxChangeC = value;
                break;
            case 15:
                minChangeScaleX = value;
                break;
            case 16:
                maxChangeScaleX = value;
                break;
            case 17:
                minChangeScaleY = value;
                break;
            case 18:
                maxChangeScaleY = value;
                break;
            case 19:
                minChangeScaleVal = value;
                break;
            case 20:
                maxChangeScaleVal = value;
                break;
            case 21:
                minOffset = value;
                break;
            case 22:
                maxOffset = value;
                break;
            case 23:
                minConst = value;
                break;
            case 24:
                maxConst = value;
                break;
            case 25:
                minA = value;
                break;
            case 26:
                maxA = value;
                break;
            case 27:
                minB = value;
                break;
            case 28:
                maxB = value;
                break;
            case 29:
                minC = value;
                break;
            case 30:
                maxC = value;
                break;
            case 31:
                minScaleX = value;
                break;
            case 32:
                maxScaleX = value;
                break;
            case 33:
                minScaleY = value;
                break;
            case 34:
                maxScaleY = value;
                break;
            case 35:
                minScaleVal = value;
                break;
            case 36:
                maxScaleVal = value;
                break;
            case 37:
                minParam = value;
                break;
            case 38:
                maxParam = value;
                break;
            case 39:
                evoLoopDuration = value;
                break;
            case 40:
                mcRewardDuration = value;
                break;
            case 41:
                errorThreshold = value;
                break;
            case 42: // tu był zupełnie inny prametr
                randoms = (int) value;
                break;
            case 43: //zmieniona nazwa
                crossings = (int) value;
                break;
            case 44:
                stepSqrt = (int) value;
                break;
            case 45: // nowe
                depthPenalty = value;
                break;
            case 46:
                mutations = (int) value;
                break;
        }
    }

    public static void SetFlag(int index, bool val) {
        switch(index) {
            case 0:
                useTime = val;
                break;
            case 1:
                useError = val;
                break;
            case 2:
                useMSE = val;
                break;
            case 3:
                useMC = val;
                break;
            case 4:
                useDSSIM = val;
                break;
            case 5:
                useABS = val;
                break;
        }
        //PrintFlags();
    }

    public static bool GetFlag(int index) {
        bool[] flags = {useTime, useError, useMSE, useMC, useDSSIM, useABS};
        return flags[index];
    }

    public static void PrintFlags() {
        bool[] flags = {useTime, useError, useMSE, useMC, useDSSIM, useABS};
        string[] names = {"time: ", ",error: ", ",mse: ", ",mc: ", ",dssim: ", ",abs: "};
        string ans = "";
        for(int i = 0; i < flags.Length; i++) {
            ans += names[i] + flags[i].ToString();
        }
        Debug.Log(ans);
    }
    
    public static bool BadChange() {
        if(!useTime && !useError)
            return true;
        if(!useMSE && !useMC && !useDSSIM && !useABS)
            return true;
        return false;
    }
    
    public static float GetParam(int index) {
        float[] parameters = {mutationChance, minChangeOffset, maxChangeOffset, minChangeConst, maxChangeConst, minChangeParam, maxChangeParam, populationSize, maxInitDepth,
                              minChangeA, maxChangeA, minChangeB, maxChangeB, minChangeC, maxChangeC, minChangeScaleX, maxChangeScaleX, minChangeScaleY, maxChangeScaleY, minChangeScaleVal,
                              maxChangeScaleVal, minOffset, maxOffset, minConst, maxConst, minA, maxA, minB, maxB, minC, maxC, minScaleX, maxScaleX, minScaleY, maxScaleY, minScaleVal, maxScaleVal,
                              minParam, maxParam, evoLoopDuration, mcRewardDuration, errorThreshold, randoms, crossings, stepSqrt, depthPenalty, mutations};
        return parameters[index];
    }
}


    /*//not actual parameters
    private static void PrintDebug() {
        float[] parameters = {mutationChance, minChangeOffset, maxChangeOffset, minChangeConst, maxChangeConst, minChangeParam, maxChangeParam, 
                                  minChangeA, maxChangeA, minChangeB, maxChangeB, minChangeC, maxChangeC, minChangeScaleX, maxChangeScaleX,
                                  minChangeScaleY, maxChangeScaleY, minChangeScaleVal, maxChangeScaleVal, populationSize, };

        string[] names = {"mutationChance", "minChangeOffset", "maxChangeOffset", "minChangeConst", "maxChangeConst", "minChangeParam", "maxChangeParam", 
                                  "minChangeA", "maxChangeA", "minChangeB", "maxChangeB", "minChangeC", "maxChangeC", "minChangeScaleX", "maxChangeScaleX",
                                  "minChangeScaleY", "maxChangeScaleY", "minChangeScaleVal", "maxChangeScaleVal", "populationSize"};
        for(int i = 0; i < parameters.Length; i++) {
            Debug.Log(names[i] + ": " + parameters[i].ToString());
        } 
    }
    */


//dodać zapisywanie parametrów i podpiąc je pod ui

/*
switch(index) {
            case 0:
                return mutationChance;
            case 1:
                return minChangeOffset;
            case 2:
                return maxChangeOffset;
            case 3:
                return minChangeConst;
            case 4:
                return maxChangeConst;
            case 5:
                return minChangeParam;
            case 6:
                return maxChangeParam;
            case 7:
                return (float) populationSize;
            case 8:
                return (float) maxInitDepth;
            case 9:
                return minChangeA;
            case 10:
                return maxChangeA;
            case 11:
                return minChangeB;
            case 12:
                return maxChangeB;
            case 13:
                return minChangeC;
            case 14:
                return maxChangeC;
            case 15:
                return minChangeScaleX;
            case 16:
                return maxChangeScaleX;
            case 17:
                return minChangeScaleY;
            case 18:
                return maxChangeScaleY;
            case 19:
                return minChangeScaleVal;
            case 20:
                return maxChangeScaleVal;
            case 21:
                return minOffset;
            case 22:
                return maxOffset;
            case 23:
                return minConst;
            case 24:
                return maxConst;
            case 25:
                return minA;
            case 26:
                return maxA;
            case 27:
                return minB;
            case 28:
                return maxB;
            case 29:
                return minC;
            case 30:
                return maxC;
            case 31:
                return minScaleX;
            case 32:
                return maxScaleX;
            case 33:
                return minScaleY;
            case 34:
                return maxScaleY;
            case 35:
                return minScaleVal;
            case 36:
                return maxScaleVal;
            case 37:
                return minParam;
            case 38:
                return maxParam;
            case 39:
                return evoLoopDuration;
        }

*/