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
    
    public override void Attack()
    {
        Debug.Log("is the attack here?");
        slimeHitbox.enabled = true;
        //todo implement attack -> casting collision to player karna slime lompat
        _animator.SetTrigger("SlimeAttack");
    }

    public void SlimeStopAttack()
    {
        slimeHitbox.enabled = false;
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
                player1.TakeDamage(5);
            }
        }
    }


}
