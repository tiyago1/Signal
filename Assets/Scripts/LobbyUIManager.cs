using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUIManager : MonoBehaviour
{
    public Rigidbody2D AnimatedPlayerRigid;
    public Text TabToPlayText;

    public GameObject Levels;
    public GameObject Main;

    private void Awake()
    {
#if UNITY_STANDALONE
        this.gameObject.SetActive(this.gameObject.name == "DesktopCanvas");
#elif UNITY_ANDROID
        this.gameObject.SetActive(this.gameObject.name == "MobileCanvas");
#endif
    }

    private void Start()
    {
        DOTween.Init();
        Test();
        StartCoroutine(AnimatedTabToPlayCorutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TabToPlayText.transform.DOShakeScale(1);
            TabToPlayText.DOColor(Random.ColorHSV(0, 1, 1, 1, 1, 1), 1);
        }

        if (Input.GetMouseButton(0))
        {
            //  SceneManager.LoadScene(1);
        }
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnLevelButtonClicked()
    {
        Main.SetActive(false);
        Levels.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        Main.SetActive(true);
        Levels.SetActive(false);
    }

    private void Test()
    {
        AnimatedPlayerRigid.velocity = Vector2.one;
        AnimatedPlayerRigid.AddForce(Vector2.up * (GameManager.SIGNAL_FORCE_VALUE * 0.5f), ForceMode2D.Impulse);
    }

    private IEnumerator AnimatedTabToPlayCorutine()
    {
        while (true)
        {
            TabToPlayText.transform.DOShakeScale(1);
            TabToPlayText.DOColor(Random.ColorHSV(0, 1, 1, 1, 1, 1), 1);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
