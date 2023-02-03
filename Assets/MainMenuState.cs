using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MainMenuState : GameState
    {
        public override void ChangeState(GameController gameController)
        {
            gameController.CurrentState = gameController.FreeRoamState;
            PlayGame();
        }
        
        private static void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            // Application.Quit();
        }
    }
}