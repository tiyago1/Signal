using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPorousWall : Wall
{
    public override void OnCollisionDetection()
    {
        //Debug.Log("mSpriteRenderer.gameObject.name : " + mSpriteRenderer.gameObject.name);
        //mSpriteRenderer.color = Color.blue;
    }

    public override void OnTriggerEnterDetection()
    {
        Debug.LogError("This wall isTrigger true!");
    }
}
