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

        [Header("Sprite Height")]
        public float SpriteHeight;

        public List<SpriteRenderer> ChopSprites;


        void Start()
        {
            ChopSprites = new List<SpriteRenderer>();
        }


        public void Chop (Vector3 position, float width)
        {
            GameObject child = new GameObject();
            child.transform.parent = transform;

            SpriteRenderer cutRenderer = child.AddComponent<SpriteRenderer>();
            cutRenderer.sprite = KnifeCut;

            cutRenderer.transform.position = position;
            cutRenderer.size = new Vector2(width, SpriteHeight);
        }
    }
}