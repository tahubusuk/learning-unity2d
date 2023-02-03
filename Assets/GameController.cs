using System;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public GameState CurrentState;
    public FreeRoamState FreeRoamState { get; private set; }
    public BattleState BattleState { get; private set; }
    public PauseMenuState PauseMenuState;

    private void Start()
    {
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("key pressed");
            if (PauseMenuState is null)
            {
                Debug.Log("menu is null");
            }
            PauseMenuState.ChangeState(this);
        }
    }
    
}
