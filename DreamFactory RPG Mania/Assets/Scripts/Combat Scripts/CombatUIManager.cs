using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CombatUIManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private CombatManager combatManager;
    [Header("Player UI")]
    [SerializeField] private GameObject playerUI;
    [SerializeField] private TMP_Text turnMessage;
    [SerializeField] private GameObject targetsPanel;
    [SerializeField] private GameObject actionsPanel;
    [SerializeField] private GameObject combatOptionPrefab;
    [SerializeField] private GameObject targetIndicator;
    private CombatEntity currentTarget;
    private CombatContext combatContext;

    private void OnEnable()
    {
        combatManager.onCombatTurnStart += handleCombatTurnStart;
    }

    private void handleCombatTurnStart(CombatContext ctx)
    {
        combatContext = ctx;
        if (combatContext.currentTurnEntity is PlayerControllableEntity)
        {
            ShowActions();            
            ShowTurnMessage();
        }
        else
        {
            HideUIForPlayer();
            (combatContext.currentTurnEntity as EnemyCombatEntity)?.StartTurn(combatContext);
        }
    }


    private void SwitchTarget(CombatEntityConfig enemyConfig)
    {        
        currentTarget = combatContext.enemyParty.Where(combatEntity => combatEntity.entityConfig == enemyConfig).FirstOrDefault();
        if (currentTarget != null)
        {
            targetIndicator.gameObject.SetActive(true);
            var indicatorPosition = currentTarget.transform.position;
            indicatorPosition.y += currentTarget.transform.localScale.y * 2;
            targetIndicator.transform.position = indicatorPosition;
            turnMessage.text = $"Enemy {currentTarget.entityConfig.Name} is being targeted";
        }
    }

    private void ShowTurnMessage()
    {
        turnMessage.text = "It's your turn!";
    }

    private void HideUIForPlayer()
    {
        targetsPanel.SetActive(false);
        actionsPanel.SetActive(false);
        targetIndicator.SetActive(false);
    }

    private void ShowActions()
    {
        playerUI.SetActive(true);
        actionsPanel.SetActive(true);

        var currentCharacter = combatContext.currentTurnEntity;
        currentCharacter.entityConfig.actions.ForEach(action =>
        {
            var optionListElement = Instantiate(combatOptionPrefab);
            if (optionListElement.TryGetComponent(out CombatOptionPrefab targetPrefab))
            {
                targetPrefab.optionName.text = action.actionName;
                targetPrefab.optionButton.onClick.AddListener(delegate
                {
                    OnActionChosen(action);
                });
                Debug.Log($"Listener added for {action.actionName} in button {targetPrefab.optionButton.gameObject}");
                var button = targetPrefab.optionButton;
            }
            optionListElement.transform.SetParent(actionsPanel.transform, false);
        });
    }

    private void OnActionChosen(CombatActionConfig action)
    {
        CleanUIFor(actionsPanel);
        //Don't like this switch
        switch (action.combatActionType)
        {
            case CombatActionType.ATTACK:
                ShowEnemiesList();
                //Show targets list and target indicator
                break;
            case CombatActionType.SPELL:
                //Show spells UI
                break;
            case CombatActionType.ITEM:
            //Show all items in inventory
            case CombatActionType.RUN:
            //Try to run
            default:
                //Do nothing
                break;
        }

    }


    private void ShowEnemiesList()
    {
        targetsPanel.gameObject.SetActive(true);
        combatContext.enemyParty.ForEach(enemy =>
        {
            var targetListElement = Instantiate(combatOptionPrefab);
            if (targetListElement.TryGetComponent(out CombatOptionPrefab targetPrefab))
            {
                targetPrefab.optionName.text = enemy.entityConfig.Name;
                targetPrefab.optionButton.onClick.AddListener(delegate
                {
                    SwitchTarget(enemy.entityConfig);
                });                
            }
            targetListElement.transform.SetParent(targetsPanel.transform, false);
        });
    }



    private void ReceiveInputFor(CombatActionConfig action)
    {
        var currentCharacter = combatContext.currentTurnEntity;
        if (currentTarget != null)
        {            
            currentCharacter.PerformAction(action, currentTarget);
            currentTarget = null;
        }
    }

    private void CleanUIFor(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).GetComponent<CombatOptionPrefab>().optionButton.onClick.RemoveAllListeners();
            GameObject.Destroy(panel.transform.GetChild(i).gameObject);
        }
        panel.gameObject.SetActive(false);
    }
    
}
