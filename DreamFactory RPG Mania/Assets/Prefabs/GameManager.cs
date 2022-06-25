using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Powers {Shadowshift,RangedAttack,Wings,ShadowDash,Invisable};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPaused;
    public GameObject Player;
   // Start is called before the first frame update
   void Start()
    {
        
        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
