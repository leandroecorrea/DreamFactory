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

    [Header("Test Data")]
    [SerializeField] private List<CombatEntityConfig> testEnemies;
    [SerializeField] private List<CombatEntityConfig> testPlayers;
    [SerializeField] public int experienceReward = 100;
    [SerializeField] private bool useTestData = true;

    [Header("Settings")]
    [SerializeField] private bool setupCombatOnAwake = true;
    [SerializeField] private bool startCombatOnAwake = true;

    public CombatContext currentTurnContext;

    public Queue<CombatEntity> combatEntities;
    [HideInInspector] public CombatEntity currentTurnEntity;

    public delegate void OnCombatTurnStart(CombatContext ctx);
    private bool isCombatFinished;
    private CombatResult combatResult;
    public event OnCombatTurnStart onCombatTurnStart;

    private void Awake()
    {
        if (setupCombatOnAwake)
        {
            RunSetup();
        }

        if (startCombatOnAwake)
        {
            InitializeTurns();
        }
    }

    public void RunSetup()
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
    }

    public void InitializeTurns()
    {
        currentTurnEntity = combatEntities.Peek();

        // Remove any entities that died last turn
        while (currentTurnEntity == null || currentTurnEntity.gameObject == null)
        {
            combatEntities.Dequeue();
            currentTurnEntity = combatEntities.Peek();
        }

        currentTurnEntity.onTurnComplete += HandleCurrentTurnComplete;

        currentTurnContext = new CombatContext();
        currentTurnContext.playerParty = combatEntities.Where(x => currentStartRequest.allies.Contains(x.entityConfig)).ToList();
        currentTurnContext.enemyParty = combatEntities.Where(x => currentStartRequest.enemies.Contains(x.entityConfig)).ToList();
        //currentTurnContext.playerParty = combatEntities.Where(x => (x as PlayerControllableEntity) != null).ToList();
        //currentTurnContext.enemyParty = combatEntities.Where(x => (x as EnemyCombatEntity) != null).ToList();
        currentTurnContext.currentTurnEntity = currentTurnEntity;

        onCombatTurnStart?.Invoke(currentTurnContext);
    }

    private void HandleCurrentTurnComplete(object sender, OnTurnCompleteEventArgs e)
    {
        currentTurnEntity.onTurnComplete -= HandleCurrentTurnComplete;
        if (isCombatFinished)
        {
            StartCoroutine(WaitAndFinishCombat());
            return;
        }
        // Removing the last combat entity from the queue
        combatEntities.Dequeue();
        combatEntities.Enqueue(currentTurnEntity);

        InitializeTurns();
    }
    private IEnumerator WaitAndFinishCombat()
    {
        yield return new WaitForSeconds(1);

        CombatEventSystem.instance.OnCombatFinished(combatResult);
    }
    private void HandleCombatEntityDeath(object sender, CombatEntityKilledArgs e)
    {
        // Check for Player Victory
        if ((e.entityKilled as EnemyCombatEntity) != null && IsCombatTeamKilled(currentTurnContext.enemyParty))
        {
            combatResult = CombatResult.WIN;
            isCombatFinished = true;
            EncounterHistory.SaveEncounterAsFinished(currentStartRequest.encounter);
            //PlayerPartyManager.UnlockedPartyMembers.ForEach(x => LevelingManager.Instance.Update(ref x.Stats, currentStartRequest.experienceReward));
        }
        else if ((e.entityKilled as PlayerControllableEntity) != null && IsCombatTeamKilled(currentTurnContext.playerParty))
        {
            combatResult = CombatResult.LOSE;
            isCombatFinished = true;            
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

    public void GoBackToOriginScene()
    {
        if (currentStartRequest.originScene != "")
        {
            SceneManager.LoadScene(currentStartRequest.originScene);
            currentStartRequest = null;
        }
    }

}
public static class LevelingManager
{    
    private static readonly Dictionary<int, int> experiencePerLevel = new Dictionary<int, int>
    {
        { 1, 100 },
        { 2, 200 },
        { 3, 345 },
        { 4, 500 },
        {5, 600 },
        {6, 850 },
        {7, 1000 },
        {8, 1500 },
        {9, 2000 },
        {10, 3000 },
        {11, 4000 },
        {12, 5000 }
    };  

    public static void LevelUp(ref LevelingStats stats)
    {
        stats.level++;
        stats.experienceUntilNextLevel = experiencePerLevel[stats.level];
    }
}