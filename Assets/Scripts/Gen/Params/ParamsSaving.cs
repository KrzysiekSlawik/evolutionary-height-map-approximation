using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class ParamsSaving : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI warn;

    public UnityEvent OnLoadParams;
    
    public void SaveParams() {
        float[] parameters = new float[47];
        bool[] flags = new bool[6];

        for(int i = 0; i < 47; i++) {
            parameters[i] = ConstParameters.GetParam(i);
        }

        for(int i = 0; i < 6; i++) {
            flags[i] = ConstParameters.GetFlag(i);
        }
        string path;
        FileDialog.TryGetSaveFilePath(out path, FileDialog.xmlFilter);
        ParamsData data = new ParamsData(parameters, flags);
        SaveSystem.SaveParamsXML(data, path);
    }

    public void LoadParams() {
        string path;
        FileDialog.TryGetOpenFilePath(out path, FileDialog.xmlFilter);
        ParamsData data = SaveSystem.LoadParamsXML(path);
        if(data != null) {
            data.UpdateAll();
            OnLoadParams.Invoke();
        } else {
            StartCoroutine(ShowWarn(path, 1.5f));
        }
    }

    IEnumerator ShowWarn(string path, float seconds) {
        warn.text = "<color=red>Saved file not found in</color=red> " + path;
        warn.gameObject.active= true;
        yield return new WaitForSeconds(seconds);
        //warn.enabled = false;
        warn.gameObject.active = false;
    }

    
}
