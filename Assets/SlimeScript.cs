using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 10;
    public Collider2D slimeCollider;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            _animator.SetTrigger("Destroy");
        }
    }

    void Defeated()
    {
        Destroy(gameObject);
    }
}
