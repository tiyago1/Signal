using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Signal
{
    public class GameManager : MonoBehaviour
    {
        #region Singelaton

        private static GameManager mGameManager;
        public static GameManager Instance
        {
            get
            {
                return mGameManager;
            }
            set
            {
                mGameManager = value;
            }
        }

        #endregion

        #region Constants

        public const float MAX_WALL_TORQUE = 1.0f;
        public const float MIN_WALL_TORQUE = 0.5f;
        public const int SIGNAL_FORCE_VALUE = 6;
        public const float TIME_SCALE_MOTION = 0.1f;
        public const float TIME_SCALE_DEFAULT = 1.0f;

        #endregion

        #region Fields

        public Transform CenterPoint;
        public SignalController Signal;
        public SignalController SignalCopy;
        public CircleCollider2D CenterPointCollider;
        public bool IsPlaying;
        public bool IsCurrentLevelFinished;
        public bool CurrentLevelIsWon;
        public int Level;

        public List<Level> Levels;

        private bool mHasCopy;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (!IsCurrentLevelFinished && !IsPlaying && Input.GetMouseButtonDown(0))
            {
                
                IsPlaying = true;
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 centerPosition = new Vector2(CenterPoint.position.x, CenterPoint.position.y);
                Vector2 heading = position - centerPosition;
                var distance = heading.magnitude;
                Vector2 direction = heading / distance;
                Signal.Move(direction);
                StartCoroutine(ActivaterCenterPoint());
            }
        }

        private void Initialize()
        {
            mGameManager = this;
            Level = 0;
            ShowLevelState(Constants.LevelState.Load);
        }

        #endregion

        #region Public Methods

        public void LevelFinished()
        {
            IsCurrentLevelFinished = true;
            //IsPlaying = false; 
            Debug.Log("<<LevelFinished>>");
            StartCoroutine(LevelTransition());
        }

        public void SetSignalVelocity(bool value)
        {
            Signal.SetVelocity(value);
        }

        public void SetSignalLevel(bool value)
        {
            UIManager.Instance.SetSignalLevel(value);
        }

        public void CreateSplitter(Vector3 collisionDetectedPosition)
        {
            StartCoroutine(SplitterCoroutine(collisionDetectedPosition));
        }

        public void LevelSetupCompleted()
        {
            Signal.enabled = true;
            SignalCopy.enabled = true;
            Signal.gameObject.SetActive(true);
            StartCoroutine(Signal.IdleAnimationCoroutine());
        }

        #endregion

        #region Private Methods

        private IEnumerator LevelTransition()
        {
            Debug.LogError("Game Finished");
            StartCoroutine(ActivaterCenterPoint());
            IsPlaying = false;
            Time.timeScale = TIME_SCALE_MOTION;
            Signal.enabled = false;
            SignalCopy.enabled = false;
            yield return new WaitForSeconds(0.2f);
            Time.timeScale = TIME_SCALE_DEFAULT;
            Signal.Reset();
            SignalCopy.Reset();
            Levels[Level].Hide();
            yield return new WaitForSeconds(0.5f);
            UIManager.Instance.ShowWinLoseAlert(CurrentLevelIsWon);
        }

        public void ShowLevelState(Constants.LevelState state)
        {
            IsCurrentLevelFinished = false;

            //if (state != Constants.LevelState.Load)
            //    Levels[Level].Hide();

            if (state == Constants.LevelState.Next)
                Level++;

            UIManager.Instance.LevelText.text = Level.ToString();
            Levels[Level].Show();
        }

        private IEnumerator ActivaterCenterPoint()
        {
            Debug.LogAssertion("IsPlaying : " + IsPlaying);
            if(IsPlaying)
            {
                Debug.Log("Girdi" );
                yield return new WaitForSeconds(1.0f);
                CenterPointCollider.gameObject.SetActive(true);
                CenterPointCollider.isTrigger = false;
                //break;
            }

            if (!IsPlaying)
            {
                CenterPointCollider.isTrigger = true;
                CenterPointCollider.gameObject.SetActive(false);
            }
        }

        private IEnumerator SplitterCoroutine(Vector3 collisionDetectedPosition)
        {
            yield return new WaitForSeconds(0.01f);

            if (!mHasCopy)
            {
                SignalCopy.transform.position = collisionDetectedPosition;
                SignalCopy.gameObject.SetActive(true);
                SignalCopy.Move(Vector2.up);
                mHasCopy = true;
            }
            else
            {
                SignalCopy.gameObject.SetActive(false);
                mHasCopy = false;
            }
        }

        #endregion
    }
}