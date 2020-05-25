using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameS : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" )
        {
            FindObjectOfType<MapDestroyer>().radio = 5;
        }
        Destroy(gameObject);
    }
}
