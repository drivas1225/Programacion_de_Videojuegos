using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinasourMovement : MonoBehaviour
{
    public float movSpeed = 2;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movment;
    // Start is called before the first frame update
    void Start()
    {

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
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movment * movSpeed * Time.deltaTime);
    }
}
