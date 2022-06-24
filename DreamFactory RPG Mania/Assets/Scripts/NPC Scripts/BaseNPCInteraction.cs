using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPCInteraction : MonoBehaviour
{
    [Header("Prefab Refs")]
    [SerializeField] protected GameObject interactionPrefab;

    [Header("Interaction UI Refs")]
    [SerializeField] protected Transform interactionUIMenuParent;
}
