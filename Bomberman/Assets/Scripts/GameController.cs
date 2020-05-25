using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int lives = 5;
    public int currentBombs = 0;
    public int maxBombs = 1;
    public int currentEnemies = 5;
    public int score = 0;
    public Tilemap tilemap;
    public float invincible = 5f;
    public Text scoreText;
    public Text livesText;
    public Text bombText;
    public Text flameText;
    public Text speedText;
    public GameObject portalPrefab;
    public GameObject instantiatedPortal;
    private AudioManager aManager;

    public bool startBlinking = false;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text ="0000";
        aManager = FindObjectOfType<AudioManager>();
        aManager.Play("Level Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (aManager.finishedPlaying("Level Start"))
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Nivel2":
                    aManager.Play("Theme Music");
                    break;
                default:
                    aManager.Play("Demo");
                    break;
            }

        }
        if(lives <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
    private void FixedUpdate()
    {
        if(currentEnemies <= 0 && !instantiatedPortal)
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(new Vector3Int(0, 0, 0));
            instantiatedPortal = Instantiate(portalPrefab, worldPos, Quaternion.identity);
        }
        invincible -= Time.deltaTime;
        SpriteBlinkingEffect();
        if (invincible <= 0f)
        {
            FindObjectOfType<PlayerMovement>().tag = "Player";
            startBlinking = false;
            FindObjectOfType<PlayerMovement>().gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        livesText.text = lives.ToString();
        bombText.text = maxBombs.ToString();
        scoreText.text = score.ToString();
        speedText.text = FindObjectOfType<PlayerMovement>().movSpeed.ToString();
        flameText.text = FindObjectOfType<MapDestroyer>().radio.ToString();
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
        aManager.Play("Death");
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
