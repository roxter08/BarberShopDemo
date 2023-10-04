using UnityEngine;

namespace BarberShopDemo
{
    public abstract class HairStyleStateMachine : MonoBehaviour
    {
        private HairStyleState currentState;

        public void SetState(HairStyleState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public HairStyleState GetCurrentState()
        {
            return currentState;
        }
    }
}
