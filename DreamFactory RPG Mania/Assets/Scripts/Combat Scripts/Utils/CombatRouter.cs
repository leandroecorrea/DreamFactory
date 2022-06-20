using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRouter : MonoBehaviour
{
    [Header("Routing Configuration")]
    [SerializeField] private float routingSpeed;
    [SerializeField] private float targetDistance;
    [SerializeField] private float attackArea;
    public event EventHandler onRoutingComplete;

    public void BeginRouting(Vector3 targetToRouteTo)
    {
        StartCoroutine(ExecuteRouting(targetToRouteTo));
    }

    private IEnumerator ExecuteRouting(Vector3 targetToRouteTo)
    {
        while(Vector3.Distance(transform.position, targetToRouteTo) > targetDistance)
        {
            transform.position += (targetToRouteTo - transform.position).normalized * routingSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetToRouteTo) < attackArea)
            {
                CombatEventSystem.instance.OnAttackAreaTrigger(gameObject.GetComponent<CombatEntity>());
                Debug.Log("Trigger de ataque");
            }
            yield return null;
        }

        onRoutingComplete?.Invoke(this, EventArgs.Empty);
    }
}
