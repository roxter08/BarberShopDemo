using UnityEngine;
using UnityEngine.Events;

namespace BarberShopDemo
{
    public class TriggerDetector : MonoBehaviour
    {
        private const string LAYER_NAME = "BodyPart";
        private int layerMask;
        private Collider2D thisCollider;
        public UnityEvent<Collider2D> OnTriggerCollided;

        private void Awake()
        {
            layerMask = 1 << LayerMask.NameToLayer(LAYER_NAME);
            thisCollider = GetComponent<Collider2D>();
        }

        private void Start()
        {
           
        }
        public bool IsTouching()
        {
            return thisCollider.IsTouchingLayers(layerMask);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerCollided?.Invoke(other);
        }
    }
}
