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

    //todo can i refactor ? I dont think that is necessary but to be thought about later
    /*
     * see slime in stardew valley. Wanted to make it like that.
     */
    private IEnumerator Attacks()
    {
        //stop the movement of the slime
        aiPath.canMove = false;
        aiPath.maxSpeed = 2f;
        aiPath.maxAcceleration = 10f;
        yield return new WaitForSeconds(2f);
        
        //destination <- collider of the player. Add some seconds buffer so the AI target is bad
        var offset = player.GetComponent<BoxCollider2D>().offset;
        aiPath.destination = player.transform.position + new Vector3(offset.x, offset.y, 0);
        yield return new WaitForSeconds(0.05f);
        
        //check if slime is dead. Y -> no need to search path. N -> move
        if (currentHealth <= 0) yield break;
        aiPath.canMove = true;
        aiPath.canSearch = false;
        _animator.SetTrigger("SlimeAttack");
        aiPath.SearchPath();
        yield return new WaitForSeconds(1f);
        
        //reset
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
        //courutotine because slime need time to move after attacking.
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
