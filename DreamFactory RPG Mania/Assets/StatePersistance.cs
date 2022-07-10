using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePersistance : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private static Vector3 playerPosition;
    // Start is called before the first frame update    

    public void StorePlayerPosition()
    {
        playerPosition = player.transform.position;
    }

    private void OnEnable()
    {
        if(playerPosition != null)
            player.transform.position = playerPosition;
    }
}
