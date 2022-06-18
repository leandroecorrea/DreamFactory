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
    [SerializeField] private GameObject targetDiana;
    private CombatEntity currentTarget;
    private CombatContext combatContext;

    private void Awake()
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
            targetDiana.gameObject.SetActive(true);
            var dianaPosition = currentTarget.transform.position;
            dianaPosition.y += currentTarget.transform.localScale.y * 2;
            targetDiana.transform.position = dianaPosition;
            turnMessage.text = $"Enemy {currentTarget.entityConfig.Name} is being targeted";
        }
    }

    private void ShowTurnMessage()
    {
        turnMessage.text = "It's your turn!";
    }

    private void ShowOptionsForPlayer()
    {

        playerUI.SetActive(true);
        combatContext.currentTurnEntity.entityConfig.actions.ForEach(action =>
        {
            var optionListElement = Instantiate(combatOptionPrefab);

            if (optionListElement.TryGetComponent(out CombatOptionPrefab targetPrefab))
            {
                targetPrefab.optionName.text = action.actionName;
                targetPrefab.optionButton.onClick.AddListener(delegate {
                    combatManager.Perform(action, currentTarget);
                });
            }
            optionListElement.transform.SetParent(optionsPanel.transform, false);
        });
    }

}
