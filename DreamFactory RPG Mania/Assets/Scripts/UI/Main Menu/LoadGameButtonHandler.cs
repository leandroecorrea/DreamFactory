using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameButtonHandler : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject selectActionParent;
    [SerializeField] private GameObject selectSaveFileParent;

    public void HandleSelectLoadGame()
    {
        selectSaveFileParent.SetActive(true);
        selectActionParent.SetActive(false);
    }
}
