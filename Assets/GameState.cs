using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] MenuController menuController;
    [SerializeField] PlayerControllers playerControllers;

    private void Start()
    {
        
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
}
