using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Test Enemies")]
    [SerializeField] private List<CombatEntityConfig> testEnemies;
    [SerializeField] private List<CombatEntityConfig> testPlayers;

    [Header("Battle Arena Config")]
    [SerializeField] private Vector3 playerControllableArena;
    [SerializeField] private Vector3 enemyArena;
    [SerializeField] private Vector3 arenaSize;

    public Queue<CombatEntity> combatEntities;
    [HideInInspector] public CombatEntity currentTurnEntity;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerControllableArena, arenaSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemyArena, arenaSize);
    }

    private void Awake()
    {
        InitializeCombatManager(new CombatStartRequest(testEnemies, testPlayers));
    }

    public void InitializeCombatManager(CombatStartRequest combatRequest)
    {
        SpawnCombatEntities(combatRequest);
        InitializeTurns();
    }

    public void SpawnCombatEntities(CombatStartRequest combatRequest)
    {
        List<CombatEntity> spawnedCombatEntities = new List<CombatEntity>();
        foreach(var playerEntityConfig in combatRequest.players)
        {
            spawnedCombatEntities.Add(
                GameObject.Instantiate(
                    playerEntityConfig.combatEntityPrefab, 
                    playerControllableArena, 
                    Quaternion.identity
                ).GetComponent<CombatEntity>()
            );
        }

        //float spacing = 1f;
        //var enemyArenaLengthPerEnemy = (arenaSize.magnitude - (spacing * (combatRequest.enemies.Count - 1))) / combatRequest.enemies.Count;

        var enemyArenaLengthPerEnemy = arenaSize.magnitude / combatRequest.enemies.Count;
        int enemiesSpawned = 0;
        foreach (var enemyEntityConfig in combatRequest.enemies)
        {
            var enemyPrefabSize = enemyEntityConfig.combatEntityPrefab.transform.position.x / 2;
            var position = enemyArena;
            position.x = (enemyArena.x - (arenaSize.x / 2)) + (enemyArenaLengthPerEnemy * enemiesSpawned) - enemyPrefabSize; 
            spawnedCombatEntities.Add(
                GameObject.Instantiate(
                    enemyEntityConfig.combatEntityPrefab,
                    position,
                    Quaternion.identity
                ).GetComponent<CombatEntity>()
            );

            enemiesSpawned++;
        }

        combatEntities = new Queue<CombatEntity>(spawnedCombatEntities.OrderByDescending(x => x.entityConfig.baseSpeed));
    }

    public void InitializeTurns()
    {
        currentTurnEntity = combatEntities.Dequeue();

        StartCombat();
    }

    public void StartCombat()
    {
        // Start Current Combat Entity Turn
    }


    public void EndTurn()
    {
        combatEntities.Enqueue(currentTurnEntity);
        currentTurnEntity = combatEntities.Dequeue();
    }
}

public class CombatStartRequest
{
    public List<CombatEntityConfig> enemies;
    public List<CombatEntityConfig> players;

    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> players)
    {
        this.enemies = enemies;
        this.players = players;
    }
}

class CombatContext
{
    public List<CombatEntity> playerParty;
    public List<CombatEntity> enemyParty;
}
