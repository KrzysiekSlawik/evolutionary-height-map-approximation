using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent OnImproveEvo;
    public UnityEvent OnRestartEvo;
    public UnityEvent OnSubmit;

    public void Restart() {
        OnRestartEvo.Invoke();
    }

    public void Improve() {
        OnImproveEvo.Invoke();
    }

    public void Submit() {
        OnSubmit.Invoke();
    }
}
