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
    
    //picking random point near the spawn point
    private Vector2 PickRandomPoint ()
    {
        var point = _initialPosition;
        point.y += Random.Range((float)-0.3, (float)0.3);
        point.x += Random.Range((float)-0.3, (float)0.3);
        return point;
    }


    public override void Move()
    {
        if (aiPath.pathPending) return;
        if (aiPath.reachedEndOfPath || !aiPath.hasPath)
        {
            aiPath.destination = PickRandomPoint();
            aiPath.SearchPath();
        }

        // check if player near aggroDistance, if yes, calculate the path and move
        var playerPosition = player.transform.position;
        var enemyPosition = transform.position;
        distance = Vector2.Distance(playerPosition, enemyPosition);
        if (!(distance < aggroDistance)) return;
        aiPath.destination = playerPosition;
        aiPath.SearchPath();
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
