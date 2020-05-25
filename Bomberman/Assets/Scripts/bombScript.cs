using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    public float countdown = 2f;

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            Destroy(gameObject);
            FindObjectOfType<MapDestroyer>().Explode(transform.position);
            FindObjectOfType<GameController>().currentBombs -= 1;
        }
    }
}
