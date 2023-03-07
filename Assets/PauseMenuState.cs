using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PauseMenuState : MainGameState
    {
        [SerializeField] public GameObject Menu;

        public override void PrepareState()
        {
            Menu.SetActive(true);
        }
        public override void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("key pressed");
                Menu.SetActive(false);
                Owner.PopState();
                
            }
        }
    }
}