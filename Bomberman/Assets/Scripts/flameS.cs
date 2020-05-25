using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameS : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible")
        {
            FindObjectOfType<MapDestroyer>().radio = 5;
            FindObjectOfType<GameController>().score += 1000;
            Destroy(gameObject);
        }
    }
}
