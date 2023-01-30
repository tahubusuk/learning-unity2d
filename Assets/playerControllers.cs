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
    public AnimationControl animationControl;

    private enum _playerState
    {
        PLAYER_ATTACK_RIGHT,
         PLAYER_ATTACK_LEFT,
         PLAYER_ATTACK_UP,
         PLAYER_ATTACK_DOWN,
         
         PLAYER_IDLE_UP,
         PLAYER_IDLE_DOWN,
         PLAYER_IDLE_RIGHT,
         PLAYER_IDLE_LEFT,
         
         PLAYER_WALK_UP,
         PLAYER_WALK_RIGHT,
         PLAYER_WALK_LEFT,
         PLAYER_WALK_DOWN,
         
         PLAYER_DEATH
        
    }

    private _playerState _currentState;
    
    private Dictionary<_playerState, string> _eventDict = new Dictionary<_playerState, string>()
    {
        { _playerState.PLAYER_IDLE_DOWN, "player_idle_front" },
        { _playerState.PLAYER_IDLE_UP, "player_idle_behind" },
        { _playerState.PLAYER_IDLE_RIGHT, "player_idle_right" },
        { _playerState.PLAYER_IDLE_LEFT, "player_idle_right" },

        { _playerState.PLAYER_ATTACK_DOWN, "player_attack_front" },
        { _playerState.PLAYER_ATTACK_UP, "player_attack_behind" },
        { _playerState.PLAYER_ATTACK_RIGHT, "player_attack_right" },
        { _playerState.PLAYER_ATTACK_LEFT, "player_attack_right" },

        { _playerState.PLAYER_WALK_DOWN, "player_walk_front" },
        { _playerState.PLAYER_WALK_UP, "player_walk_behind" },
        { _playerState.PLAYER_WALK_RIGHT, "player_walk_right" },
        { _playerState.PLAYER_WALK_LEFT, "player_walk_right" },

        { _playerState.PLAYER_DEATH, "player_death" }

    };



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
            // UpdateStateMove(success);
        }
        else
        {
            animator.SetBool("isMoving", false);
            // UpdateStateMove(false);
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

    private void UpdateStateMove(bool isMoving)
    {
        switch (facedDirection)
        {
            case 0:
                _currentState = isMoving ? _playerState.PLAYER_WALK_UP : _playerState.PLAYER_IDLE_UP; 
                
                // animationControl.PlayEvent(_eventDict["playerAttackUp"]);
                break;
            case 1:
                _currentState = isMoving ? _playerState.PLAYER_WALK_RIGHT : _playerState.PLAYER_IDLE_RIGHT;
                // animationControl.PlayEvent(_eventDict["playerAttackRight"]);
                break;
            case 2:
                _currentState = isMoving ? _playerState.PLAYER_WALK_DOWN : _playerState.PLAYER_IDLE_DOWN;
                // animationControl.PlayEvent(_eventDict["playerAttackDown"]);
                break;
            case 3:
                _currentState = isMoving ? _playerState.PLAYER_WALK_LEFT : _playerState.PLAYER_IDLE_LEFT;
                // animationControl.PlayEvent(_eventDict["playerAttackLeft"]);
                break;
                
        }
        //coba ditaruh disini, tp sepertinya g deh
        animationControl.PlayEvent(_eventDict[_currentState], 0);
    }

    private void UpdateStateAttack(bool isAttack)
    {
        
        switch (facedDirection)
        {
            case 0:
                _currentState = isAttack ? _playerState.PLAYER_ATTACK_UP : _playerState.PLAYER_IDLE_UP; 
                
                // animationControl.PlayEvent(_eventDict["playerAttackUp"]);
                break;
            case 1:
                _currentState = isAttack ? _playerState.PLAYER_ATTACK_RIGHT : _playerState.PLAYER_IDLE_RIGHT;
                // animationControl.PlayEvent(_eventDict["playerAttackRight"]);
                break;
            case 2:
                _currentState = isAttack ? _playerState.PLAYER_ATTACK_DOWN : _playerState.PLAYER_IDLE_DOWN;
                // animationControl.PlayEvent(_eventDict["playerAttackDown"]);
                break;
            case 3:
                _currentState = isAttack ? _playerState.PLAYER_ATTACK_LEFT : _playerState.PLAYER_IDLE_LEFT;
                // animationControl.PlayEvent(_eventDict["playerAttackLeft"]);
                break;
                
        }
        //coba ditaruh disini, tp sepertinya g deh
        animationControl.PlayEvent(_eventDict[_currentState], 1);
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
        // UpdateStateAttack(true);
        Debug.Log("on fire clicked");
        Debug.Log(facedDirection.ToString());
        // attackOffset = ani
        // Invoke("StopFire", );
        // UpdateStateAttack(false);
    }

    void StopFire()
    {
        
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
