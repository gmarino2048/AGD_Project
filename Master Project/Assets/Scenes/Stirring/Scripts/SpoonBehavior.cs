using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stirring
{
    public class SpoonBehavior : MonoBehaviour
    {

        [Header("Game Controls")]
        public TimerBehavior Timer;

        [Header("Spoon Behavior Settings")]
        public float Radius;
        public BoxCollider2D SpoonCollider;
        public Camera MainCamera;

        public bool Dragging { get; private set; }
        public bool Direction { get; private set; }
        public float Distance { get; private set; }

        Vector3 Center;
        Vector3 Offset;

        Vector3 LastPosition;
        float LastAngle;

        // Use this for initialization
        void Start()
        {
            Center = gameObject.transform.position;
            Dragging = false;
        }

        void OnMouseDown()
        {
            if (Timer.GameActive)
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);
                worldPosition.z = SpoonCollider.transform.position.z;

                if (SpoonCollider.bounds.Contains(worldPosition))
                {
                    Offset = gameObject.transform.position - worldPosition;
                    LastPosition = gameObject.transform.position;

                    LastAngle = Mathf.Atan2(gameObject.transform.position.y,
                                            gameObject.transform.position.x);

                    Dragging = true;
                }
                else Dragging = false;
            }
        }

        void OnMouseDrag()
        {
            if (Dragging && Timer.GameActive)
            {
                // Get the new position
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);
                worldPosition.z = SpoonCollider.transform.position.z;

                Vector3 objectPosition = worldPosition + Offset;
                float angle = Mathf.Atan2(objectPosition.y, objectPosition.x);

                float zPosition = gameObject.transform.position.z;

                Direction = angle < LastAngle;
                Debug.Log(Direction);
                LastAngle = angle;

                float objectRadius = Mathf.Min(Radius, Vector3.Distance(objectPosition, Center));

                float objectX = Mathf.Cos(angle) * objectRadius;
                float objectY = Mathf.Sin(angle) * objectRadius;

                gameObject.transform.position = new Vector3(objectX, objectY, zPosition);

                Distance += Vector3.Distance(gameObject.transform.position, LastPosition);
                LastPosition = gameObject.transform.position;
            }
        }

        void OnMouseUp() { Dragging = false; }
    }
}