using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRouter : MonoBehaviour
{
    [Header("Routing Configuration")]
    [SerializeField] private float routingSpeed;
    [SerializeField] private float targetDistance;    
    public event EventHandler onRoutingComplete;

    public void BeginRouting(Vector3 targetToRouteTo)
    {
        StartCoroutine(ExecuteRouting(targetToRouteTo));
    }

    private IEnumerator ExecuteRouting(Vector3 targetToRouteTo)
    {
        Debug.Log(targetToRouteTo);
        while(Vector3.Distance(transform.position, targetToRouteTo) > targetDistance)
        {
            transform.position += (targetToRouteTo - transform.position).normalized * routingSpeed * Time.deltaTime;
            yield return null;
        }
        Debug.Log(transform.position);
        onRoutingComplete?.Invoke(this, EventArgs.Empty);
    }
}
