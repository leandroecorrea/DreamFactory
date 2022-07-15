using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Animator cutsceneAnim;

    public void ToggleIsMoving(bool isMoving)
    {
        cutsceneAnim.SetBool("IsMoving", isMoving);
    }

    public void ToggleTalkingAnimation(bool isTalking)
    {
        cutsceneAnim.SetBool("IsTalking", isTalking);
    }
}
