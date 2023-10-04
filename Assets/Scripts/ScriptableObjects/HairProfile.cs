using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarberShopDemo
{
    [CreateAssetMenu(menuName = "Hair Profile")]
    public class HairProfile : ScriptableObject
    {
        [SerializeField] private GameObject hairFollicle;
        [Range(0.1f,5.0f)]
        [SerializeField] private float maxHairLength;
        [SerializeField] private float minHairLength;
        [SerializeField] private float growthSpeed;

        public GameObject HairFollicle { get => hairFollicle;}
        public float MaxHairLength { get => maxHairLength;}
        public float GrowthSpeed { get => growthSpeed;}
        public float MinHairLength { get => minHairLength;}

        public float GetHairSpriteLength()
        {
            Sprite hairSprite = hairFollicle.GetComponent<SpriteRenderer>().sprite;
            return hairSprite.rect.height / hairSprite.pixelsPerUnit;
        }
    }
}
