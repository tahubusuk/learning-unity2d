using UnityEngine;
using UnityEngine.XR;

namespace DefaultNamespace
{
    public class FreeRoamState : MainGameState
    {
        public PlayerControllers playerControllers;

        public override void PrepareState()
        {
            
        }

        public override void UpdateState()
        {
            playerControllers.HandleUpdate();
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Debug.Log("key pressed");
                Owner.PushState("pause");
            }
        }
    }
}