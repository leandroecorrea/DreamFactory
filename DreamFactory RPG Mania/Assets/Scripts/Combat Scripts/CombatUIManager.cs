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
    [SerializeField] private GameObject optionsPanel;
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
            ShowOptionsForPlayer();
            ShowEnemiesList();
            ShowTurnMessage();
        }
        else
        {
            HideUIForPlayer();
            (combatContext.currentTurnEntity as EnemyCombatEntity)?.StartTurn(combatContext);
        }
    }

    private void ShowEnemiesList()
    {
        combatContext.enemyParty.ForEach(enemy =>
        {
            var targetListElement = Instantiate(combatOptionPrefab);
            if (targetListElement.TryGetComponent(out CombatOptionPrefab targetPrefab))
            {
                targetPrefab.optionName.text = enemy.entityConfig.Name;
                targetPrefab.optionButton.onClick.AddListener(delegate {
                    SwitchTarget(enemy.entityConfig);
                });
            }
            targetListElement.transform.SetParent(targetsPanel.transform, false);
        });
    }

    private void SwitchTarget(CombatEntityConfig enemyConfig)
    {
        //if its a healing spell, it should allow to target a friend, not only foes
        currentTarget = combatContext.enemyParty.Where(combatEntity => combatEntity.entityConfig == enemyConfig).FirstOrDefault();
        if (currentTarget != null)
        {
            targetIndicator.gameObject.SetActive(true);
            var dianaPosition = currentTarget.transform.position;
            dianaPosition.y += currentTarget.transform.localScale.y * 2;
            targetIndicator.transform.position = dianaPosition;
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
        optionsPanel.SetActive(false);
        targetIndicator.SetActive(false);
    }

    private void ShowOptionsForPlayer()
    {
        playerUI.SetActive(true);
        targetsPanel.SetActive(true);
        optionsPanel.SetActive(true);

        var currentCharacter = combatContext.currentTurnEntity;
        currentCharacter.entityConfig.actions.ForEach(action =>
        {
            var optionListElement = Instantiate(combatOptionPrefab);

            if (optionListElement.TryGetComponent(out CombatOptionPrefab targetPrefab))
            {
                targetPrefab.optionName.text = action.actionName;
                targetPrefab.optionButton.onClick.AddListener(delegate
                {
                    OnOptionChosenFor(action, currentCharacter);
                });
            }
            optionListElement.transform.SetParent(optionsPanel.transform, false);
        });
    }

    private void OnOptionChosenFor(CombatActionConfig action, CombatEntity currentCharacter)
    {
        Debug.Log("I'm Firing when I'm not suppose to and its annoying");

        if (currentTarget != null)
        {
            CleanUI();

            currentCharacter.PerformAction(action, currentTarget);
            currentTarget = null;
        }
    }

    private void CleanUI()
    {
        
        for (int i = 0; i < targetsPanel.transform.childCount; i++)
        {
            targetsPanel.transform.GetChild(i).GetComponent<CombatOptionPrefab>().optionButton.onClick.RemoveAllListeners();
            GameObject.Destroy(targetsPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < optionsPanel.transform.childCount; i++)
        {
            optionsPanel.transform.GetChild(i).GetComponent<CombatOptionPrefab>().optionButton.onClick.RemoveAllListeners();
            GameObject.Destroy(optionsPanel.transform.GetChild(i).gameObject);
        }
    }
}
