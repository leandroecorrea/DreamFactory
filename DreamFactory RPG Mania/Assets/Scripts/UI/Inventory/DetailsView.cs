using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsView : MonoBehaviour
{
    [SerializeField] private GameObject detailsPrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private TMP_Text abilityDescription;
    public void InitializeAbilities(PlayerPartyMemberConfig playerParty)
    {
        foreach (var ability in playerParty.PartyMemberCombatConfig.CombatEntityConfig.actions)
        {
            if (ability.combatActionType == CombatActionType.SPELL)
            {
                CreateDetail(ability.actionName, ability.description);
            }
        }
    }

    private DetailView CreateDetail(string name, string description)
    {
        var newPrefab = Instantiate(detailsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newPrefab.transform.SetParent(content.transform, false);
        var detailView = newPrefab.GetComponent<DetailView>();
        detailView.SetName(name);
        newPrefab.GetComponent<Button>().onClick.AddListener(() => SetAbilityText(description));
        return detailView;
    }

    public void InitializeItems(List<Item> items)
    {
        foreach(var item in items)
        {
            var detail = CreateDetail(item.data.itemName, item.data.description);
            detail.SetAmount(item.amount.ToString());
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            var item = content.transform.GetChild(i);
            item.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(item.gameObject);
        }
        abilityDescription.text = "";
    }

    private void SetAbilityText(string description)
        => abilityDescription.text = description;
}
