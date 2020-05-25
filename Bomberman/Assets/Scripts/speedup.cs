using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedup : MonoBehaviour
{
    private AudioManager aManager;

    private void Start()
    {
        aManager = FindObjectOfType<AudioManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible")
        {
            aManager.Play("Pick Up");
            if (FindObjectOfType<PlayerMovement>().movSpeed < 10)
            {
                FindObjectOfType<PlayerMovement>().movSpeed += 1;
            }
            FindObjectOfType<GameController>().score += 500;
            Destroy(gameObject);
        }
    }
}
