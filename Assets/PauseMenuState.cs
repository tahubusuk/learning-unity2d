using UnityEngine;

namespace DefaultNamespace
{
    public class PauseMenuState : MainGameState
    {
        [SerializeField] GameObject menu;

        private bool _isActive = false;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //TODO observer pattern?
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("key pressed");
            }
        }

        public void ChangeState(GameController gameController)
        {
            menu.SetActive(_isActive = !_isActive);
            gameController.CurrentState = this;
        }
    }
}