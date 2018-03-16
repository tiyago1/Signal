using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager mAudioManager;

    public static AudioManager Instance
    {
        get
        {
            return mAudioManager;
        }
        set
        {
            mAudioManager = value;
        }
    }

    private AudioSource mSource;
    public AudioClip Clip;

    private void Start()
    {
        mAudioManager = this;
        mSource = this.GetComponent<AudioSource>();
    }

    public void FireHitSound()
    {
        mSource.PlayOneShot(Clip, 1);
    }
}
