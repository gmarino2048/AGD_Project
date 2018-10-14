using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stirring
{
    public class spoonScript : MonoBehaviour
    {

        //distance of the mouse from the center of the spoon
        private Vector3 Offset;

        //where mouse is
        private Vector3 MousePosition;

        //where mouse was last frame
        private Vector3 PrevMousePosition;

        //how far the spoon traveled
        public float TravelDistance = 0;

        //angle of mouse relative to center
        public float Angle;

        //how far the mouse is from the center
        public float MouseRadius;

        //where the center is
        private Vector3 Center;

        //x location of spoon
        public float SpoonX;

        //y location of spoon
        public float SpoonY;

        //if time is up
        public bool TimerDone = false;


        // Use this for initialization
        void Start()
        {
            Center = new Vector3(0, 0, 0);
        }


        /// <summary>
        /// keeps track of distance traveled
        /// </summary>
        void Update()
        {

            TravelDistance += Vector3.Distance(transform.position, PrevMousePosition);


            PrevMousePosition = transform.position;
        }

        /// <summary>
        /// keeps the spoon for auto-centering on the mouse making it look disconnected from the previous frame
        /// </summary>
        void OnMousePressed()
        {
            Offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        /// <summary>
        /// moves the spoon
        /// </summary>
        void OnMouseDrag()
        {
            if (!TimerDone)
            {
                Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                MousePosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
                //transform.position = mousePosition;

                MouseRadius = Vector3.Distance(MousePosition, Center);

                Angle = Mathf.Atan2(MousePosition.y, MousePosition.x);

                if (MouseRadius < 2)
                {
                    SpoonX = Mathf.Cos(Angle) * MouseRadius;
                    SpoonY = Mathf.Sin(Angle) * MouseRadius;
                }
                else
                {
                    SpoonX = Mathf.Cos(Angle) * 2;
                    SpoonY = Mathf.Sin(Angle) * 2;
                }

                transform.position = new Vector3(SpoonX, SpoonY, 0);
            }
        }
    }
}
