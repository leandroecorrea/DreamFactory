using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxController : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(PlayerCameraController.instance.transform, transform.up);
    }
}
