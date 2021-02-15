using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;

[System.Serializable]
public class ParamsData
{
    //evo
    public float mutationChance;
    public int   populationSize;
    public int   maxInitDepth;
    public float evoLoopDuration;
    public float mcRewardDuration;
    public float errorThreshold;
    public int   crossings; // zmienione
    public int   stepSqrt;
    public float depthPenalty;
    public int   mutations;
    public int   randoms;

    
    //common params
    public float minChangeOffset;
    public float maxChangeOffset;
    //const params
    public float minChangeConst;
    public float maxChangeConst;
    // X/Y params
    public float minChangeParam;
    public float maxChangeParam;
    //elipse params
    public float minChangeA;
    public float maxChangeA;
    public float minChangeB;
    public float maxChangeB;
    public float minChangeC;
    public float maxChangeC;
    //perlin params
    public float minChangeScaleX;
    public float maxChangeScaleX;
    public float minChangeScaleY;
    public float maxChangeScaleY;
    public float minChangeScaleVal;
    public float maxChangeScaleVal;

    //init params
    public float minOffset;
    public float maxOffset;
    public float minConst;
    public float maxConst;
    public float minA;
    public float maxA;
    public float minB;
    public float maxB;
    public float minC;
    public float maxC;
    public float minScaleX;
    public float maxScaleX;
    public float minScaleY;
    public float maxScaleY;
    public float minScaleVal;
    public float maxScaleVal;
    public float minParam;
    public float maxParam;

    //falgs
    public bool useTime;
    public bool useError;
    public bool useABS;
    public bool useMSE;
    public bool useMC;
    public bool useDSSIM;

    public void UpdateAll() {
        float[] parameters = {mutationChance, minChangeOffset, maxChangeOffset, minChangeConst, maxChangeConst, minChangeParam, maxChangeParam, populationSize, maxInitDepth,
                              minChangeA, maxChangeA, minChangeB, maxChangeB, minChangeC, maxChangeC, minChangeScaleX, maxChangeScaleX, minChangeScaleY, maxChangeScaleY, minChangeScaleVal,
                              maxChangeScaleVal, minOffset, maxOffset, minConst, maxConst, minA, maxA, minB, maxB, minC, maxC, minScaleX, maxScaleX, minScaleY, maxScaleY, minScaleVal, maxScaleVal,
                              minParam, maxParam, evoLoopDuration, mcRewardDuration, errorThreshold, randoms, crossings, stepSqrt, depthPenalty, mutations};
        
        bool[] flags = {useTime, useError, useMSE, useMC, useDSSIM, useABS};
        
        for(int i = 0; i < parameters.Length; i++) {
            ConstParameters.ChangeParam(i, parameters[i]);
        }

        for(int i = 0; i < flags.Length; i++) {
            ConstParameters.SetFlag(i, flags[i]);
        }
    }

    public ParamsData(float[] floats, bool[] bools) {        
        for(int i = 0; i < floats.Length; i++) {
            switch(i) {
                case 0:
                    mutationChance = floats[i];
                    break;
                case 1:
                    minChangeOffset = floats[i];
                    break;
                case 2:
                    maxChangeOffset = floats[i];
                    break;
                case 3:
                    minChangeConst = floats[i];
                    break;
                case 4:
                    maxChangeConst = floats[i];
                    break;
                case 5:
                    minChangeParam = floats[i];
                    break;
                case 6:
                    maxChangeParam = floats[i];
                    break;
                case 7:
                    populationSize = (int) floats[i];
                    break;
                case 8:
                    maxInitDepth = (int) floats[i];
                    break;
                case 9:
                    minChangeA = floats[i];
                    break;
                case 10:
                    maxChangeA = floats[i];
                    break;
                case 11:
                    minChangeB = floats[i];
                    break;
                case 12:
                    maxChangeB = floats[i];
                    break;
                case 13:
                    minChangeC = floats[i];
                    break;
                case 14:
                    maxChangeC = floats[i];
                    break;
                case 15:
                    minChangeScaleX = floats[i];
                    break;
                case 16:
                    maxChangeScaleX = floats[i];
                    break;
                case 17:
                    minChangeScaleY = floats[i];
                    break;
                case 18:
                    maxChangeScaleY = floats[i];
                    break;
                case 19:
                    minChangeScaleVal = floats[i];
                    break;
                case 20:
                    maxChangeScaleVal = floats[i];
                    break;
                case 21:
                    minOffset = floats[i];
                    break;
                case 22:
                    maxOffset = floats[i];
                    break;
                case 23:
                    minConst = floats[i];
                    break;
                case 24:
                    maxConst = floats[i];
                    break;
                case 25:
                    minA = floats[i];
                    break;
                case 26:
                    maxA = floats[i];
                    break;
                case 27:
                    minB = floats[i];
                    break;
                case 28:
                    maxB = floats[i];
                    break;
                case 29:
                    minC = floats[i];
                    break;
                case 30:
                    maxC = floats[i];
                    break;
                case 31:
                    minScaleX = floats[i];
                    break;
                case 32:
                    maxScaleX = floats[i];
                    break;
                case 33:
                    minScaleY = floats[i];
                    break;
                case 34:
                    maxScaleY = floats[i];
                    break;
                case 35:
                    minScaleVal = floats[i];
                    break;
                case 36:
                    maxScaleVal = floats[i];
                    break;
                case 37:
                    minParam = floats[i];
                    break;
                case 38:
                    maxParam = floats[i];
                    break;
                case 39:
                    evoLoopDuration = floats[i];
                    break;
                case 40:
                    mcRewardDuration = floats[i];
                    break;
                case 41:
                    errorThreshold = floats[i];
                    break;
                case 42://ten jeden nowy
                    randoms = (int) floats[i];
                    break;
                case 43:
                    crossings = (int) floats[i];
                    break;
                case 44:
                    stepSqrt = (int) floats[i];
                    break;
                case 45: // nowe
                    depthPenalty = floats[i];
                    break;
                case 46:
                    mutations = (int) floats[i];
                    break;
            }
        }

        for(int i = 0; i < bools.Length; i++) {
            switch(i) {
                case 0:
                    useTime = bools[i];
                    break;
                case 1:
                    useError = bools[i];
                    break;
                case 2:
                    useMSE = bools[i];
                    break;
                case 3:
                    useMC = bools[i];
                    break;
                case 4:
                    useDSSIM = bools[i];
                    break;
                case 5:
                    useABS = bools[i];
                    break;
            }
        }
    }
}
