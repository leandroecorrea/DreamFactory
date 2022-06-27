using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBall : MonoBehaviour
{
    [SerializeField] public bool isOpen = false;
    [SerializeField] AudioSource SFX;
    [SerializeField] GameObject Eye;
    [SerializeField] float EffectsWaitTime = 1f;
   // [SerializeField] Material mat;
    [SerializeField] bool isLooking = false;
    [SerializeField] GameObject eyelight;
    [SerializeField] public Animator DoorAnim;
    Vector3 worldUp = Vector3.up;
    // Start is called before the first frame update
    [SerializeField] public GameObject OrientGzaeIdle;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (isLooking)
        {
            Eye.transform.LookAt(GameManager.Instance.Player.transform.position, worldUp );
        }
        else
        {
            Eye.transform.LookAt(OrientGzaeIdle.transform.position,worldUp);
     

        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if(!isOpen)
            {
                isLooking = true;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isLooking = false;
        }
    }

  public void CheckState()
    {
        if (isOpen)
        {
            StartCoroutine(Close());

        }
        else
        {
            StartCoroutine(Open());
        }
    }


  public   IEnumerator Open()
    {

        SFX.Play();
        DoorAnim.SetBool("Open", true);
        eyelight.SetActive(false);
        isOpen = true;
        isLooking = true;
        yield return new WaitForSeconds(EffectsWaitTime);
    }

  public   IEnumerator Close()
    {

        SFX.Play();
        DoorAnim.SetBool("Open", false);
        isOpen = false;
        isLooking = false;
        yield return new WaitForSeconds(EffectsWaitTime);
        eyelight.SetActive(true);
    }

}
