using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SplitterWall : Wall
{
    private bool mIsActive;

    public override void OnCollisionDetection()
    {
        Debug.LogError("This wall isTrigger false!");
    }

    public override void OnTriggerEnterDetection()
    {
        GameManager.Instance.CreateSplitter();
        mSpriteRenderer.color = Color.cyan;

        StartCoroutine(WaitAndWorkCoroutine());
    }

    private IEnumerator WaitAndWorkCoroutine()
    {
        mCollider.enabled = false;
        mSpriteRenderer.DOFade(0.2f, 1.0f);
        yield return new WaitForSeconds(1.0f);

        mSpriteRenderer.DOFade(1.0f, 1.0f);
        mSpriteRenderer.DOColor(Color.gray, 1.0f);
        mCollider.enabled = true;
        StopCoroutine(WaitAndWorkCoroutine());
    }
}
