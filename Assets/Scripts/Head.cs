using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BarberShopDemo
{
    public class Head : MonoBehaviour
    {
        private const float HAIR_POINTS_RADIUS = 0.3f;
        

        [SerializeField] HairProfile hairProfile;
        [SerializeField] Transform centerPoint;
        [SerializeField] private bool prewarm;
        [SerializeField] float prewarmedHairLength;
        [Range(100,300)]
        [SerializeField] private int prewarmedHairCount;
        [Range(300, 400)]
        [SerializeField] private int maxHairCount;

        private Collider2D headCollider;
        private int HairCount => hairDictionary.Count;
        

        private Dictionary<Collider2D, Hair> hairDictionary;

        private void Awake()
        {
            hairDictionary = new Dictionary<Collider2D, Hair>();
            headCollider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            if (prewarm)
            {
                GrowNewHair(centerPoint.position, prewarmedHairCount);
                foreach(KeyValuePair<Collider2D, Hair> hairCollection in hairDictionary)
                {
                    hairCollection.Value.GrowInstant(prewarmedHairLength);
                }
            }
        }

        public void GrowNewHair(Vector2 position, int numberOfHair)
        {
            if(HairCount >= maxHairCount) return;

            Vector2[] hairPoints = GenerateRandomHairPoints(numberOfHair, position, HAIR_POINTS_RADIUS);
            for (int i = 0; i < numberOfHair; i++)
            {
                Vector2 newHairPosition = hairPoints[i];
                Quaternion newHairRotation = GetRotationForNewHair(newHairPosition);
                Hair newHair = GameObject.Instantiate(hairProfile.HairFollicle, newHairPosition, newHairRotation, this.transform).GetComponent<Hair>();
                newHair.Init(hairProfile, RemoveHairFromCollection);
                AddNewHairToCollection(newHair);
            }
        }

        public bool IsPointerInsideHead(Vector2 point)
        {
            return headCollider.OverlapPoint(point);
        }

        public Dictionary<Collider2D, Hair> GetHairCollection()
        {
            return hairDictionary;
        }

        #region Private Methods

        private void AddNewHairToCollection(Hair newHair)
        {
            hairDictionary.Add(newHair.GetHairCollider(), newHair);
        }

        private Quaternion GetRotationForNewHair(Vector2 hairPoint)
        {
            Vector2 direction = (hairPoint - (Vector2)centerPoint.position).normalized;

            float angle = Vector2.SignedAngle(centerPoint.position, direction) - 180f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return rotation;
        }

        private void RemoveHairFromCollection(Hair hair)
        {
            hairDictionary.Remove(hair.GetHairCollider());
        }

        private Vector2[] GenerateRandomHairPoints(int totalPoints, Vector2 center, float radius)
        {
            Vector2[] hairPoints = new Vector2[totalPoints];
            for(int i = 0;i < totalPoints;i++) 
            {
                Vector2 newPos = center + Random.insideUnitCircle * radius;
                
                while(hairPoints.Contains(newPos) || headCollider.OverlapPoint(newPos) == false)
                {
                    newPos = center + Random.insideUnitCircle * radius;
                }
                hairPoints[i] = newPos;
            }

            return hairPoints;
        }

        #endregion
    }
}