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



    public void BeginRouting(GameObject targetToRouteTo)
    {
        targetDistance = targetToRouteTo.transform.localScale.magnitude;
        snapToPosition = false;

        StartCoroutine(ExecuteRouting(targetToRouteTo.transform.position));
    }

    public void BeginRouting(Vector3 targetToRouteTo)
    {
        targetDistance = 0.01f;
        snapToPosition = true;

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

            Debug.Log(Vector3.Distance(adjustedPosition, adjustedTargetRoutePosition));
            adjustedPosition = new Vector3(transform.position.x, 0f, transform.position.z);
            yield return null;
        }

        if (snapToPosition)
        {
            transform.position = targetToRouteTo;
        }

        onRoutingComplete?.Invoke(this, EventArgs.Empty);
    }
}
