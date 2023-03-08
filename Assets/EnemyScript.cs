using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public abstract class EnemyScript: MonoBehaviour
    {
        public int initialHealth;
        public int currentHealth;
        public Rigidbody2D rb;
        public Collider2D enemyCollider;
        public Animator _animator;
        public AIPath aiPath;
        public float speed;
        public float distance;
        public GameObject player;
        public float aggroDistance;
        public Vector2 _initialPosition;
        public float maxDistance = 3;

        public void Start()
        {
            currentHealth = initialHealth;
            _initialPosition = transform.position;
        }

        public void Update()
        {
            if (currentHealth <= 0) return;
            Move();
        }
        
        public virtual void HandleUpdate()
        {
            
        }
        
        //picking random point near the spawn point
        private Vector2 PickRandomPoint ()
        {
            var point = _initialPosition;
            point.y += Random.Range((float)-0.3, (float)0.3);
            point.x += Random.Range((float)-0.3, (float)0.3);
            return point;
        }
        
        //todo check the State first then move. Make enemy spawner first. 
        protected virtual void Move()
        {
            var playerPosition = player.transform.position;
            var enemyPosition = transform.position;
            distance = Vector2.Distance(playerPosition, enemyPosition);
            
            //check if the position is too far
            if (distance > maxDistance) return;
            
            if (aiPath.pathPending) return;
            if (aiPath.reachedEndOfPath || !aiPath.hasPath)
            {
                //todo make some waiting time (tried with corroutine but the result is not good)
                _animator.SetBool("isMoving", false);
                aiPath.destination = PickRandomPoint();
                aiPath.SearchPath();
                _animator.SetBool("isMoving", true);
            }

            // check if player near aggroDistance, if yes, calculate the path and move
            if (!(distance < aggroDistance)) return;
            aiPath.destination = playerPosition;
            aiPath.SearchPath();

            if (!(distance < 0.3f)) return;
            Attack();
        }

        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                _animator.SetTrigger("Destroy");
            }
            
        }

        public virtual void Attack()
        {
            
        }

        public virtual void Defeated()
        {
            
            _animator.SetTrigger("WaitToRespawn");
            StartCoroutine(Respawn());
        }
        
        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(5f);
            Instantiate(gameObject, _initialPosition, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

}