using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Animator cutsceneAnim;

    private Vector3 lastPosition;

    private void LateUpdate()
    {
        cutsceneAnim.SetBool("IsMoving", Mathf.Abs((transform.position - lastPosition).magnitude) >= 0.1f);
        lastPosition = transform.position;
    }

    public void ToggleTalkingAnimation(bool isTalking)
    {
        cutsceneAnim.SetBool("IsTalking", isTalking);
    }
}
