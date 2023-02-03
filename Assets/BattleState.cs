﻿namespace DefaultNamespace
{
    public class BattleState : MainGameState
    {
        
        public override void ChangeState(GameController gameController)
        {
            gameController.CurrentState = gameController.FreeRoamState;
        }
    }
}