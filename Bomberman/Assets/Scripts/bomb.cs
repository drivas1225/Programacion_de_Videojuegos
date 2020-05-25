using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Player" && FindObjectOfType<PlayerMovement>().movSpeed < 10)
        {
            FindObjectOfType<PlayerMovement>().movSpeed += 1;
        }
        Destroy(gameObject);*/
    }
}
