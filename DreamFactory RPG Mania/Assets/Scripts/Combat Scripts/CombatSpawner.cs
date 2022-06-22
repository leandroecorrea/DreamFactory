using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatSpawner : MonoBehaviour
{   

    [Header("Spawning Configurations")]
    [SerializeField] private int rowCapacity;
    [SerializeField] private Vector3 rowSize;
    [SerializeField] private Vector3 playerTeamArenaOrigin;
    [SerializeField] private Vector3 enemyTeamArenaOrigin;

    public List<CombatEntity> SpawnParties(CombatStartRequest request)
    {
        var parties = new List<CombatEntity>();
        parties.AddRange(SpawnEntities(playerTeamArenaOrigin, enemyTeamArenaOrigin, request.allies.Select(x => x.combatEntityPrefab).ToList()));
        parties.AddRange(SpawnEntities(enemyTeamArenaOrigin, playerTeamArenaOrigin, request.enemies.Select(x => x.combatEntityPrefab).ToList()));
        return parties;
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

    private List<CombatEntity> SpawnEntities(Vector3 playerTeamArenaOrigin, Vector3 opposingArenaOrigin, List<GameObject> entitiesToSpawn)
    {
        List<CombatEntity> entities = new List<CombatEntity>();
        // If only one entity, place them dead center
        if (entitiesToSpawn.Count == 1)
        {
            var entity = GameObject.Instantiate(entitiesToSpawn[0], playerTeamArenaOrigin, Quaternion.identity);
            entity.transform.LookAt(new Vector3(opposingArenaOrigin.x, transform.position.y, opposingArenaOrigin.z), Vector3.up);

            entities.Add(entity.GetComponent<CombatEntity>());
            return entities;
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
                GameObject entity = GameObject.Instantiate(entitiesToSpawn[runningEntitiySpawnIndex], currentRowOrigin, Quaternion.identity);
                entity.transform.LookAt(new Vector3(opposingArenaOrigin.x, transform.position.y, opposingArenaOrigin.z), Vector3.up);

                entities.Add(entity.GetComponent<CombatEntity>());
                break;
            }

            float xIncrement = rowSize.x / (entitiesInRow - 1);
            for (int i = 0; i < entitiesInRow; i++)
            {
                var entity = GameObject.Instantiate(entitiesToSpawn[runningEntitiySpawnIndex++], runningPosition, Quaternion.identity);
                entity.transform.LookAt(new Vector3(opposingArenaOrigin.x, transform.position.y, opposingArenaOrigin.z), Vector3.up);

                entities.Add(entity.GetComponent<CombatEntity>());
                runningPosition.x += xIncrement;
            }

            currentRowOrigin.z -= rowSize.z;
        }
        return entities;
    }
}
