using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroTextScroll : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private InputAction completeTextScroll;

    [Header("Text Scroll Settings")]
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 scrollSpeed;

    [Header("Scene References")]
    [SerializeField] private string targetScene;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI completeTextScrollText;

    private Transform parentTransform;

    private void Awake()
    {
        parentTransform = transform;
        completeTextScroll.performed += HandleCompleteTextScroll;
    }

    private void HandleCompleteTextScroll(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(targetScene);
    }

    private void Update()
    {
        if (parentTransform.localPosition.y < endPosition.y)
        {
            parentTransform.position += scrollSpeed * Time.deltaTime;
        }
        else
        {
            if (!completeTextScroll.enabled)
            {
                completeTextScroll.Enable();
                completeTextScrollText.gameObject.SetActive(true);
            }
        }
    }
}
