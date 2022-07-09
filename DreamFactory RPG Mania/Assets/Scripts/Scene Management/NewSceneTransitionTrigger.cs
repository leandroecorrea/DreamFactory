using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneTransitionTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private string targetSceneName;
    [SerializeField] private Vector3 targetSpawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneTransitionManager.LoadNewSingleScene(targetSceneName);
            PlayerOverworldPersistance.persistance.gameObject.transform.position = targetSpawnPosition;
        }
    }
}
