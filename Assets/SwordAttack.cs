using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage = 3;
    public Collider2D swordCollider;

    private Vector2 attackOffset;
    // Start is called before the first frame update
    void Start()
    {
        attackOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackLeft()
    {
        transform.localPosition = new Vector3((float)0.218, (float)-0.179, (float)0.69);
        transform.localRotation = Quaternion.Euler(240, 320, 270);
        transform.localScale = new Vector3((float)1.5, 2, 1);
    }
    private void AttackRight()
    {
        transform.localPosition = new Vector3((float)-0.271, (float)-0.17, (float)0.053);
        transform.localRotation = Quaternion.Euler(240, 160, 270);
        transform.localScale = new Vector3((float)1.5, 2, 1);
    }
    private void AttackDown()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }
    private void AttackUp()
    {
        transform.localPosition = new Vector3((float)-0.0314, (float)-0.2053, 0);
        transform.localRotation = Quaternion.Euler(0, 180, 180);
        transform.localScale = new Vector3(1, 1, 1);
    }
    public void Attack(int faceDirection)
    {
        swordCollider.enabled = true;
        switch (faceDirection)
        {
            case 0:
                AttackDown();
                break;
            case 1:
                AttackRight();
                break;
            case 2:
                AttackUp();
                break;
            case 3:
                AttackLeft();
                break;
        }
    }

    public void StopAttack()
    {
        Debug.Log("sowrd atack disabled");
        swordCollider.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("player sowrd triger enemy");
            
            SlimeScript slime = other.GetComponent<SlimeScript>();
            if (slime != null)
            {
                Debug.Log(slime.currentHealth.ToString());
                slime.TakeDamage(damage);
            }
        }
    }
    
}
