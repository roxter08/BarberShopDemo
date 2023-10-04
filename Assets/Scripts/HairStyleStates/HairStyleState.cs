using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarberShopDemo
{
    public abstract class HairStyleState
    {
        protected HairStyleController hairStyleController;
        protected Dictionary<Collider2D, Hair> HairCollection { get; private set; }

        public HairStyleState(HairStyleController hairStyleController)
        {
            this.hairStyleController = hairStyleController;
            HairCollection = hairStyleController.Head.GetHairCollection();
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}
