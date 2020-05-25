using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalScript : MonoBehaviour
{
    private AudioManager aManager;

    private void Start()
    {
        aManager = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        if (aManager.finishedPlaying("Level Clear"))
        {
            SceneManager.LoadScene(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Invincible")
        {
            aManager.StopPlaying("Demo");
            FindObjectOfType<PlayerMovement>().enabled = false;
            aManager.Play("Level Clear");
        }
    }
}
