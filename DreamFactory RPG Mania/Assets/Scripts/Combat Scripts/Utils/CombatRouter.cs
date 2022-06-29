using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRouter : MonoBehaviour
{
    [Header("Routing Configuration")]
    [SerializeField] private float routingSpeed;

    private float targetDistance;
    private bool snapToPosition;
    public event EventHandler onRoutingComplete;

    private Quaternion savedRotation;

    public void SaveRotation()
    {
        savedRotation = transform.rotation;
    }

    public void RestoreRotation()
    {
        transform.rotation = savedRotation;
    }

    public void BeginRouting(GameObject targetToRouteTo)
    {
        targetDistance = targetToRouteTo.transform.localScale.magnitude;
        snapToPosition = false;

        transform.LookAt(new Vector3(targetToRouteTo.transform.position.x, transform.position.y, targetToRouteTo.transform.position.z));
        StartCoroutine(ExecuteRouting(targetToRouteTo.transform.position));
    }

    public void BeginRouting(Vector3 targetToRouteTo)
    {
        targetDistance = 30f;
        snapToPosition = true;
        transform.LookAt(new Vector3(targetToRouteTo.x, transform.position.y, targetToRouteTo.z));
        StartCoroutine(ExecuteRouting(targetToRouteTo));
    }

    private IEnumerator ExecuteRouting(Vector3 targetToRouteTo)
    {
        Vector3 adjustedPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 adjustedTargetRoutePosition = new Vector3(targetToRouteTo.x, 0f, targetToRouteTo.z);

        while (Vector3.Distance(adjustedPosition, adjustedTargetRoutePosition) > targetDistance)
        {
            Vector3 diffVector = (adjustedTargetRoutePosition - adjustedPosition).normalized;
            transform.position += new Vector3(diffVector.x, 0f, diffVector.z) * routingSpeed * Time.deltaTime;

            adjustedPosition = new Vector3(transform.position.x, 0f, transform.position.z);
            yield return null;
        }

        if (snapToPosition)
        {
            transform.position = targetToRouteTo;
        }

        onRoutingComplete?.Invoke(this, EventArgs.Empty);
    }

    public void MakeLookTo(params CombatEntity[] targets)
    {
        var average = GetAveragePositionFor(targets);        
        transform.LookAt(average);
    }

    private Vector3 GetAveragePositionFor(CombatEntity[] targets)
    {
        var accumulation = Vector3.zero;
        for (int i = 0; i < targets.Length; i++)
        {
            accumulation+=targets[i].transform.position;
        }
        return accumulation / targets.Length;
    }
}
