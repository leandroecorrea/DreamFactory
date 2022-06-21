using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CombatUIManager : MonoBehaviour, ITargetUpdatable
{
    [Header("Refs")]
    [SerializeField] private CombatManager combatManager;
    [Header("Player UI")]
    [SerializeField] private GameObject playerUI;
    [SerializeField] private TMP_Text turnMessage;
    [SerializeField] private GameObject targetsPanel;
    [SerializeField] private GameObject actionsPanel;
    [SerializeField] private GameObject charactersPanel;
    [SerializeField] private GameObject targetIndicator;
    [Header("Prefabs")]
    [SerializeField] private GameObject combatOptionPrefab;
    [SerializeField] private GameObject targetOptionPrefab;
    [SerializeField] private GameObject characterStatsPrefab;
    [SerializeField] private GameObject actionFeedbackPrefab;    
    private CombatEntity currentTarget;
    private CombatContext combatContext;
    private CombatActionConfig currentAction;    

    private void OnEnable()
    {
        combatManager.onCombatTurnStart += handleCombatTurnStart;        
        CombatEventSystem.instance.onActionPerformed += CombatEventSystem_onActionPerformed;
    }

    private void CombatEventSystem_onActionPerformed(object sender, ActionPerformedArgs e)
    {        
        var currentTarget = e.TargetedUnits.FirstOrDefault();
        var feedback = Instantiate(actionFeedbackPrefab, currentTarget.transform.position, Quaternion.identity);
        feedback.GetComponent<TMP_Text>().text = e.ActionPerformed.ToString();
        feedback.transform.SetParent(playerUI.transform, false);
    }

    private void handleCombatTurnStart(CombatContext ctx)
    {        
        combatContext = ctx;        
        ShowCharactersPanel();        
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

    private void CleanTargets(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).GetComponent<TargetOptionPrefab>().optionButton.onClick.RemoveAllListeners();
            Destroy(panel.transform.GetChild(i).gameObject);
        }
        panel.gameObject.SetActive(false);
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
                var button = targetPrefab.optionButton;
            }
            optionListElement.transform.SetParent(actionsPanel.transform, false);
        });
    }

    private void OnActionChosen(CombatActionConfig action)
    {
        //CleanCombatOptions(actionsPanel);
        currentAction = action;
        //Don't like this switch
        switch (action.targetType)
        {
            case TargetType.ENEMIES:
                CleanCombatActions(actionsPanel);
                CleanTargets(targetsPanel);
                ShowEnemiesList();
                //Show targets list and target indicator
                break;
            case TargetType.ALLIES:
                CleanCombatActions(actionsPanel);
                CleanTargets(targetsPanel);
                ShowAlliesList();
                //Show spells UI
                break;            
            default:
                //Do nothing
                break;
        }
    }

    private void ShowAlliesList()
    {
        actionsPanel.SetActive(false);
        targetsPanel.gameObject.SetActive(true);
        combatContext.playerParty.ForEach(ally =>
        {
            var targetListElement = Instantiate(targetOptionPrefab);
            if (targetListElement.TryGetComponent(out TargetOptionPrefab targetPrefab))
            {
                targetPrefab.optionName.text = ally.entityConfig.Name;
                targetPrefab.Attach(ally.entityConfig);
                targetPrefab.Subscribe(this);
                targetPrefab.optionButton.onClick.AddListener(delegate
                {
                    ReceiveInputFor(currentAction);
                });
            }
            targetListElement.transform.SetParent(targetsPanel.transform, false);
        });
    }

    private void ShowCharactersPanel()
    {
        CleanCharacterStats(charactersPanel);
        combatContext.playerParty.ForEach(character =>
        {
            var statsPrefab = Instantiate(characterStatsPrefab);
            var component = statsPrefab.GetComponent<CharacterStatsPrefab>();
            component.characterName.text = character.entityConfig.Name;
            component.HP.text = $"{character.CurrentHP}/{character.entityConfig.baseHP}";
            component.MP.text = $"{character.CurrentMP}/{character.entityConfig.baseMP}";
            statsPrefab.transform.SetParent(charactersPanel.transform, false);
        });
    }
    private void ShowEnemiesList()
    {
        actionsPanel.SetActive(false);
        targetsPanel.gameObject.SetActive(true);
        combatContext.enemyParty.ForEach(enemy =>
        {
            var targetListElement = Instantiate(targetOptionPrefab);
            if (targetListElement.TryGetComponent(out TargetOptionPrefab targetPrefab))
            {
                targetPrefab.optionName.text = enemy.entityConfig.Name;
                targetPrefab.Attach(enemy.entityConfig);
                targetPrefab.Subscribe(this);
                targetPrefab.optionButton.onClick.AddListener(delegate
                {
                    ReceiveInputFor(currentAction);
                });
            }
            targetListElement.transform.SetParent(targetsPanel.transform, false);
        });
    }
    public void SwitchTarget(CombatEntityConfig enemyConfig)
    {
        var allPossibleTargets = new List<CombatEntity>();
        allPossibleTargets.AddRange(combatContext.enemyParty);
        allPossibleTargets.AddRange(combatContext.playerParty);
        currentTarget = allPossibleTargets.Where(combatEntity => combatEntity.entityConfig == enemyConfig).FirstOrDefault();
        if (currentTarget != null)
        {
            targetIndicator.gameObject.SetActive(true);
            var indicatorPosition = currentTarget.transform.position;
            indicatorPosition.y += currentTarget.transform.localScale.y * 2;
            targetIndicator.transform.position = indicatorPosition;
            turnMessage.text = $"{currentTarget.entityConfig.Name} is being targeted";
        }
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

    private void CleanCombatActions(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).GetComponent<CombatOptionPrefab>().optionButton.onClick.RemoveAllListeners();
            Destroy(panel.transform.GetChild(i).gameObject);
        }
        panel.gameObject.SetActive(false);
    }
    private void CleanCharacterStats(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            var child = panel.transform.GetChild(i).gameObject;
            if (child.tag == "Removable")
                Destroy(child);
        }
    }
    
}
