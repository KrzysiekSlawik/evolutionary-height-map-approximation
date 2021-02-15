using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SliderManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField input;
    [SerializeField]
    float maxValue;
    [SerializeField]
    float minValue;
    [SerializeField]
    int paramIndex;

    private Slider featureSlider;
    private char goodSep = ',';
    private ParamsSaving save;


    void Awake() {
        featureSlider = gameObject.GetComponent<Slider>();
        featureSlider.maxValue = maxValue;
        featureSlider.minValue = minValue;
        featureSlider.value = ConstParameters.GetParam(paramIndex);
        goodSep = Application.isEditor ? ',' : '.';
        input.text = ValidString(featureSlider.value.ToString());
        //ConstParameters.ChangeParam(paramIndex, featureSlider.value);
        save = FindObjectOfType<ParamsSaving>();
        save.OnLoadParams.AddListener(OnLoad);
    }

    private void OnLoad() {
        featureSlider.value = ConstParameters.GetParam(paramIndex);
    }

    public void SliderChange() {
        input.text = (Mathf.Round(featureSlider.value * 100f) / 100f).ToString();
        ConstParameters.ChangeParam(paramIndex, featureSlider.value);
    }

    public void EndInput() {
        if(input.text == "") 
            featureSlider.value = 0.0f;
        else
            featureSlider.value = Single.Parse(input.text);
        ConstParameters.ChangeParam(paramIndex, featureSlider.value);
    }

    public void OnInput() {
        string valid = ValidString(input.text);
        input.text = valid;
    }

    private string ValidString(string text) {
        string valid = "";
        bool wasSep = false;
        for(int i = 0; i < text.Length; i++) {
            if((text[i] >= '0' && text[i] <= '9') || (text[i] == '-' && i == 0)){
                valid += text[i];
            } else if(text[i] == goodSep && !wasSep) {
                valid += text[i];
                wasSep = true;
            }
        }
        return valid;
    }

    public float GetFeatureValue() {
        return featureSlider.value;
    }
}