using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private string moneyPickupInteractionId;
    [SerializeField] private int moneyPickupAmount;

    [Header("Component Refs")]
    [SerializeField] private DialogueManager dialogueManager;

    private void OnDestroy()
    {
        dialogueManager.onConversationComplete -= HandlePickupDialogueComplete;
    }

    private void Awake()
    {
        List<string> completedInteractions = PlayerProgression.GetPlayerData<List<string>>(SaveKeys.COMPLETED_INTERACTIONS);
        if (completedInteractions.Contains(moneyPickupInteractionId))
        {
            GameObject.Destroy(gameObject);
            return;
        }

        dialogueManager.onConversationComplete += HandlePickupDialogueComplete;
    }

    private void HandlePickupDialogueComplete(object sender, ConversationCompletedArgs e)
    {
        PickupMoney();
    }

    public void PickupMoney()
    {
        PlayerProgression.UpdatePlayerData<int>(SaveKeys.CURRENT_CURRENCY, PlayerProgression.GetPlayerData<int>(SaveKeys.CURRENT_CURRENCY) + moneyPickupAmount);

        List<string> completedInteractions = PlayerProgression.GetPlayerData<List<string>>(SaveKeys.COMPLETED_INTERACTIONS);
        completedInteractions.Add(moneyPickupInteractionId);

        PlayerProgression.UpdatePlayerData<List<string>>(SaveKeys.COMPLETED_INTERACTIONS, completedInteractions);
        GameObject.Destroy(gameObject);
    }
}
