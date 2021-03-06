﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    public float countdown = 2f;
    private AudioManager aManager;

    private void Start()
    {
        aManager = FindObjectOfType<AudioManager>();

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible")
        {
            FindObjectOfType<BoxCollider2D>().enabled = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            aManager.Play("Explosion");
            Destroy(gameObject);
            FindObjectOfType<MapDestroyer>().Explode(transform.position);
            FindObjectOfType<GameController>().currentBombs -= 1;
        }
    }
}
