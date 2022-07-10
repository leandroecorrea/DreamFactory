using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBetweenPanels : MonoBehaviour
{
    public Animator anim;
    public float TranstionTime = 1.5f;
    public GameObject Open;
    public GameObject Close;

    public void TriggerFadeOpen()
    {
        StartCoroutine(WaitTillFadedToOpen());
    }
   
    IEnumerator WaitTillFadedToOpen()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(TranstionTime);

        Open.SetActive(true);
        Close.SetActive(false);
        anim.SetTrigger("End");
    }
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
