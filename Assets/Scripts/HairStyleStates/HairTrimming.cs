using UnityEngine;

namespace BarberShopDemo
{
    public class HairTrimming : HairStyleState
    {
        public HairTrimming(HairStyleController hairStyleController) : base(hairStyleController)
        {
        }

        public override void Enter()
        {
            hairStyleController.TriggerDetector.OnTriggerCollided.AddListener(OnInteractedWithHair);
        }

        public override void Exit()
        {
            hairStyleController.TriggerDetector.OnTriggerCollided.RemoveListener(OnInteractedWithHair);
        }

        public override void Update()
        {

        }

        private void OnInteractedWithHair(Collider2D collider)
        {
            if(HairCollection.TryGetValue(collider, out Hair hair))
            {
                Vector3 hairBrushPosition = hairStyleController.GetPointerWorldPosition();
                hair.Shorten(hairBrushPosition, true);
            }
        }
    }
}
