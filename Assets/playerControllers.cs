using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControllers : MonoBehaviour
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
    public MenuController menuController;
    public AnimationControl animationControl;

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        menuController = GetComponent<MenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("input pressed");
            if (menuController is null)
            {
                Debug.Log("null menu");
            }
            menuController.OpenMenu();
        }
        
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("input pressed");
            if (menuController is null)
            {
                Debug.Log("null menu");
            }
            menuController.CloseMenu();
        }
    }
    
    private void FixedUpdate()
    {
        ChangeDirection(movementInput);
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
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

    void OnFire()
    {
        animator.SetTrigger("swordTrigger");
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
