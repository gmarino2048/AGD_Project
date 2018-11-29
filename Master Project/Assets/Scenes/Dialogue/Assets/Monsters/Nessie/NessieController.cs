using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class NessieController : MonsterController
    {
        [Header("Neutral Trigger")]
        public string NeutralTrigger;

        [Header("Bob animation")]
        public GameObject NessieSprite;
        public float Range;
        public float Boundary;
        public float BobDuration;

        float StartTime;

        bool Bobbing;
        bool Direction; // Sprite is bobbing up

        float Top;
        float Bottom;

        void Start()
        {
            Top = NessieSprite.transform.position.y + Range;
            Bottom = NessieSprite.transform.position.y - Range;

            StartTime = Time.time;
            Direction = true;

            Bobbing = true;
            StartCoroutine(NeutralAnimate(NeutralTrigger));
        }

        private void Update()
        {
            Vector3 spritePosition = NessieSprite.transform.position;

            float yPosition = spritePosition.y;
            float elapsedTime = Time.time - StartTime;
            float scaledTime = elapsedTime / BobDuration;

            float destination = Direction ? Top : Bottom;
            float newY = Mathf.SmoothStep(yPosition, destination, scaledTime);
            

            if (Mathf.Abs(newY - destination) <= Boundary)
            {
                StartTime = Time.time;
                Direction = !Direction;
            }

            NessieSprite.transform.position = new Vector3(spritePosition.x, newY, spritePosition.z);
        }

        IEnumerator NeutralAnimate (string neutralTrigger)
        {
            while (Bobbing)
            {
                float waitSeconds = Random.Range(5f, 15f);
                yield return new WaitForSeconds(waitSeconds);
                Animation.SetTrigger(neutralTrigger);
            }
        }
    }
}