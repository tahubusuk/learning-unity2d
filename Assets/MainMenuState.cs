using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MainMenuState : GameState
    {
        //i dont know if this is important. I think because the scene is different, maybe this does not need
        // the same state as it makes the workflow weird.
        
        private void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Owner.PushState("freeRoam");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}