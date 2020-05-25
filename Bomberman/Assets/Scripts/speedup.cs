using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (FindObjectOfType<PlayerMovement>().movSpeed < 10)
            {
                FindObjectOfType<PlayerMovement>().movSpeed += 1;
            }
            FindObjectOfType<GameController>().score += 500;
            Destroy(gameObject);
        }
    }
}
