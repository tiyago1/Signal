using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signal
{
    public class SplitterWall : Wall
    {
        private bool mIsActive;

        public override void OnCollisionDetection()
        {
            Debug.LogError("This wall isTrigger false!");
        }

        public override void OnTriggerEnterDetection()
        {
            GameManager.Instance.CreateSplitter(mCollisionDetectedPosition);
            mSpriteRenderer.color = Color.cyan;

            StartCoroutine(WaitAndWorkCoroutine());
        }

        private IEnumerator WaitAndWorkCoroutine()
        {
            mCollider.enabled = false;
            mSpriteRenderer.DOColor(Color.gray, 1.4f);
            mSpriteRenderer.DOFade(0.2f, 1.4f);

            yield return new WaitForSeconds(1.4f);

            mSpriteRenderer.DOColor(Color.cyan, 1.0f);
            mSpriteRenderer.DOFade(1.0f, 1.0f);
            mCollider.enabled = true;

            StopCoroutine(WaitAndWorkCoroutine());
        }
    }
}
