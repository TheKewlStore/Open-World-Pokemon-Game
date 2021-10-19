using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Vector2 direction;
    private Rigidbody2D rigidBody2D;
    public float movementSpeed = 10f;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void updateDirection(Vector2 newDirection) {
        Vector2 previousDirection = this.direction;
        this.direction = newDirection;
        updateAnimator(previousDirection);
    }
    
    void updateAnimator(Vector2 previousDirection) {
        if (direction == Vector2.up) {
            animator.Play("base.walking_north");
        } else if (direction == Vector2.left) {
            animator.Play("base.walking_west");
        } else if (direction == Vector2.down) {
            animator.Play("base.walking_south");
        } else if (direction == Vector2.right) {
            animator.Play("base.walking_east");
        } else if (direction == Vector2.zero) {
            if (previousDirection == Vector2.up) {
                animator.Play("base.idle_north");
            } else if (previousDirection == Vector2.left) {
                animator.Play("base.idle_west");
            } else if (previousDirection == Vector2.down) {
                animator.Play("base.idle_south");
            } else if (previousDirection == Vector2.right) {
                animator.Play("base.idle_east");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);

        updateDirection(movement.normalized);

        rigidBody2D.MovePosition(rigidBody2D.position + (movement * movementSpeed));
    }
}
