using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients
{
    public class IngredientController : MonoBehaviour
    {

        [Header("Components")]
        public IngredientType Type;
        public BoxCollider2D Collider;

        public GameObject Label;
        public GameObject IncorrectMarker;
        public bool CorrectIngredient { get; private set; }

        [Header("Scene Objects")]
        public Camera MainCamera;
        public List<Collider2D> Plates;
        public BoxCollider2D SceneBorder;

        bool Dragging;
        bool MouseInside;
        Vector3 Offset;
           

        void Start()
        {
            MainCamera = Camera.main;
            Dragging = false;
            MouseInside = false;
            CorrectIngredient = true;

            Label.SetActive(false);
            IncorrectMarker.SetActive(false);
        }

        void OnMouseEnter()
        {
            if (!Dragging) Label.SetActive(true);
            MouseInside = true;
        }

        void OnMouseExit()
        {
            Label.SetActive(false);
            MouseInside = false;
        }

        void OnMouseDown()
        {
            if (MouseInside)
            {
                if (CorrectIngredient)
                {
                    Label.SetActive(false);
                    Offset = gameObject.transform.position - MouseToWorldPoint();
                }
                else StartCoroutine(ShowX());
            }
        }

        private void OnMouseDrag()
        {
            Dragging = true;
            if (CorrectIngredient)
            {
                Vector3 newPosition = MouseToWorldPoint() + Offset;
                gameObject.transform.position = newPosition;
                Offset = gameObject.transform.position - MouseToWorldPoint();
            }
        }

        private void OnMouseUp()
        {
            Dragging = false;
        }

        IEnumerator ShowX ()
        {
            IncorrectMarker.SetActive(true);
            yield return new WaitForSeconds(1);
            IncorrectMarker.SetActive(false);
        }

        Vector3 MouseToWorldPoint ()
        {
            Vector3 worldPoint = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            return worldPoint;
        }
    }
}