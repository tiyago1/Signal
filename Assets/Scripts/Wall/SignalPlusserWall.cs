using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signal
{
    public class SignalPlusserWall : Wall
    {
        public override void OnCollisionDetection()
        {
            GameManager.Instance.SetSignalLevel(true);
            mSpriteRenderer.color = new Color(0.5f, 0.5f, 0);
        }

        public override void OnTriggerEnterDetection()
        {
            Debug.LogError("This wall isTrigger true!");
        }
    }
}