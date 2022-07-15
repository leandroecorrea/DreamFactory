using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneAnimController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Animator cutsceneAnim;

    [Header("Events")]
    [SerializeField] private UnityEvent animationComplete;

    public void ToggleIsMoving()
    {
        cutsceneAnim.SetBool("IsMoving", !cutsceneAnim.GetBool("IsMoving"));
    }

    public void ToggleIsMoving(bool isMoving)
    {
        cutsceneAnim.SetBool("IsMoving", isMoving);
    }

    public void ToggleTalkingAnimation(bool isTalking)
    {
        cutsceneAnim.SetBool("IsTalking", isTalking);
    }

    public void NotifyAnimationComplete()
    {
        animationComplete?.Invoke();
    }
}
