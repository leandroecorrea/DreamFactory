using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class CombatManager : MonoBehaviour
{    
    [SerializeField] private CombatSpawner combatSpawner;    
    [Header("Test Enemies")]
    [SerializeField] private List<CombatEntityConfig> testEnemies;
    [SerializeField] private List<CombatEntityConfig> testPlayers;

    public Queue<CombatEntity> combatEntities;
    [HideInInspector] public CombatEntity currentTurnEntity;
    public delegate void OnCombatTurnStart(CombatContext ctx);
    public event OnCombatTurnStart onCombatTurnStart;   

    private void Awake()
    {
        InitializeCombatManager(new CombatStartRequest(testEnemies, testPlayers));
    }

    public void InitializeCombatManager(CombatStartRequest combatRequest)
    {
        var spawnedEntities = combatSpawner.SpawnParties(combatRequest);
        combatEntities = new Queue<CombatEntity>(spawnedEntities.OrderByDescending(x => x.entityConfig.baseSpeed));
        
        InitializeTurns();        
    }

    public void InitializeTurns()
    {
        currentTurnEntity = combatEntities.Peek();
        while(currentTurnEntity == null || currentTurnEntity.gameObject == null)
        {
            combatEntities.Dequeue();
            currentTurnEntity = combatEntities.Peek();
        }

        currentTurnEntity.onTurnComplete += HandleCurrentTurnComplete;

        var combatContext = new CombatContext();
        combatContext.playerParty = combatEntities.Where(x => (x as PlayerControllableEntity) != null).ToList();
        combatContext.enemyParty = combatEntities.Where(x => (x as EnemyCombatEntity) != null).ToList();
        combatContext.currentTurnEntity = currentTurnEntity;

        onCombatTurnStart?.Invoke(combatContext);
    }

    private void HandleCurrentTurnComplete(object sender, OnTurnCompleteEventArgs e)
    {
        currentTurnEntity.onTurnComplete -= HandleCurrentTurnComplete;

        // Removing the last combat entity from the queue
        combatEntities.Dequeue();
        combatEntities.Enqueue(currentTurnEntity);

        InitializeTurns();
    }
}

public class CombatStartRequest
{
    public List<CombatEntityConfig> enemies;
    public List<CombatEntityConfig> allies;

    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> players)
    {
        this.enemies = enemies;
        this.allies = players;
    }
}

public class CombatContext
{
    public List<CombatEntity> playerParty;
    public List<CombatEntity> enemyParty;

    public CombatEntity currentTurnEntity;
}
