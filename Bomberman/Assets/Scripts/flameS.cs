using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameS : MonoBehaviour
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
            FindObjectOfType<MapDestroyer>().radio = 5;
            FindObjectOfType<GameController>().score += 1000;
            Destroy(gameObject);
        }
    }
}
