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

        [Header("Reference Object")]
        [SerializeField]
        public GameObject PositionReference;
        public float ZPosition;

        public List<SpriteRenderer> ChopSprites;


        void Start()
        {
            ChopSprites = new List<SpriteRenderer>();
        }


        public void DrawChop ()
        {
            GameObject child = new GameObject();
            child.transform.parent = gameObject.transform;

            SpriteRenderer cutRenderer = child.AddComponent<SpriteRenderer>();
            cutRenderer.sprite = KnifeCut;

            Vector3 position = PositionReference.transform.position;
            position.z = ZPosition;

            child.transform.position = position;
        }
    }
}