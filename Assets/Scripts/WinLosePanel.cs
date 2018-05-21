using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Signal
{
    public class WinLosePanel : MonoBehaviour
    {
        private const float SHOW_TIME = 0.1f;

        public Text Title;
        public List<GameObject> Buttons;

        private RectTransform mRect;

        public void Awake()
        {
            mRect = this.GetComponent<RectTransform>();
        }

        public void Show(bool isWon)
        {
            string message = String.Empty;
            Buttons.ForEach(it => it.SetActive(false));

            if (isWon)
            {
                message = "YOU WIN";
                Buttons[0].SetActive(true);
                Buttons[3].SetActive(true);
            }
            else
            {
                message = "YOU LOSE";
                Buttons[1].SetActive(true);
                Buttons[2].SetActive(true);
            }

            Title.text = message;
            mRect.DOLocalMove(Vector3.zero, SHOW_TIME);
        }

        private void Hide()
        {
            mRect.DOLocalMove(new Vector3(600.0f, 0.0f), SHOW_TIME);
        }

        #region Button Events

        public void OnRetryButtonClicked()
        {
            Hide();
            GameManager.Instance.ShowLevelState(Constants.LevelState.Retry);
        }

        public void OnNextButtonClicked()
        {
            Hide();
            GameManager.Instance.ShowLevelState(Constants.LevelState.Next);
        }

        public void OnMainMenuButtonClicked()
        {
            Hide();
            SceneManager.LoadScene(0);
        }

        public void OnVideoButtonClicked()
        {
            Hide();
            GameManager.Instance.ShowLevelState(Constants.LevelState.Retry);
        }

        #endregion
    }
}