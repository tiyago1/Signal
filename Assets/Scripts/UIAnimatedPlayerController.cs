using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimatedPlayerController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().DOColor(Random.ColorHSV(0,1,1,1,1,1), 0.2f);
    }
}
