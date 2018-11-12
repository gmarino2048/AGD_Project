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

        [Header("Game Controller")]
        public TimerBehavior GameController;

        public List<CookObjectController> Active { get; private set; }

        Camera MainCamera;

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

                CookObjectController instantiated = Instantiate(CookObject);

                if (CheckPosition(instantiated.gameObject))
                {
                    Active.Add(instantiated);

                    instantiated.transform.position =
                        new Vector3(worldPosition.x, worldPosition.y, ZIndex);
                }
                else DestroyImmediate(instantiated);
            }
        }

        bool CheckPosition (GameObject instantiated) 
        {
            Collider2D thisCollider = instantiated.GetComponent<Collider2D>();

            bool valid = true;

            if (thisCollider != null)
            {
                foreach (CookObjectController cookObject in Active)
                {
                    Collider2D other = cookObject.gameObject.GetComponent<Collider2D>();

                    valid = thisCollider.IsTouching(other) ? valid && false : valid && true;
                }
            }
            else valid = false;

            return valid;
        }
    }
}