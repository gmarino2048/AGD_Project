using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
    public class PlateController : MonoBehaviour
    {
        [Header("Components")]
        public SpriteRenderer Renderer;
        public BoxCollider2D Collider;
        public GameObject Reference;

        public bool Used { get; private set; }

        private void Start()
        {
            Used = false;
        }

        public void SetFood ()
        {
            Used = true;
        }
    }
}