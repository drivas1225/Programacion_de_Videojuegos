using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible")
        {
            if (FindObjectOfType<GameController>().maxBombs < 10)
            {
                FindObjectOfType<GameController>().maxBombs += 1;
            }
            FindObjectOfType<GameController>().score += 500;
            Destroy(gameObject);
        }
    }
}
