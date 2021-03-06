﻿using System.Collections;
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

        bool FlameRunning;

        [Header("Animation Controllers")]
        public Animator MainController;
        public Animator FlameController;

        [Header("Animation State Names")]
        public string CookingStateName;
        public string BurningStateName;
        public string FlameStart = "Start";

        string PreviousState;
        float OriginalSpeed;

        [Header("Game Controls")]
        public TimerBehavior GameController;
        public ScoreKeeperBehavior ScoreKeeper;
        public PlacementController ActiveList;
        public BoxCollider2D MainCollider;
        public Camera MainCamera;
        public SFXController SFX;
        public AudioSource PlatingSound;
        
        public float InternalTimer { get; private set; }
        public float Score { get; private set; }

        // Use this for initialization
        void Start()
        {
            MainCamera = Camera.main;

            CookTime = TimeToCook;
            BurnTime = TimeToBurn;

            PreviousState = "";
            OriginalSpeed = MainController.speed;

            InternalTimer = 0f;
            
            FlameRunning = false;
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
            AnimatorStateInfo info = MainController.GetCurrentAnimatorStateInfo(0);

            if (GameController.GameActive)
            {
                UpdateAnimation(info);

                InternalTimer += Time.deltaTime;

                if (InternalTimer > CookTime + BurnTime && !FlameRunning)
                {
                    FlameController.SetTrigger(FlameStart);
                    SFX.PlayFire();
                    FlameRunning = true;
                }
            }
            else {
                MainController.speed = 0;
                PreviousState = "";
            }
        }

        void UpdateAnimation (AnimatorStateInfo info) 
        {
            if (info.IsName(CookingStateName) && PreviousState != CookingStateName)
            {
                MainController.speed = OriginalSpeed;
                float newSpeed = MainController.speed / CookTime;
                MainController.speed = newSpeed;

                PreviousState = CookingStateName;
            }

            if (info.IsName(BurningStateName) && PreviousState != BurningStateName)
            {
                MainController.speed = OriginalSpeed;
                float newSpeed = MainController.speed / BurnTime;
                MainController.speed = newSpeed;

                PreviousState = BurningStateName;
            }
        }

        void RemovePatty () {
            Score = CalculateScore();
            ScoreKeeper.AddScore(this);
            ActiveList.Active.Remove(this);

            PlatingSound.PlayOneShot(SFX.Spatula);
            DestroyImmediate(gameObject);
        }

        float CalculateScore () {
            if (InternalTimer <= CookTime)
            {
                return InternalTimer / CookTime;
            }
            if (InternalTimer > CookTime && InternalTimer <= CookTime + BurnTime)
            {
                float scaledTime = InternalTimer - CookTime;
                return 1 - (scaledTime / BurnTime);
            }
            return 0f;
        }
    }
}