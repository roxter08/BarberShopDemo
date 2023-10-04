using System.Collections.Generic;
using UnityEngine;

namespace BarberShopDemo
{
    public class HairGrowing : HairStyleState
    {
        private const float BRUSH_RADIUS = 0.13f;
        private Vector2 lastPointerPos;
        private int minHair;
        private int maxHair;

        public HairGrowing(HairStyleController hairStyleController) : base(hairStyleController)
        { 
            //Min and Max number of new hair to grow
            minHair = 1;
            maxHair = 10;
        }

        public override void Enter()
        {
            lastPointerPos = hairStyleController.GetPointerWorldPosition();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            Vector2 pointerPosition = hairStyleController.GetPointerWorldPosition();
            GrowNewHair(pointerPosition);
            GrowExistingHair(pointerPosition);
        }

        #region Private Methods

        private void GrowNewHair(Vector2 pointerPosition)
        {
            bool hasPointerMoved = Vector2.SqrMagnitude(lastPointerPos - pointerPosition) >= Mathf.Pow(0.1f, 2);
            if (hairStyleController.Head.IsPointerInsideHead(pointerPosition) && hasPointerMoved)
            {
                hairStyleController.Head.GrowNewHair(pointerPosition, Random.Range(minHair, maxHair));
                lastPointerPos = pointerPosition;
            }
        }

        private void GrowExistingHair(Vector2 pointerPosition)
        {
            List<Collider2D> results = new List<Collider2D>();
            int hairCount = Physics2D.OverlapCircle(pointerPosition, BRUSH_RADIUS, new ContactFilter2D().NoFilter(), results);
            if (hairCount > 0)
            {
                foreach (Collider2D col in results)
                {
                    if (HairCollection.TryGetValue(col, out Hair hair))
                    {
                        hair.GrowGradually();
                    }
                }
            }
        }

        #endregion
    }
}
