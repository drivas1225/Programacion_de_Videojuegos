using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movSpeed;

    public Rigidbody2D rb;
    public Animator animator;
    private AudioManager aManager;


    Vector2 movment;
    // Start is called before the first frame update
    void Start()
    {
        aManager = FindObjectOfType<AudioManager>();

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bomp" )
        {
            Debug.Log("chocoooo");
            other.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //input
        movment.x = Input.GetAxisRaw("Horizontal");
        movment.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movment.x);
        animator.SetFloat("Vertical", movment.y);
        animator.SetFloat("Speed", movment.sqrMagnitude);
        aManager.Play("Walk");
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movment * movSpeed * Time.deltaTime);
    }
}
