using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusManager : MonoBehaviour
{
    TextMeshProUGUI statusBar;
    FileReceiver receiver;
    MapLoader loader;

    void Start() {
        statusBar = gameObject.GetComponent<TextMeshProUGUI>();
        receiver = FindObjectOfType<FileReceiver>();
        loader = FindObjectOfType<MapLoader>();
        receiver.loadSucces.AddListener(Loaded);
        receiver.loadFailed.AddListener(ErrorLoading);
        receiver.saveSucces.AddListener(Saved);
        receiver.saveFailed.AddListener(ErrorSaving);
        loader.OnFinishComputing.AddListener(Ready);
    }

    public void Computing() {
        statusBar.text = "COMPUTING";
        Invoke("Test", 0.02f);
    }

    IEnumerator Test() {
        yield return new WaitForEndOfFrame();
    }

    public void Ready() {
        statusBar.text = "READY";
    }

    public void Loaded() {
        StartCoroutine(ChangeText("File Succesfully Loaded", 3f));
    }

    public void Saved() {
        StartCoroutine(ChangeText("File Succesfully Saved", 3f));
    }

    public void ErrorSaving() {
        StartCoroutine(ChangeText("Error Saving File", 3f));
    }

    public void ErrorLoading() {
        StartCoroutine(ChangeText("Error Loading File", 3f));
    }

    IEnumerator ChangeText(string text, float seconds) {
        string oldText = statusBar.text;
        statusBar.text = text;
        yield return new WaitForSeconds(seconds);
        statusBar.text = oldText;
    }

}
