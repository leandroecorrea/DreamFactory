using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] protected Transform _defaultFollowTarget;
    [SerializeField] protected float _followDelay;

    [Header("Follow Control")]
    [SerializeField] protected bool _isFollowEnabled = true;

    protected Transform _currentFollowTarget;
    protected Vector3 _followOffset;
    protected Vector3 _followVelocity;

    protected virtual void Awake()
    {
        if (_defaultFollowTarget != null)
        {
            UpdateCameraFollowTarget(_defaultFollowTarget);
        }
    }

    protected void FixedUpdate()
    {
        if (_currentFollowTarget != null && _isFollowEnabled)
        {
            Vector3 targetPosition = _currentFollowTarget.position + _followOffset;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref _followVelocity,
                _followDelay
            );
        }
    }

    public void UpdateCameraFollowTarget(Transform newFollowTarget)
    {
        _currentFollowTarget = newFollowTarget;
        _followOffset = transform.position - _currentFollowTarget.position;
    }
}
