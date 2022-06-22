using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ActionFeedback : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
        transform.LeanMove(transform.position + new Vector3(0, 10f), 1f)
                 .setEasePunch()
                 .setLoopOnce();       
    }

    private void SetColor(Color c)
    {
        text.color = c;
    }
    private void Update()
    {
        if (!LeanTween.isTweening())
            Destroy(gameObject);
    }
}
