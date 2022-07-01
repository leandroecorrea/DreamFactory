using UnityEngine;


[CreateAssetMenu(fileName = "New Character Config", menuName = "Player Party/Configuration/New Character config")]
public class CharacterDetails : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public Sprite portrait;
}