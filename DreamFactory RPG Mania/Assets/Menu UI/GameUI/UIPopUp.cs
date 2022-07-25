using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class UIPopUp : MonoBehaviour
{
    [Header("Interaction Popup")]
    [SerializeField] GameObject Hud;
    [SerializeField] GameObject CurrentPopUPUI;
    [SerializeField] GameObject PopUPUIPrefab;

    private void Start()
    {
        Hud = GameManager.Instance.HUD;
    }
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (CurrentPopUPUI != null)
            {
                Destroy(CurrentPopUPUI);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

         CurrentPopUPUI = MakePopUp();
        GameManager.Instance.CanPause = false;

    }
    private void OnTriggerExit(Collider other)
    {
        if (CurrentPopUPUI != null)
        { 
        Destroy(CurrentPopUPUI);
        }
        GameManager.Instance.CanPause = true;

    }

    GameObject MakePopUp()
    {
       return GameObject.Instantiate(PopUPUIPrefab, Hud.transform);

    }
    public void OpenInteractionDispaly()
    {
        Hud.SetActive(true);
    }
    public void CloseInteractionDispaly()
    {
        Hud.SetActive(false);
    }
}
