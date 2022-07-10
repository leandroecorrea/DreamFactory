using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] bool isOn = false;
    bool CanInteract = true;
    [SerializeField] bool CanToggle = true;
    [SerializeField] bool PeesurePlate = true;
    [SerializeField] Renderer mat;

    [SerializeField] Animator SwitchAnim;
    [SerializeField] AudioSource SFX;

    [SerializeField] Animator  ConnectedDoor;
    [SerializeField] float animWaitTime = 1f;

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            if (CanToggle)
            {
                CheckState();
            }
            else
            {
                if (CanInteract)
                {
                    CheckState();
                    CanInteract = false;
                }
            }




        }
    }
    public void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            if (CanToggle && PeesurePlate)
            {
                CheckState();

            }

        }
    }

    void CheckState()
    {
        if (isOn)
        {
            Off();

        }
        else
        {
            On();
        }
    }
    void On()
    {

        isOn = true;
        SFX.Play();
        SwitchAnim.SetBool("On", true);
        mat.material.color = Color.green;
        //door 
        StartCoroutine(Open());


    }

    void Off()
    {

        isOn = false;
        SFX.Play();
        SwitchAnim.SetBool("On", false);
        mat.material.color = Color.red;

        StartCoroutine(Close());

    }


    public IEnumerator Open()
    {

        SFX.Play();
        ConnectedDoor.SetBool("Open", true);;
        yield return new WaitForSeconds(animWaitTime);
    }

    public IEnumerator Close()
    {

        SFX.Play();
        ConnectedDoor.SetBool("Open", false);
        yield return new WaitForSeconds(animWaitTime);
        
    }
}
