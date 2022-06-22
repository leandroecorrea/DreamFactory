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
        transform.LeanMove(transform.position + new Vector3(0, 10f), 1.5f)
                 .setEasePunch()
                 .setLoopOnce();        
    }

    private IEnumerator LerpTextColor(Color to, float transitionDuration)
    {
        float elapsedTime = 0f;
        while(elapsedTime < transitionDuration)
        {
            yield return null;
            var r = Mathf.Lerp(text.color.r, to.r, transitionDuration);
            var g = Mathf.Lerp(text.color.g, to.g, transitionDuration);
            var b = Mathf.Lerp(text.color.b, to.b, transitionDuration);
            var tempColor = new Color(r, g, b);
            text.color = tempColor;
            elapsedTime += Time.deltaTime;
            Debug.Log($"{r}, {g}, {b}");
        }

    }


    private void Update()
    {
        if (!LeanTween.isTweening())
            Destroy(gameObject);
    }
}
