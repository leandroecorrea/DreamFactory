using UnityEngine;

public class Character
{
    [SerializeField] public string Name { get; set; }
    [SerializeField] public int CurrentMP { get; set; }
    [SerializeField] public int MaxMP { get; set; }
    [SerializeField] public int CurrentHP { get; set; }
    [SerializeField] public int MaxHP { get; set; }
    [SerializeField] public int Level { get; set; }
}