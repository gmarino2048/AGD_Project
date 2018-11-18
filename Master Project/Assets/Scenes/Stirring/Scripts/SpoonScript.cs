using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stirring
{
    public class SpoonScript : MonoBehaviour
    {
        private const float _SPOON_Z_POS = 0f;
        public float _SPOON_MAX_RADIUS = 2f;

        //distance of the mouse from the center of the spoon
        private Vector3 _Offset;

        //where mouse is
        private Vector3 _MousePosition;

        //where mouse was last frame
        private Vector3 _PrevMousePosition;

        //where the center is
        public Vector3 _Center;

        //how far the spoon traveled
        public float travelDistance = 0;

        //if time is up
        public bool isTimerDone = false;

        // Use this for initialization
        void Start()
        {
            _Center = new Vector3(0, 0, 0);
        }


        /// <summary>
        /// keeps track of distance traveled
        /// </summary>
        void Update()
        {
            travelDistance += Vector3.Distance(transform.position, _PrevMousePosition);
            _PrevMousePosition = transform.position;
        }

        /// <summary>
        /// keeps the spoon for auto-centering on the mouse making it look disconnected from the previous frame
        /// </summary>
        void OnMousePressed()
        {
            _Offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        /// <summary>
        /// moves the spoon
        /// </summary>
        void OnMouseDrag()
        {
            if (!isTimerDone)
            {
                Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                _MousePosition = Camera.main.ScreenToWorldPoint(cursorPoint) + _Offset;

                var mouseRadius = Mathf.Min(_SPOON_MAX_RADIUS, Vector3.Distance(_MousePosition, _Center));

                var angle = Mathf.Atan2(_MousePosition.y, _MousePosition.x);

                var spoonX = Mathf.Cos(angle) * mouseRadius;
                var spoonY = Mathf.Sin(angle) * mouseRadius;

                transform.position = new Vector3(spoonX, spoonY, _SPOON_Z_POS);
            }
        }
    }
}
