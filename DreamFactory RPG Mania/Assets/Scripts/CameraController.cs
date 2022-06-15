using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform _defaultFollowTarget;
    [SerializeField] private float _followDelay;

    [Header("Follow Control")]
    [SerializeField] private bool _isFollowEnabled = true;

    private Transform _currentFollowTarget;
    private Vector3 _followOffset;
    private Vector3 _followVelocity;

    private void Awake()
    {
        if (_defaultFollowTarget != null)
        {
            UpdateCameraFollowTarget(_defaultFollowTarget);
        }
    }

    private void FixedUpdate()
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
