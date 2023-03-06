using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SlimeScript : EnemyScript
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // Move();
    }

    public override void Move()
    {
        //get the player position and enemy position
        Vector2 playerPosition = player.transform.position;
        Vector2 enemyPosition = this.transform.position;
        
        //calculating distance and the direction
        distance = Vector2.Distance(enemyPosition, playerPosition);
        
        //this is for the rotation later
        var positionDifference = playerPosition - enemyPosition;
        Debug.Log(positionDifference.ToString());
        var direction = new Vector2
        {
            x = positionDifference.x switch
            {
                > 0 => (float)0.5,
                < 0 => (float)-0.5,
                _ => 0
            },
            y = positionDifference.y switch
            {
                > 0 => (float)0.5,
                < 0 => (float)-0.5,
                _ => 0
            }
        };
        Debug.Log("direction");
        Debug.Log(direction.ToString());

        //moving and rotating
        // if (distance < aggroDistance)
        // {
        // var direction =
        //     Vector2.MoveTowards(enemyPosition, playerPosition, speed * Time.deltaTime);
        // }
        var success = TryMove(direction);
        if (!success)
        {
            success = TryMove(new Vector2(direction.x, 0));
            if (!success)
            {
                success = TryMove(new Vector2(0, direction.y));
            }
        }
        
        
        //todo casting collision
        //todo walking when there's no player
        //todo get back to the initial place
    }

    private bool TryMove(Vector2 direction)
    {
        
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            speed * Time.fixedDeltaTime + collisionOffset);
       
        if (count == 0)
        {
            rb.MovePosition(rb.position + Time.fixedDeltaTime * speed * direction);
            return true;
        }

        return false;
    }
    
    public override void Attack()
    {
        //todo implement attack -> casting collision to player karna slime lompat
        base.Attack();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            _animator.SetTrigger("Destroy");
        }
    }

    public override void Defeated()
    {
        Destroy(gameObject);
    }
}
