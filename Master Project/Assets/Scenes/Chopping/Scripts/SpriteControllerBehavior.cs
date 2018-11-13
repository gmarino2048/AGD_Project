using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chopping
{
    public class SpriteControllerBehavior : MonoBehaviour
    {

        [Header("Chop Sprite")]
        [SerializeField]
        public GameObject KnifeCut;
        SpriteRenderer Renderer;
        public Sprite CutSprite;

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
            GameObject child = Instantiate(KnifeCut);
            Renderer = child.GetComponent<SpriteRenderer>();

            child.transform.parent = gameObject.transform;

            Renderer.sprite = CutSprite;

            Renderer.color = new Color(255, 255, 255, 1);

            Vector3 position = PositionReference.transform.position;
            position.z = ZPosition;

            child.transform.position = position;
        }
    }
}