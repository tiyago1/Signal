using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signal
{
    public class SpeederWall : Wall
    {
        public override void OnCollisionDetection()
        {
            GameManager.Instance.SetSignalVelocity(true);
            mSpriteRenderer.color = Color.green;
        }

        public override void OnTriggerEnterDetection()
        {
            Debug.LogError("This wall isTrigger true!");
        }
    }
}