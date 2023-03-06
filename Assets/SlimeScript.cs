using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlimeScript : EnemyScript
{
    // Start is called before the first frame update
    public AIPath aiPath;

    private Vector2 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private Vector2 PickRandomPoint ()
    {
        var point = _initialPosition;
        point.y += Random.Range((float)-0.3, (float)0.3);
        point.x += Random.Range((float)-0.3, (float)0.3);
        return point;
    }
    
    public override void Move()
    {
        //getting player's position
        aiPath.destination = player.transform.position;
        
        // check if player near aggroDistance, if yes, calculate the path and move
        if (aiPath.remainingDistance < aggroDistance)
        {
            aiPath.SearchPath();
        }
        //if no, find a position near spawn point and move
        else
        {
            if (aiPath.destination == player.transform.position || aiPath.reachedDestination)
            {
                aiPath.destination = PickRandomPoint();
            }
        }
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
