using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    private static LightingManager lighting;

    private void Awake()
    {
        if (lighting == null)
        {
            lighting = this;
            GameObject.DontDestroyOnLoad(gameObject);

            return;
        }

        GameObject.Destroy(gameObject);
        return;
    }
}
