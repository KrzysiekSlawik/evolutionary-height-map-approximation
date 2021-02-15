using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    [SerializeField]
    int paramIndex;

    private Toggle paramToggle;
    private ParamsSaving save;

    void Start() {
        paramToggle = gameObject.GetComponent<Toggle>();
        paramToggle.isOn = ConstParameters.GetFlag(paramIndex);
        paramToggle.interactable = !paramToggle.isOn;
        save = FindObjectOfType<ParamsSaving>();
        save.OnLoadParams.AddListener(OnLoad);
    }

    private void OnLoad() {
        paramToggle.isOn = ConstParameters.GetFlag(paramIndex);
        paramToggle.interactable = !paramToggle.isOn;
    }

    public void UpdateToggle() {
        paramToggle.interactable = !paramToggle.isOn;
        ConstParameters.SetFlag(paramIndex, paramToggle.isOn);
    }
}
