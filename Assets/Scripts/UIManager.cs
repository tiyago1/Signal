using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singelaton

    private static UIManager mUIManager;
    public static UIManager Instance
    {
        get
        {
            return mUIManager;
        }
        set
        {
            mUIManager = value;
        }
    }

    #endregion

    #region Fields

    public Image SignalLevelImage;
    public Text LevelText;

    private float lerpValue = 1.0f;
    private float timeElapsedValue = 0.05f;
    private float t;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        mUIManager = this;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            if (SignalLevelImage.fillAmount > 0.125f)
            {
                t += timeElapsedValue * Time.deltaTime;
                SetSignalLevelColor();
                SignalLevelImage.fillAmount = Mathf.Lerp(lerpValue, 0.0f, t);
            }
            else
            {
                Time.timeScale = GameManager.TIME_SCALE_MOTION;
            }
        }
        else
        {
            SignalLevelImage.fillAmount = 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetSignalLevel(true);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SetSignalLevel(false);
        }
    }

    #endregion

    #region Public Methods

    public void SetSignalLevel(bool isPlusser)
    {
        lerpValue = SignalLevelImage.fillAmount;
        lerpValue = isPlusser ? lerpValue + 0.2f : lerpValue - 0.2f;
        t = 0;
    }

    public void OnRestartLevelButtonClicked()
    {
        Time.timeScale = GameManager.TIME_SCALE_DEFAULT;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Time.timeScale = GameManager.TIME_SCALE_DEFAULT;
    }

    #endregion

    #region Private Methods

    private void SetSignalLevelColor()
    {
        float amount = SignalLevelImage.fillAmount;

        if (amount > 0.62f)
        {
            SignalLevelImage.color = Color.green;
        }
        else if (amount < 0.62f && amount > 0.47f)
        {
            SignalLevelImage.color = Color.yellow;
        }
        else
        {
            SignalLevelImage.color = Color.red;
        }
    }

    #endregion
}
