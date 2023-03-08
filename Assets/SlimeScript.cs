using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Pathfinding;
using TMPro.Examples;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlimeScript : EnemyScript
{

    public Collider2D slimeHitbox;

    // Start is called before the first frame update

    // public void Start()
    // {
    // }

    // Update is called once per frame
    // public void Update()
    // {
    // }

    public override void ResetEnemy()
    {
        aiPath.canMove = true;
        aiPath.canSearch = true;
        slimeHitbox.enabled = false;
        isAttacking = false;
        aiPath.maxAcceleration = default;
        aiPath.maxSpeed = 0.1f;
        
    }
    
    public override void Attack()
    {
        isAttacking = true;
        slimeHitbox.enabled = true;
        _animator.SetBool("isMoving", false);
        StartCoroutine(Attacks());
    }

    private IEnumerator Attacks()
    {
        aiPath.canMove = false;
        aiPath.maxSpeed = 2f;
        aiPath.maxAcceleration = 10f;
        yield return new WaitForSeconds(2f);
        Vector2 offset = player.GetComponent<BoxCollider2D>().offset;
        Vector3 destination = player.transform.position + new Vector3(offset.x, offset.y, 0);
        aiPath.destination = destination;
        yield return new WaitForSeconds(0.05f);
        if (currentHealth <= 0) yield break;
        aiPath.canMove = true;
        aiPath.canSearch = false;
        _animator.SetTrigger("SlimeAttack");
        aiPath.SearchPath();
        yield return new WaitForSeconds(1f);
        aiPath.maxAcceleration = default;
        aiPath.maxSpeed = 0.1f;
        aiPath.canSearch = true;
    }

    public void SlimeStopAttack()
    {
        slimeHitbox.enabled = false;
        StartCoroutine(AttackFalse());
    }

    private IEnumerator AttackFalse()
    {
        yield return new WaitForSeconds(1.25f);
        base.isAttacking = false;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("trigger entrer");
        if (other.CompareTag("Player"))
        {
            var player1 = other.GetComponent<PlayerControllers>();
            Debug.Log(player1);
            if (player1 != null)
            {
                player1.TakeDamage(1);
            }
        }
        else
        {
            Debug.Log(other.ToString());
            Debug.Log("wrong tag ??");
        }
    }


}
