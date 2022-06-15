using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersView : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public void InitializeCardFor(Character c)
    {

        var newPrefab = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        newPrefab.transform.SetParent(transform, false);
        var card = newPrefab.GetComponent<CharacterCard>();
        card.UpdateCard(c);
    }
}
