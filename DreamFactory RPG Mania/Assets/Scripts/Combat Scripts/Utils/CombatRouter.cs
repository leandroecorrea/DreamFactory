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
        targetDistance = 0.1f;
        snapToPosition = true;

        StartCoroutine(ExecuteRouting(targetToRouteTo));
    }

    private IEnumerator ExecuteRouting(Vector3 targetToRouteTo)
    {
        while(Vector3.Distance(transform.position, targetToRouteTo) > targetDistance)
        {
            Vector3 diffVector = (targetToRouteTo - transform.position).normalized;
            transform.position += new Vector3(diffVector.x, 0f, diffVector.z) * routingSpeed * Time.deltaTime;

            yield return null;
        }

        if (snapToPosition)
        {
            transform.position = targetToRouteTo;
        }

        onRoutingComplete?.Invoke(this, EventArgs.Empty);
    }
}
