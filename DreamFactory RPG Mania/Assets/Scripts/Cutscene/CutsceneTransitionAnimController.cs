using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneTransitionAnimController : MonoBehaviour
{
    public UnityEvent onTransitionComplete;

    public void HandleTransitionComplete()
    {
        onTransitionComplete?.Invoke();
    }
}
