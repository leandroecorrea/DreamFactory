using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersView : MonoBehaviour
{
    [SerializeField] private GameObject characterCardPrefab;
    public Action<PlayerPartyMemberConfig> DelegateBehaviour;

    public void InitializeCardFor(PlayerPartyMemberConfig character)
    {
        var newPrefab = Instantiate(characterCardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newPrefab.transform.SetParent(transform, false);
        var card = newPrefab.GetComponent<CharacterCard>();
        card.UpdateCard(character);
        card.GetComponent<Button>().onClick.AddListener(() => DelegateBehaviour(character));
    }
}
