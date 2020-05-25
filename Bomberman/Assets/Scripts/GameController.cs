using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int lives = 5;
    public int currentBombs = 0;
    public int maxBombs = 1;
    public int currentEnemies = 5;
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
    }
}
