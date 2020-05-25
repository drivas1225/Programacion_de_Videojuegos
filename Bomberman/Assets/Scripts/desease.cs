using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desease : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<GameController>().die();
            Destroy(gameObject);
        }
    }
}
