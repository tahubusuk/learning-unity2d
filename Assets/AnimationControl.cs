
using System;
using System.Linq;
using UnityEngine;

public class AnimationControl: MonoBehaviour
{
    
    public Animator _animator;
    private string _currentState;
    private bool _isAttack;
    

    void Start()
    {
        _animator = GetComponent<Animator>();
        _isAttack = false;
    }
    
    public void PlayEvent(string events, int layer)
    {
        // if (_currentState == events || _isAttack) return;
        
        if (_currentState == events) return;
        Debug.Log(events);
        if (events.Contains("attack"))
        {
            Debug.Log("attack command");
            // _isAttack = true;
        }
        _animator.Play(events, layer);
        float attackOffset = _animator.GetCurrentAnimatorStateInfo(0).length;
        // Invoke(nameof(AttackComplete), 0f);
        _currentState = events;
    }

    void AttackComplete()
    {
        _isAttack = false;
    }
    
    
    
}