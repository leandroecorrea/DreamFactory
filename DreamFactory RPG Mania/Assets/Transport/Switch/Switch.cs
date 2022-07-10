using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] bool isOn = false;
       bool CanInteract = true;
    [SerializeField] bool CanToggle = true;
    [SerializeField] bool PeesurePlate = true;
    [SerializeField] Renderer mat;

    [SerializeField] Animator SwitchAnim;
    [SerializeField] AudioSource SFX;
  

    [SerializeField] EyeBall ConnectedDoor;


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
    public  void OnTriggerExit(Collider other)
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
           StartCoroutine(ConnectedDoor.Open());


    }

        void Off()
        {

           isOn = false;
             SFX.Play();
          SwitchAnim.SetBool("On", false);
          mat.material.color = Color.red;

        //door 
        StartCoroutine(ConnectedDoor.Close());



    }
    }
