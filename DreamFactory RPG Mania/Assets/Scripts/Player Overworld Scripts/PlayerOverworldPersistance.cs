using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldPersistance : MonoBehaviour
{
    public static PlayerOverworldPersistance persistance;
    private Vector3 previousPosition;

    private void Awake()
    {
        if (persistance == null)
        {
            persistance = this;
            GameObject.DontDestroyOnLoad(gameObject);

            return;
        }

        GameObject.Destroy(gameObject);
        return;
    }

    public void StorePosition()
    {
        previousPosition = transform.position;
        gameObject.SetActive(false);
    }

    public void ResetPosition()
    {
        gameObject.SetActive(true);
        transform.position = previousPosition;        
        GetComponent<PlayerMovement>().StopMoving();
    }

}
