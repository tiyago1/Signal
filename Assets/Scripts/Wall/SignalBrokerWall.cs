using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signal
{
    public class SignalBrokerWall : Wall
    {
        public override void OnCollisionDetection()
        {
            GameManager.Instance.SetSignalLevel(false);
            mSpriteRenderer.color = Color.red;
        }

        public override void OnTriggerEnterDetection()
        {
            Debug.LogError("This wall isTrigger true!");
        }
    }
}