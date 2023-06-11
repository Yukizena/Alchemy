using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MovementTesting : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;


    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        if(moveDirection.x < 0)
        {
            GetComponent<Animator>().SetBool("Left",true);
            GetComponent<Animator>().SetBool("Right",false);
        }
        else if(moveDirection.x > 0)
        {
            GetComponent<Animator>().SetBool("Left", false);
            GetComponent<Animator>().SetBool("Right", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Left", false);
            GetComponent<Animator>().SetBool("Right", false);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
