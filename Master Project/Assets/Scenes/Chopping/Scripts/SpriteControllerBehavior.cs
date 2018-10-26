using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chopping
{
    public class SpriteControllerBehavior : MonoBehaviour
    {

        [Header("Chop Sprite")]
        [SerializeField]
        public Sprite KnifeCut;

        public List<SpriteRenderer> ChopSprites { get; set; }


        void Start()
        {
            ChopSprites = new List<SpriteRenderer>();
        }

        void Chop (Vector3 position, float width)
        {
            SpriteRenderer cutRenderer = new SpriteRenderer();
            cutRenderer.sprite = KnifeCut;

        }
    }
}