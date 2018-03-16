using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private void Awake()
    {
        //  Invoke("Show", 0.3f);
        ResetPosition();
        //this.gameObject.SetActive(false);
    }

    public void Show()
    {
        ResetPosition();
        this.gameObject.SetActive(true);
        this.transform.DOBlendableScaleBy(Vector3.one, 1.5f);
        this.transform.DOJump(Vector3.zero, 15, 2, 1);
        StartCoroutine(LevelCompleteCourutine());
    }

    public void Hide()
    {
        this.transform.DOScale(Vector3.zero, 0.4f);
        this.transform.DOJump(Vector3.one, 5,0, 0.2f);
        Invoke("ResetPosition", 0.4f);
    }

    private void ResetPosition()
    {
        this.gameObject.SetActive(false);
        this.transform.position = new Vector3(0.0f, -10.0f, 0.0f);
        this.transform.localScale = new Vector3(0.0001f, 0.00001f, 0.0f);
    }

    private IEnumerator LevelCompleteCourutine()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.LevelSetupCompleted();
    }
}
