using UnityEngine;
using UnityEngine.XR;

namespace DefaultNamespace
{
    public abstract class GameState : MonoBehaviour
    {
        public virtual void ChangeState(GameController gameController)
        {
            
        }

    }
}