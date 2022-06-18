using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSpawner : MonoBehaviour
{
    [Header("Testing Variables")]
    [SerializeField] private List<GameObject> enemiesToSpawn;
    [SerializeField] private List<GameObject> alliesToSpawn; 

    [Header("Spawning Configurations")]
    [SerializeField] private int rowCapacity;
    [SerializeField] private Vector3 rowSize;
    [SerializeField] private Vector3 playerTeamArenaOrigin;
    [SerializeField] private Vector3 enemyTeamArenaOrigin;

    private void Awake()
    {
        SpawnEntities(playerTeamArenaOrigin, alliesToSpawn);
        SpawnEntities(enemyTeamArenaOrigin, enemiesToSpawn);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(playerTeamArenaOrigin, 0.5f);
        Gizmos.DrawWireCube(playerTeamArenaOrigin, rowSize);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(enemyTeamArenaOrigin, 0.5f);
        Gizmos.DrawWireCube(enemyTeamArenaOrigin, rowSize);
    }

    public void SpawnEntities(Vector3 playerTeamArenaOrigin, List<GameObject> entitiesToSpawn)
    {
        // If only one entity, place them dead center
        if (entitiesToSpawn.Count == 1)
        {
            GameObject.Instantiate(entitiesToSpawn[0], playerTeamArenaOrigin, Quaternion.identity);
            return;
        }

        // Otherwise, start by spawning from the edge
        Vector3 currentRowOrigin = playerTeamArenaOrigin;
        int targetRowCount = Mathf.CeilToInt(entitiesToSpawn.Count / (float)rowCapacity);
        int runningEntitiySpawnIndex = 0;

        for (int rowIndex = 0; rowIndex < targetRowCount; rowIndex++)
        {
            Vector3 startPosition = new Vector3(currentRowOrigin.x - (rowSize.x / 2), currentRowOrigin.y, currentRowOrigin.z);
            Vector3 runningPosition = startPosition;

            int entitiesInRow = Mathf.Min((entitiesToSpawn.Count - runningEntitiySpawnIndex), rowCapacity);
            if (entitiesInRow == 1)
            {
                GameObject.Instantiate(entitiesToSpawn[runningEntitiySpawnIndex], currentRowOrigin, Quaternion.identity);
                break;
            }

            float xIncrement = rowSize.x / (entitiesInRow - 1);
            for (int i = 0; i < entitiesInRow; i++)
            {
                GameObject.Instantiate(entitiesToSpawn[runningEntitiySpawnIndex++], runningPosition, Quaternion.identity);
                runningPosition.x += xIncrement;
            }

            currentRowOrigin.z -= rowSize.z;
        }
    }
}
