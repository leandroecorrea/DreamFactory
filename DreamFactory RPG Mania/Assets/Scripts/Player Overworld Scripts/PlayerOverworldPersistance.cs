using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldPersistance : MonoBehaviour
{
    private static PlayerOverworldPersistance persistance;

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
}
