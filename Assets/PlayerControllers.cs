using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllers : MonoBehaviour
{
    Vector2 movementInput;

    private Rigidbody2D rb;

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public int facedDirection = 0;
    public SwordAttack swordAttack;

    private SpriteRenderer _spriteRenderer;
    
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void HandleUpdate()
    {
        ChangeDirection(movementInput);
        if (movementInput != Vector2.zero)
        {
            var success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (movementInput.x > 0 || facedDirection != 3)
        {
            _spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0 || facedDirection == 3)
        {
            _spriteRenderer.flipX = true;
        }

        // == OnFire, problem is we have states and OnFire does not care abt that. Will try to find another way
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("swordTrigger");
        }
    }

    

    private void ChangeDirection(Vector2 direction)
    {
        if (direction is { x: > 0, y: 0 })
        {
            facedDirection = 1;
        } else if (direction is { x: < 0, y: 0 })
        {
            facedDirection = 3;
        } else if (direction is { x: 0, y: > 0 })
        {
            facedDirection = 2;
        } else if (direction is { x: 0, y: < 0 })
        {
            facedDirection = 0;
        }
        
        //dibuat khusus karena blm punya animasi left
        int fd = facedDirection == 3 ? 1 : facedDirection;
        animator.SetInteger("faceDirection", fd);

    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);
       
        if (count == 0)
        {
            rb.MovePosition(rb.position + Time.fixedDeltaTime * moveSpeed * direction);
            return true;
        }
        
        return false;
    }
    

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    
    void StartSwordAttack()
    {
        
        swordAttack.Attack(facedDirection);
    }

    void EndSwordAttack()
    {
        swordAttack.StopAttack();
    }
}
