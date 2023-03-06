using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class EnemyScript: MonoBehaviour
    {
        public int health;
        public Rigidbody2D rb;
        public Collider2D enemyCollider;
        public ContactFilter2D movementFilter;
        public List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
        public float collisionOffset = 0.05f;
        public Animator _animator;
        public float speed;
        public float distance;
        public GameObject player;
        public float aggroDistance;

        public virtual void HandleUpdate()
        {
            
        }
        
        public virtual void Move()
        {
            
        }

        public virtual void TakeDamage(int damage)
        {
            
        }

        public virtual void Attack()
        {
            
        }

        public virtual void Defeated()
        {
            
        }
        
    }

}