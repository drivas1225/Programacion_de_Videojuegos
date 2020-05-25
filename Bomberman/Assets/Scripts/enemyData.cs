using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyData : MonoBehaviour
{
    public int lives; 
    public int score; 
    void Start()
    {
        lives = 1;
        score = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameController>().score += score;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && FindObjectOfType<GameController>().lives >= 1)
        {
            FindObjectOfType<GameController>().die();
        }
    }
}
