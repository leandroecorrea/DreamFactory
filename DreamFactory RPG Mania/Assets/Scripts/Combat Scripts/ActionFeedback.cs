using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
        LeanTween.easeInSine(2, 2, 2);
        transform.LeanMoveLocal(new Vector3(0, 20), 1).setEaseOutQuart().setLoopOnce();
    }

    private void Update()
    {
        if (!LeanTween.isTweening())
            Destroy(gameObject);
    }
}
