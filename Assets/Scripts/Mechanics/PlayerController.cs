using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DialogUi _dialogUi;

    private Animator _animator;
    private Vector2 _direction;
    private Vector2 _boxSize;
    private Vector2 _facingDirection;
    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;

    public float movementSpeed = 10f;
    public Sprite sprite;
    public DialogUi DialogUi => _dialogUi;
    public IInteractable Interactable { get; set; }

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _boxSize = new Vector2(0.1f, 1f);
    }

    void updateDirection(Vector2 newDirection) {
        Vector2 previousDirection = this._direction;
        this._direction = newDirection;
        updateAnimator(previousDirection);

        if(!newDirection.Equals(Vector2.zero))
        {
            _facingDirection = newDirection;
        }
    }
    
    void updateAnimator(Vector2 previousDirection) {
        if (_direction == Vector2.up) {
            _animator.Play("base.walking_north");
        } else if (_direction == Vector2.left) {
            _animator.Play("base.walking_west");
        } else if (_direction == Vector2.down) {
            _animator.Play("base.walking_south");
        } else if (_direction == Vector2.right) {
            _animator.Play("base.walking_east");
        } else if (_direction == Vector2.zero) {
            if (previousDirection == Vector2.up) {
                _animator.Play("base.idle_north");
            } else if (previousDirection == Vector2.left) {
                _animator.Play("base.idle_west");
            } else if (previousDirection == Vector2.down) {
                _animator.Play("base.idle_south");
            } else if (previousDirection == Vector2.right) {
                _animator.Play("base.idle_east");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogUi.IsOpen)
        {
            return;
        }
        //Movement
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);

        updateDirection(movement.normalized);

        _rigidBody2D.MovePosition(_rigidBody2D.position + (movement * movementSpeed));

        //Interaction
        // Use GetButtonDown() instead: https://docs.unity3d.com/ScriptReference/Input.html
        // This will allow us more configuration of input using the Unity project settings.
        if(Input.GetButtonDown("Submit"))
        {
            Interactable?.Interact(this);
        }
    }
}
