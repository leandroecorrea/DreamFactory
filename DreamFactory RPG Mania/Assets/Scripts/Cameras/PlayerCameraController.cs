using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : CameraController
{
    public static PlayerCameraController instance;

    protected override void Awake()
    {
        if (instance == null)
        {
            Transform playerTransform = GameObject.FindObjectOfType<PlayerMovement>().transform;
            if (playerTransform == null)
            {
                GameObject.Destroy(gameObject);
                return;
            }

            instance = this;
            _defaultFollowTarget = playerTransform;

            GameObject.DontDestroyOnLoad(gameObject);

            base.Awake();
        }
        else
        {
            GameObject.Destroy(gameObject);
            return;
        }
    }
}
