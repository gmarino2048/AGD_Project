using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shaking
{
    public class AccumulationManager : MonoBehaviour
    {
        [Header("Scene Objects")]
        public SpriteRenderer Display;
        public ShakerBehavior Shakes;

        [Header("Settings")]
        public int Target = 100;
        int Count;
        int Segment;

        [Header("NoneSprite")]
        public Sprite None;

        [Header("Sprite List")]
        public List<Sprite> Accumulation;

        // Use this for initialization
        void Start()
        {
            Display.sprite = None;

            Count = Accumulation.Count;
            Segment = Target / Count;
        }

        // Update is called once per frame
        void Update()
        {
            int index = (Shakes.Shakes / Segment) - 1;

            if (index >= Count)
            {
                Display.sprite = Accumulation[Count - 1];
            }
            else if (index >= 0)
            {
                Display.sprite = Accumulation[index];
            }
        }
    }
}