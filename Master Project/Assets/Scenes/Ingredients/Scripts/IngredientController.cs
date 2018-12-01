using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;

namespace Ingredients
{
    public class IngredientController : MonoBehaviour
    {

        [Header("Components")]
        public IngredientType Type;
        public BoxCollider2D Collider;

        public GameObject Label;
        public GameObject IncorrectMarker;
        public bool Active { get; set; }
        public bool InRecipe { get; set; }

        [Header("Scene Objects")]
        public Camera MainCamera;
        public List<PlateController> Plates;

        bool Dragging;
        bool MouseInside;
        Vector3 Offset;

        void Start()
        {
            MainCamera = Camera.main;
            Active = false;
            Dragging = false;
            MouseInside = false;

            Label.SetActive(false);
            IncorrectMarker.SetActive(false);
        }

        void OnMouseEnter()
        {
            if (Active)
            {
                if (!Dragging) Label.SetActive(true);
                MouseInside = true;
            }
        }

        void OnMouseExit()
        {
            Label.SetActive(false);
            MouseInside = false;
        }

        void OnMouseDown()
        {
            if (MouseInside && Active)
            {
                if (InRecipe)
                {
                    Label.SetActive(false);
                    Offset = gameObject.transform.position - MouseToWorldPoint();
                }
                else StartCoroutine(ShowX());
            }
        }

        private void OnMouseDrag()
        {
            if (Active)
            {
                Dragging = true;
                Cursor.lockState = CursorLockMode.Confined;
                if (InRecipe)
                {
                    foreach (PlateController plate in Plates)
                    {
                        bool touching = IsTouching(Collider, plate.Collider);
                        bool used = plate.Used;

                        if (touching && !used)
                        {
                            Vector3 reference = plate.Reference.transform.position;

                            gameObject.transform.position = new Vector3(reference.x, reference.y, reference.z);
                            plate.SetFood();
                            Active = false;
                        }
                    }
                    if (Active)
                    {
                        Vector3 newPosition = MouseToWorldPoint() + Offset;
                        gameObject.transform.position = newPosition;
                        Offset = gameObject.transform.position - MouseToWorldPoint();
                    }
                }
            }
        }

        private void OnMouseUp()
        {
            Dragging = false;
            Cursor.lockState = CursorLockMode.None;
        }

        IEnumerator ShowX ()
        {
            MonsterFactory monsterFactory = FindObjectOfType<MonsterFactory>();
            GameNarrativeManager narrativeManager = FindObjectOfType<GameNarrativeManager>();
            try
            {
                var monsterData = monsterFactory.LoadMonster(narrativeManager.CurrentStage.MonsterID);
                monsterData.UpdateAffectionFromIngredientSelection(Type);

                if (monsterData.AffectionValue <= monsterData.FightThreshold)
                {
                    var combatInitiator = FindObjectOfType<CombatInitiator>();
                    combatInitiator.InitiateCombat(narrativeManager.CurrentStage.MonsterID, 1 - monsterData.AffectionValue);
                }
            }
            catch (Exception)
            {
                Debug.LogWarning("Scene Not Running in Game");
            }

            IncorrectMarker.SetActive(true);
            yield return new WaitForSeconds(1);
            IncorrectMarker.SetActive(false);
        }

        Vector3 MouseToWorldPoint ()
        {
            Vector3 worldPoint = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            return worldPoint;
        }

        bool IsTouching(Collider2D self, Collider2D other)
        {
            Vector3 sCenter = self.transform.position;
            Vector3 sbounds = self.bounds.extents;

            bool valid = true;

            Vector3 oCenter = other.transform.position;
            Vector3 oBounds = other.bounds.extents;

            Vector2 dist = new Vector2(Mathf.Abs(sCenter.x - oCenter.x),
                                       Mathf.Abs(sCenter.y - oCenter.y));

            Vector2 bounds = new Vector2(sbounds.x + oBounds.x, sbounds.y + oBounds.y);

            valid = valid && (dist.x >= bounds.x || dist.y >= bounds.y);

            return !valid;
        }
    }
}