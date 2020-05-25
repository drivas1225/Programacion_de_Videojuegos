using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
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
            if (FindObjectOfType<MapDestroyer>().radio < 5)
            {
                FindObjectOfType<MapDestroyer>().radio += 1;
            }
            FindObjectOfType<GameController>().score += 500;
            Destroy(gameObject);
        }
    }
}
