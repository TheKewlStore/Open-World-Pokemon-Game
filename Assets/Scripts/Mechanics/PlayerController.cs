using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    private Vector2 direction;
    public float movementSpeed = 10f;

    void Start() {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
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
        float newXSpeed = Input.GetAxis("Horizontal");
        float newYSpeed = Input.GetAxis("Vertical");

        Vector2 nonNormalized = new Vector2(newXSpeed, newYSpeed);
        Vector2 newDirection = nonNormalized.normalized;

        if (newDirection != direction) {
            updateDirection(newDirection);
        }

        Vector2 translation = newDirection * movementSpeed * Time.deltaTime;
        transform.position += new Vector3(translation.x, translation.y, 0);
    }
}
