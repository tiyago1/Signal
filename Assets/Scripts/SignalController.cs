using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Signal
{
    public class SignalController : MonoBehaviour
    {
        #region Fields

        private Rigidbody2D mRigidbody;
        private Vector2 mForceVector;
        private CircleCollider2D mCircleCollider;

        [Header("Effects")]
        public ParticleSystem ExplosionEffect;
        public ParticleSystem ExplosionEffectCopy;

        private Vector3[] Paths;

        public float duration;

        #endregion

        #region Unity Methods
        private void Awake()
        {
            Init();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                this.transform.DOPath(Paths, duration);
            }
        }

        #endregion

        #region Public Methods

        public void Move(Vector2 direction)
        {
            mForceVector = direction;
            mRigidbody.velocity = Vector2.one;
            mCircleCollider.isTrigger = false;
            mRigidbody.AddForce(mForceVector * GameManager.SIGNAL_FORCE_VALUE, ForceMode2D.Impulse);
            Time.timeScale = GameManager.TIME_SCALE_DEFAULT;
        }

        public void SetVelocity(bool value)
        {
            if (value)
            {
                if (mRigidbody.velocity.magnitude < 40)
                {
                    Vector2 velocity = new Vector2(mRigidbody.velocity.x * 2, mRigidbody.velocity.y * 2);
                    mRigidbody.velocity = velocity;
                }
            }
            else
            {
                if (mRigidbody.velocity.magnitude > 8.0f)
                {
                    Vector2 velocity = new Vector2(mRigidbody.velocity.x * 0.5f, mRigidbody.velocity.y * 0.5f);
                    mRigidbody.velocity = velocity;
                }
            }
        }

        public IEnumerator IdleAnimationCoroutine()
        {
            while (!GameManager.Instance.IsPlaying)
            {
                this.transform.DOPath(Paths, duration);
                yield return new WaitForSeconds(duration);
            }

            Time.timeScale = 0.5f;
            this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            StopCoroutine(IdleAnimationCoroutine());
        }

        public void Reset()
        {
            this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            this.gameObject.SetActive(false);
        }

        #endregion

        #region Private Methods
        private void Init()
        {
            mRigidbody = this.GetComponent<Rigidbody2D>();
            mCircleCollider = this.GetComponent<CircleCollider2D>();

            Paths = new Vector3[11]
            {
                new Vector3(0.0f,1.0f,0.0f),
                new Vector3(0.5f,1.0f,0.0f),
                new Vector3(0.0f,0.5f,0.0f),
                new Vector3(-1.0f,0.0f,0.0f),
                new Vector3(-0.5f,-0.5f,0.0f),
                new Vector3(0.0f,-1.0f,0.0f),
                new Vector3(0.5f,-0.5f,0.0f),
                new Vector3(1.0f,0.0f,0.0f),
                new Vector3(0.0f,0.5f,0.0f),
                new Vector3(0.5f,1.0f,0.0f),
                new Vector3(0.0f,1.0f,0.0f)
            };
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (this.tag == "Signal")
            {
                ExplosionEffect.transform.position = this.transform.position;
                ExplosionEffect.Play();
            }
            else
            {
                ExplosionEffectCopy.transform.position = this.transform.position;
                ExplosionEffectCopy.Play();
            }

            if (!GameManager.Instance.IsCurrentLevelFinished && collision.collider.tag == "Target")
            {
                GameManager.Instance.LevelFinished();
            }
        }

        #endregion
    }
}