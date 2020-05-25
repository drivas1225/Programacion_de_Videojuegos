using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible")
        {
            if (FindObjectOfType<MapDestroyer>().radio < 5)
            {
                FindObjectOfType<MapDestroyer>().radio += 1;
            }
            FindObjectOfType<GameController>().score += 500;
            Destroy(gameObject);
        }
    }
}
