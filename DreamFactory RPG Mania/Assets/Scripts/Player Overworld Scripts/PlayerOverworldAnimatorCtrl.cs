using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldAnimatorCtrl : MonoBehaviour
{
    [Header("Component Refs")]
    [SerializeField] private Animator playerOverworldAnim;

    public void NotifyMovementUpdate(bool isMoving)
    {
        playerOverworldAnim.SetBool("IsMoving", isMoving);
    }

    public void StopMovement()
    {
        playerOverworldAnim.SetBool("IsMoving", false);
    }

    public void StartTalking()
    {
        playerOverworldAnim.SetBool("IsTalking", true);
    }

    public void StopTalking()
    {
        playerOverworldAnim.SetBool("IsTalking", false);
    }
}
