using System;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] MenuController menuController;
    [SerializeField] PlayerControllers playerControllers;
    
    private BattleState _battleState;
    private MainMenuState _mainMenuState;
    private FreeRoamState _freeRoamState;
    private MainGameState _mainGameState;

    public GameState CurrentState;
    
    public MainGameState MainGameState { get; private set; }
    public FreeRoamState FreeRoamState { get; private set; }


    private void Start()
    {
        _battleState = new BattleState();
        _mainMenuState = new MainMenuState();
        CurrentState = _mainMenuState;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("key pressed");
            if (menuController is null)
            {
                Debug.Log("menu is null");
            }
            menuController.OpenOrCloseMenu();
        }
    }


    public void ChangeState()
    {
        CurrentState.ChangeState(this);
    }
}
