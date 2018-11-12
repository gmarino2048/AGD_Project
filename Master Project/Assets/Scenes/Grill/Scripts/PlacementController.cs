using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grill
{
    public class PlacementController : MonoBehaviour
    {

        [Header("Cook Object Prefab")]
        public CookObjectController CookObject;
        public float ZIndex = -1;

        [Header("Grill Collider")]
        public PolygonCollider2D GrillCollider;

        [Header("Game Controller")]
        public TimerBehavior GameController;

        public List<CookObjectController> Active { get; private set; }

        Camera MainCamera;

        bool ValidPosition;

        // Use this for initialization
        void Start()
        {
            MainCamera = Camera.main;
            Active = new List<CookObjectController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameController.GameActive && Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);
                worldPosition.z = ZIndex;

                CookObjectController instantiated = Instantiate(CookObject);

                instantiated.gameObject.transform.position = worldPosition;

                bool within = WithinBounds(instantiated, GrillCollider);
                bool touching = IsTouching(instantiated, Active);

                if (within && !touching) Active.Add(instantiated);
                else DestroyImmediate(instantiated.gameObject);
            }
        }

        bool WithinBounds (CookObjectController self, PolygonCollider2D grill)
        {
            Vector3 sCenter = self.transform.position;
            Vector3 sBounds = self.MainCollider.bounds.extents;

            Vector3 top = new Vector3(sCenter.x, sCenter.y + sBounds.y, sCenter.z);
            Vector3 bottom = new Vector3(sCenter.x, sCenter.y - sBounds.y, sCenter.z);
            Vector3 left = new Vector3(sCenter.x - sBounds.x, sCenter.y, sCenter.z);
            Vector3 right = new Vector3(sCenter.x + sBounds.x, sCenter.y, sCenter.z);

            bool within = true;

            foreach (Vector3 position in new Vector3[] {top, bottom, left, right})
            {
                within = grill.bounds.Contains((Vector2)position) ? within && true : within && false;
            }

            return within;
        }

        bool IsTouching (CookObjectController self, List<CookObjectController> others) 
        {
            Vector3 sCenter = self.transform.position;
            Vector3 sbounds = self.MainCollider.bounds.extents;

            bool valid = true;

            foreach (CookObjectController other in others)
            {
                Vector3 oCenter = other.transform.position;
                Vector3 oBounds = other.MainCollider.bounds.extents;

                Vector2 dist = new Vector2(Mathf.Abs(sCenter.x - oCenter.x),
                                           Mathf.Abs(sCenter.y - oCenter.y));

                Vector2 bounds = new Vector2(sbounds.x + oBounds.x, sbounds.y + oBounds.y);

                valid = valid && (dist.x >= bounds.x || dist.y >= bounds.y);
            }

            return !valid;
        }
    }
}