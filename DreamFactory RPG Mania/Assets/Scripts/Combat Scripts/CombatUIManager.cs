using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] private GameObject backButton;

    [Header("Prefabs")]
    [SerializeField] private GameObject combatOptionPrefab;
    [SerializeField] private GameObject targetOptionPrefab;
    [SerializeField] private GameObject characterStatsPrefab;
    [SerializeField] private GameObject actionFeedbackPrefab;
    [SerializeField] private GameObject effectsIconPrefab;
    [SerializeField] private Camera view;
    private CombatEntity currentTarget;
    private CombatContext combatContext;
    private CombatActionConfig currentAction;    

    private void OnEnable()
    {
        combatManager.onCombatTurnStart += handleCombatTurnStart;        
        CombatEventSystem.instance.onCombatEntityDamaged += HandleCombatEntityDamageFeedback;
    }

    private void HandleCombatEntityDamageFeedback(object sender, CombatEntityDamagedArgs e)
    {
        CombatEntity damagedUnit = (CombatEntity)sender;        
        var position = view.WorldToScreenPoint(damagedUnit.transform.position);
        position.x = position.x - view.pixelWidth / 2;
        position.y = position.y - view.pixelHeight / 2;
        Debug.Log(position);
        GameObject feedback = Instantiate(actionFeedbackPrefab, position, Quaternion.identity);
        feedback.GetComponent<TMP_Text>().text = $"{e.damageTaken}";
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

                targetPrefab.optionButton.interactable = (combatContext.currentTurnEntity.CurrentMP >= action.requireMana);
                if (targetPrefab.optionButton.interactable)
                {
                    targetPrefab.optionButton.onClick.AddListener(delegate
                    {
                        OnActionChosen(action);
                    });
                } 
            }
            optionListElement.transform.SetParent(actionsPanel.transform, false);
        });
    }

    private void OnActionChosen(CombatActionConfig action)
    {
        //CleanCombatOptions(actionsPanel);
        currentAction = action;
        backButton.SetActive(true);
        //Don't like this switch
        switch (action.targetType)
        {
            case TargetType.ENEMIES:
                CleanCombatActions(actionsPanel);
                CleanTargets(targetsPanel);
                ShowEnemiesList();
                
                break;
            case TargetType.ALLIES:
                CleanCombatActions(actionsPanel);
                CleanTargets(targetsPanel);
                ShowAlliesList();                
                break;            
            default:
                break;
        }
    }

    public void BackToPreviousView()
    {       
        CleanTargets(targetsPanel);
        CleanCombatActions(actionsPanel);
        ShowActions();
        ShowTurnMessage();
        targetIndicator.SetActive(false);
        backButton.SetActive(false);
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
            var characterStats = statsPrefab.GetComponent<CharacterStatsPrefab>();
            characterStats.characterName.text = character.entityConfig.Name;
            characterStats.HP.text = $"{character.CurrentHP}/{character.entityConfig.baseHP}";
            characterStats.MP.text = $"{character.CurrentMP}/{character.entityConfig.baseMP}";
            statsPrefab.transform.SetParent(charactersPanel.transform, false);            
            int maxIterations = Mathf.Min(character.EffectsCaused.Count, characterStats.effectIcons.Length);
            for (int i = 0; i < maxIterations; i++)
            {
                characterStats.effectIcons[i].enabled = true;
                characterStats.effectIcons[i].sprite = character.EffectsCaused[i].combatEffectConfig.displayIcon;
            }
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
        SetCurrentDefaultEnemyTarget();
    }

    private void SetCurrentDefaultEnemyTarget()
    {
        currentTarget = combatContext.enemyParty.FirstOrDefault();
        ShowTargetIndicator();
    }

    public void SwitchTarget(CombatEntityConfig enemyConfig)
    {
        var allPossibleTargets = new List<CombatEntity>();
        allPossibleTargets.AddRange(combatContext.enemyParty);
        allPossibleTargets.AddRange(combatContext.playerParty);
        currentTarget = allPossibleTargets.Where(combatEntity => combatEntity.entityConfig == enemyConfig).FirstOrDefault();
        ShowTargetIndicator();
    }

    private void ShowTargetIndicator()
    {
        if (currentTarget != null)
        {
            targetIndicator.gameObject.SetActive(true);
            
            var indicatorPosition = currentTarget.transform.position;
            indicatorPosition.y += currentTarget.transform.localScale.y * 2;
            targetIndicator.transform.position = indicatorPosition;
            turnMessage.text = $"{currentTarget.entityConfig.Name} is being targeted";
        }
    }
    private void ShowEffectsApplied(CombatEntity controllableEntity)
    {
        Debug.Log(controllableEntity is PlayerControllableEntity);
        var targetRow = charactersPanel.GetComponentsInChildren<CharacterStatsPrefab>().Where(x => x.characterName.text == controllableEntity.entityConfig.Name).FirstOrDefault();
        int maxIterations = Mathf.Min(controllableEntity.EffectsCaused.Count, targetRow.effectIcons.Length);
        for (int i = 0; i < maxIterations; i++)
        {
            targetRow.effectIcons[i].enabled = true;
        }
    }


    private void ReceiveInputFor(CombatActionConfig action)
    {        
        var currentCharacter = combatContext.currentTurnEntity;
        if (currentTarget != null)
        {
            HideUIForPlayer();
            currentCharacter.PerformAction(action, currentTarget);
            currentTarget = null;
            backButton.gameObject.SetActive(false);
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
