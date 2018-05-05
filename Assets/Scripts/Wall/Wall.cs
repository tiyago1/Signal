using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Signal
{
    public abstract class Wall : MonoBehaviour
    {
        protected SpriteRenderer mSpriteRenderer;
        protected EdgeCollider2D mCollider;
        protected ConstantForce2D mConstantForce;
        protected Rigidbody2D mRigidBody;

        protected Vector3 mCollisionDetectedPosition;

        private void Awake()
        {
            mSpriteRenderer = this.GetComponent<SpriteRenderer>();
            mCollider = this.GetComponent<EdgeCollider2D>();
            mConstantForce = this.GetComponent<ConstantForce2D>();
            mRigidBody = this.GetComponent<Rigidbody2D>();

            if (mSpriteRenderer == null)
                mSpriteRenderer = this.GetComponentInParent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Signal" || collision.collider.tag == "SignalCopy")
            {
                mCollisionDetectedPosition = collision.transform.position;
                OnCollisionDetection();
                AudioManager.Instance.PlayClip(Constants.SoundType.SignalCollision);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Signal")
            {
                mCollisionDetectedPosition = collider.transform.position;
                OnTriggerEnterDetection();
                AudioManager.Instance.PlayClip(Constants.SoundType.SignalCollision);
            }
        }

        public abstract void OnCollisionDetection();
        public abstract void OnTriggerEnterDetection();
    }
}