
using System;
using UnityEngine;

public class Emerald : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehavior player))
        {
            player.EmeraldCollected();

            Destroy(gameObject);
        }
    }
}
