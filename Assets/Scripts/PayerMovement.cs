using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerMovement : MonoBehaviour
{
    //Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private Vector3 moveDirection, velocity;

    [SerializeField] private bool isGrounded;

    [SerializeField] private float groundCheckDistance;

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float gravity;
    //Referencias
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveZ);
        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //Camine
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.A))
            {
                //Corra
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                //Parado
                Idle();
            }
        }

        moveDirection *= moveSpeed;

        _controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        
    }
    
    private void Walk()
    {
        moveSpeed = walkSpeed;
    }
    
    private void Run()
    {
        moveSpeed = runSpeed;
    }
}
