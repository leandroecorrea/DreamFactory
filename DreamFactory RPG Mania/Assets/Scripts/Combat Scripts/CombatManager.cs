using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public static CombatStartRequest currentStartRequest;

    [SerializeField] private CombatSpawner combatSpawner;
    
    [Header("Test Enemies")]
    [SerializeField] private List<CombatEntityConfig> testEnemies;
    [SerializeField] private List<CombatEntityConfig> testPlayers;
    [SerializeField] private bool useTestData = true;

    public CombatContext currentTurnContext;

    public Queue<CombatEntity> combatEntities;
    [HideInInspector] public CombatEntity currentTurnEntity;

    public delegate void OnCombatTurnStart(CombatContext ctx);
    public event OnCombatTurnStart onCombatTurnStart;

    private void Awake()
    {
        if (useTestData)
        {
            InitializeCombatManager(new CombatStartRequest(testEnemies, testPlayers, ""));
            return;
        }
        
        InitializeCombatManager(currentStartRequest);
    }


    private void HandleItemUsedInCombat(string itemHandler)
    {
        var item = InventoryManager.GetAll().Find(x => x.data.actionConfig.actionHandlerClassName == itemHandler);
        if (item != null)
            InventoryManager.Consume(item);
    }
    public void InitializeCombatManager(CombatStartRequest combatRequest)
    {
        var spawnedEntities = combatSpawner.SpawnParties(combatRequest);
        combatEntities = new Queue<CombatEntity>(spawnedEntities.OrderByDescending(x => x.entityConfig.baseSpeed));

        CombatEventSystem.instance.onCombatEntityKilled += HandleCombatEntityDeath;
        CombatEventSystem.instance.onItemUsedInCombat += HandleItemUsedInCombat;    
        InitializeTurns();        
    }

    public void InitializeTurns()
    {
        currentTurnEntity = combatEntities.Peek();

        // Remove any entities that died last turn
        while(currentTurnEntity == null || currentTurnEntity.gameObject == null)
        {
            combatEntities.Dequeue();
            currentTurnEntity = combatEntities.Peek();
        }

        currentTurnEntity.onTurnComplete += HandleCurrentTurnComplete;

        currentTurnContext = new CombatContext();
        currentTurnContext.playerParty = combatEntities.Where(x => (x as PlayerControllableEntity) != null).ToList();
        currentTurnContext.enemyParty = combatEntities.Where(x => (x as EnemyCombatEntity) != null).ToList();
        currentTurnContext.currentTurnEntity = currentTurnEntity;

        onCombatTurnStart?.Invoke(currentTurnContext);
    }

    private void HandleCurrentTurnComplete(object sender, OnTurnCompleteEventArgs e)
    {
        currentTurnEntity.onTurnComplete -= HandleCurrentTurnComplete;

        // Removing the last combat entity from the queue
        combatEntities.Dequeue();
        combatEntities.Enqueue(currentTurnEntity);

        InitializeTurns();
    }

    private void HandleCombatEntityDeath(object sender, CombatEntityKilledArgs e)
    {
        // Check for Player Victory
        if ((e.entityKilled as EnemyCombatEntity) != null && IsCombatTeamKilled(currentTurnContext.enemyParty))
        {
            HandlePlayerVictory();
        }
        else if ((e.entityKilled as PlayerControllableEntity) != null && IsCombatTeamKilled(currentTurnContext.playerParty))
        {
            HandlePlayerLose();
        }
    }

    private bool IsCombatTeamKilled(List<CombatEntity> team)
    {
        bool allEntitiesKilled = true;
        foreach (CombatEntity combatEntity in team)
        {
            if (combatEntity != null && combatEntity.gameObject != null && !combatEntity.IsDead())
            {
                allEntitiesKilled = false;
                break;
            }
        }

        return allEntitiesKilled;
    }
    
    private void HandlePlayerVictory()
    {
        if (currentStartRequest.originScene != "")
        {
            SceneManager.LoadScene(currentStartRequest.originScene);
            currentStartRequest = null;
        }
    }

    private void HandlePlayerLose()
    {
        Debug.Log("Players Lose...");
    }
}

public class CombatStartRequest
{
    public List<CombatEntityConfig> enemies;
    public List<CombatEntityConfig> allies;
    public string originScene;

    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> players, string originScene)
    {
        this.enemies = enemies;
        this.allies = players;
        this.originScene = originScene;
    }
}

public class CombatContext
{
    public List<CombatEntity> playerParty;
    public List<CombatEntity> enemyParty;

    public CombatEntity currentTurnEntity;
}
