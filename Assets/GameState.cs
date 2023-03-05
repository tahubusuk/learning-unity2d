using UnityEngine;
using UnityEngine.XR;

namespace DefaultNamespace
{
    public abstract class GameState
    {
        public GameController Owner;

        public virtual void PrepareState()
        {
            
        }
        public virtual void UpdateState()
        {
            
        }

        public virtual void DestroyState()
        {
            
        }
    }
}