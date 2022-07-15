using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAudio : MonoBehaviour
{
    [SerializeField] Cart cart;
    private void OnEnable()
    {
        cart.onBuy += PlayBuySound;
    }

    // Update is called once per frame
    void PlayBuySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
