using UnityEngine;
using UnityEngine.XR;

namespace DefaultNamespace
{
    public class FreeRoamState : MainGameState
    {
        [SerializeField] private PlayerControllers playerControllers;

        private void FixedUpdate()
        {
            if (playerControllers is null)
            {
                Debug.Log("playerController is null (free roam state)");
            }
            playerControllers.HandleUpdate();
        }
        public override void ChangeState(GameController gameController)
        {
            gameController.CurrentState = gameController.BattleState;
        }
    }
}