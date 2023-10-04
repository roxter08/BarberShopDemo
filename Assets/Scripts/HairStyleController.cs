using UnityEngine;

namespace BarberShopDemo
{
    public class HairStyleController : HairStyleStateMachine
    {
        [SerializeField]private Head head;
        [SerializeField]private TriggerDetector triggerDetector;
        public TriggerDetector TriggerDetector => triggerDetector;
        public Head Head => head;

        void Start()
        {
            //Set state to hair cutting as default
            CutHair();
        }
        void Update()
        {
            if (IsMouseButtonPressed(0))
            {
                MoveTriggerDetectorWithPointer();
                if (triggerDetector.IsTouching())
                {
                    GetCurrentState().Update();
                }
            }
        }

        private void MoveTriggerDetectorWithPointer()
        {
            triggerDetector.transform.position = GetPointerWorldPosition();
        }

        public void CutHair()
        {
            SetState(new HairCutting(this));
        }
        public void GrowHair()
        {
            SetState(new HairGrowing(this));
        }
        public void TrimHair()
        {
            SetState(new HairTrimming(this));
        }

        #region Helper Functions

        //Can be upgraded to NewInputSystem later for better input mapping
        private bool IsMouseButtonPressed(int index)
        {
            return Input.GetMouseButton(index);
        }

        public Vector2 GetPointerWorldPosition() 
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        #endregion
    }
}

