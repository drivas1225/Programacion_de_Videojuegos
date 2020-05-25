using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public int lives = 5;
    public int currentBombs = 0;
    public int maxBombs = 1;
    public int currentEnemies = 5;
    public int score = 0;
    public Tilemap tilemap;
    public float invincible = 5f;
  
    public bool startBlinking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if(currentEnemies == 0)
        {
            Debug.Log("Ya ganaste. Que quieres? Te aplaudo?");
        }
        invincible -= Time.deltaTime;
        SpriteBlinkingEffect();
        if (invincible <= 0f)
        {
            FindObjectOfType<PlayerMovement>().tag = "Player";
            startBlinking = false;
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void die()
    {
        lives -= 1;
        maxBombs = 1;
        FindObjectOfType<MapDestroyer>().radio = 2;
        Vector3 position = tilemap.GetCellCenterWorld(new Vector3Int(-8, 3, 0));
        FindObjectOfType<PlayerMovement>().gameObject.transform.position = position;
        FindObjectOfType<PlayerMovement>().tag = "Invincible";
        invincible = 5;
    }

    private void SpriteBlinkingEffect()
    {
        if (invincible >= 0.0f)
        {
            if (FindObjectOfType<PlayerMovement>().gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                FindObjectOfType<PlayerMovement>().gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }
    }
