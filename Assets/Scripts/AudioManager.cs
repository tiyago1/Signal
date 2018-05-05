using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signal
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton 

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

        #endregion

        #region Fields

        public AudioClip[] SoundTracks;
        public AudioClip MenuMusic;
        public AudioSource SoundTrackSource;
        public AudioSource MenuMusicSource;

        private int mCounter;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            mAudioManager = this;
            mCounter = -1;
            StartMenuMusic();
        }

        public void PlayClip(Constants.SoundType type)
        {
            SoundTrackSource.PlayOneShot(SoundTracks[(int)type], 1);
        }

        public void PauseClip(Constants.SoundType type, bool pause)
        {
            if (pause)
                SoundTrackSource.Pause();
            else
                SoundTrackSource.UnPause();
        }

        public void StopClip(Constants.SoundType type)
        {
            SoundTrackSource.Stop();
        }

        #endregion

        #region Private Methods

        private void StartMenuMusic()
        {
            StartCoroutine(MenuMusicCoroutine());
        }

        private IEnumerator MenuMusicCoroutine()
        {
            MenuMusicSource.PlayOneShot(MenuMusic, 1);
            MenuMusicSource.loop = true;

            while (true)
            {
                mCounter++;
                yield return new WaitForSeconds(60.46f);

                if (mCounter == 2)
                {
                    mCounter = -1;
                    MenuMusicSource.Pause();
                    yield return new WaitForSeconds(60.0f);
                }
            }
            //while (true)
            //{
            //    yield return new WaitForSeconds(2.0f);
            //    MenuMusicSource.Pause();
            //    yield return new WaitForSeconds(1.0f);
            //    MenuMusicSource.UnPause();
            //    yield return new WaitForSeconds(0.39f);
            //}
        }

        #endregion
    }
}