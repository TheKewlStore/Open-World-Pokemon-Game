using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    private Vector2 direction;
    public float movementSpeed = 10.0f;

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

    float getRotationAngle() {
        Vector2 basis = Vector2.down;
        if (direction == Vector2.up) {
            basis = Vector2.up;
        } else if (direction == Vector2.left) {
            basis = Vector2.left;
        } else if (direction == Vector2.down) {
            basis = Vector2.down;
        } else if (direction == Vector2.right) {
            basis = Vector2.right;
        } else if (direction == Vector2.zero) {
            return 0;
        }

        return Vector2.Angle(direction, basis);
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

    Vector2 calculateDirection(float newXSpeed, float newYSpeed) {
        Vector2 newDirection = direction;

        if (Math.Abs(newXSpeed) > Math.Abs(newYSpeed)) {
            if (newXSpeed > 0) {
                newDirection = Vector2.right;
            } else {
                newDirection = Vector2.left;
            }
        } else {
            if (newYSpeed > 0) {
                newDirection = Vector2.up;
            } else if (newYSpeed < 0) {
                newDirection = Vector2.down;
            }
        }

        if (newXSpeed == 0 && newYSpeed == 0) {
            newDirection = Vector2.zero;
        }

        return newDirection;
    }

    // Update is called once per frame
    void Update()
    {
        float newXSpeed = Input.GetAxis("Horizontal") * movementSpeed;
        float newYSpeed = Input.GetAxis("Vertical") * movementSpeed;
        // Vector2 newDirection = calculateDirection(newXSpeed, newYSpeed);
        Vector2 newDirection = new Vector2(newXSpeed, newYSpeed).normalized;

        if (newDirection != direction) {
            updateDirection(newDirection);
        }

        Vector2 translation = newDirection * Time.deltaTime;
        float angle = getRotationAngle();

        transform.position += new Vector3(translation.x, translation.y, 0);
        Debug.Log("Rotating about z axis " + angle.ToString() + " degrees");
        
        if (angle != 0) {
            transform.Rotate(0, 0, angle); 
        }
    }
}
