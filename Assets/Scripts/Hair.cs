using System;
using UnityEngine;

namespace BarberShopDemo
{
    public class Hair : MonoBehaviour
    {
        private Action<Hair> OnHairRemoved;
        private float hairSpriteLength;
        private HairProfile hairProfile;
        private Vector2 origin;
        private Collider2D hairCollider;

        public void Start()
        {
            origin = transform.position;
        }

        public void Init(HairProfile hairProfile, Action<Hair> OnHairRemoved)
        {
          this.hairProfile = hairProfile;
          hairSpriteLength = hairProfile.GetHairSpriteLength();
          this.OnHairRemoved = OnHairRemoved;
          hairCollider = this.GetComponent<Collider2D>();
        }

       public void Shorten(Vector2 snippingPoint, bool removeAtRoot)
       {
            float yScale = transform.localScale.y;
            if (yScale <= hairProfile.MinHairLength)
            {
                if(removeAtRoot) 
                {
                    Remove();
                }
                return;
            }

            Vector2 heading = snippingPoint - origin;
            Vector2 onNormal = transform.up;
            Vector2 projectedVector = Vector3.Project(heading, onNormal);
            float finalScale = projectedVector.magnitude / hairSpriteLength;
            finalScale = Mathf.Clamp(finalScale, hairProfile.MinHairLength, yScale);
            transform.localScale = new Vector3(transform.localScale.x, finalScale, 1.0f);
        }

        public void GrowGradually()
        {
            Vector3 localScale = transform.localScale;
            if (localScale.y >= hairProfile.MaxHairLength)
            {
                return;
            }
            localScale += Vector3.up * hairProfile.GrowthSpeed * Time.deltaTime;
            transform.localScale = localScale;
        }

        public void GrowInstant(float hairLength)
        {
            Vector3 localScale = transform.localScale;
            localScale.y = Mathf.Min(hairLength, hairProfile.MaxHairLength);
            transform.localScale = localScale;
        }

        public void Remove()
        {
            if(OnHairRemoved != null)
            {
                OnHairRemoved.Invoke(this);
            }
            
            Destroy(this.gameObject);
        }

        public Collider2D GetHairCollider() 
        {
            return hairCollider;
        }
    }
}
