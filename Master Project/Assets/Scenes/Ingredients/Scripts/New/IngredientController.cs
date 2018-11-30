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

        bool MouseInside;
           

        void Start()
        {
            MainCamera = Camera.main;
            MouseInside = false;
        }

        void OnMouseEnter()
        {
            Label.SetActive(true);
            MouseInside = true;
        }

        void OnMouseExit()
        {
            Label.SetActive(false);
            MouseInside = false;
        }

        void OnMouseDown()
        {
            if (MouseInside && !CorrectIngredient)
            {
                StartCoroutine(ShowX());
            }
        }

        IEnumerator ShowX ()
        {
            IncorrectMarker.SetActive(true);
            yield return new WaitForSeconds(1);
            IncorrectMarker.SetActive(false);
        }

        Vector3 MouseToWorldPoint (Vector3 mousePosition)
        {
            Vector3 worldPoint = MainCamera.ScreenToWorldPoint(mousePosition);
            return mousePosition;
        }
    }
}