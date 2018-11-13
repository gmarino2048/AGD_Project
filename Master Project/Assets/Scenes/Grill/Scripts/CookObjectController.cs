using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grill
{
    public class CookObjectController : MonoBehaviour
    {

        [Header("Cook Times")]
        public int TimeToCook;
        public int TimeToBurn;

        float CookTime;
        float BurnTime;

        [Header("Animation Controller")]
        public Animator Controller;

        [Header("Animation State Names")]
        public string CookingStateName;
        public string BurningStateName;

        string PreviousState;
        float OriginalSpeed;

        [Header("Game Controls")]
        public TimerBehavior GameController;
        public ScoreKeeperBehavior ScoreKeeper;
        public PlacementController ActiveList;
        public BoxCollider2D MainCollider;
        public Camera MainCamera;
        
        public float InternalTimer { get; private set; }
        public float Score { get; private set; }

        // Use this for initialization
        void Start()
        {
            MainCamera = Camera.main;

            CookTime = TimeToCook;
            BurnTime = TimeToBurn;

            PreviousState = "";
            OriginalSpeed = Controller.speed;

            InternalTimer = 0f;
        }

        public void CheckIfRemoved()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);

            Vector3 scaled = new Vector3(worldPosition.x, worldPosition.y, MainCollider.transform.position.z);

            if (MainCollider.bounds.Contains(scaled))
            {
                RemovePatty();
                ActiveList.AnyRemoved = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            AnimatorStateInfo info = Controller.GetCurrentAnimatorStateInfo(0);

            if (GameController.GameActive)
            {
                UpdateAnimation(info);

                InternalTimer += Time.deltaTime;
            }
            else {
                Controller.speed = 0;
                PreviousState = "";
            }
        }

        void UpdateAnimation (AnimatorStateInfo info) 
        {
            if (info.IsName(CookingStateName) && PreviousState != CookingStateName)
            {
                Controller.speed = OriginalSpeed;
                float newSpeed = Controller.speed / CookTime;
                Controller.speed = newSpeed;

                PreviousState = CookingStateName;
            }

            if (info.IsName(BurningStateName) && PreviousState != BurningStateName)
            {
                Controller.speed = OriginalSpeed;
                float newSpeed = Controller.speed / BurnTime;
                Controller.speed = newSpeed;

                PreviousState = BurningStateName;
            }
        }

        void RemovePatty () {
            Score = CalculateScore();
            ScoreKeeper.AddScore(this);
            // TODO: Implement Flip animation
            ActiveList.Active.Remove(this);
            DestroyImmediate(gameObject);
        }

        float CalculateScore () {
            if (InternalTimer <= CookTime)
            {
                return 1f - (InternalTimer / CookTime);
            }
            if (InternalTimer > CookTime && InternalTimer <= CookTime + BurnTime)
            {
                float scaledTime = InternalTimer - CookTime;
                return scaledTime / BurnTime;
            }
            return 1f;
        }
    }
}