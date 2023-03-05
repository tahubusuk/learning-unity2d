using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public GameState currentState;
    [SerializeField] private PlayerControllers playerControllers;
    [SerializeField] private GameObject menu;
    private Dictionary<string, GameState> _stateDict = new Dictionary<string, GameState>();
    private Stack<GameState> _stateStack = new Stack<GameState>();

    private void Start()
    {
        if (currentState is null)
        {
            Debug.Log("current state is null");
        }
        
        //make dict for (string, state)
        //making it here because of the pause menu that is a prefab and i still dont know how to load the prefab in non-monobehaviour class
        _stateDict.Add("mainMenu", new MainMenuState{Owner = this});
        _stateDict.Add("pause", new PauseMenuState{Owner = this, Menu = menu});
        _stateDict.Add("freeRoam", new FreeRoamState{Owner = this, playerControllers = playerControllers});
        
        currentState = _stateDict["freeRoam"];
        _stateStack.Push(currentState);
        Debug.Log(_stateStack.Peek());
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void PushState(string stateID)
    {
        Debug.Log(_stateDict[stateID]);
        _stateStack.Push(_stateDict[stateID]);
        currentState = _stateDict[stateID];
        currentState.PrepareState();
    }

    public void PopState()
    {
        _stateStack.Pop();
        Debug.Log(_stateStack.Peek());
        currentState = _stateStack.Peek();
    }
    
}
