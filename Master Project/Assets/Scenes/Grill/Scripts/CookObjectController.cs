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
        public BoxCollider2D MainCollider;

        public float Score { get; private set; }

        // Use this for initialization
        void Start()
        {
            CookTime = TimeToCook;
            BurnTime = TimeToBurn;

            PreviousState = "";
            OriginalSpeed = Controller.speed;
        }

        // Update is called once per frame
        void Update()
        {
            AnimatorStateInfo info = Controller.GetCurrentAnimatorStateInfo(0);

            if (GameController.GameActive)
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
            else {
                Controller.speed = 0;
                PreviousState = "";
            }
        }
    }
}