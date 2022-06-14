using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform defaultFollowTarget;
    [SerializeField] private float followDelay;

    [Header("Follow Control")]
    [SerializeField] private bool isFollowEnabled = true;

    private Transform currentFollowTarget;
    private Vector3 followOffset;
    private Vector3 followVelocity;

    private void Awake()
    {
        if (defaultFollowTarget != null)
        {
            UpdateCameraFollowTarget(defaultFollowTarget);
        }
    }

    private void LateUpdate()
    {
        if (currentFollowTarget != null && isFollowEnabled)
        {
            Vector3 targetPosition = currentFollowTarget.position + followOffset;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref followVelocity,
                followDelay
            );
        }
    }

    public void UpdateCameraFollowTarget(Transform newFollowTarget)
    {
        currentFollowTarget = newFollowTarget;
        followOffset = transform.position - currentFollowTarget.position;
    }
}
