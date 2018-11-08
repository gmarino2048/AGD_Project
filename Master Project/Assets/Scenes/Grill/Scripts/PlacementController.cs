using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grill
{
    public class PlacementController : MonoBehaviour
    {

        [Header("Cook Object Prefab")]
        public CookObjectController CookObject;

        List<CookObjectController> Active;

        Camera MainCamera;

        // Use this for initialization
        void Start()
        {
            MainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);


            }
        }
    }
}