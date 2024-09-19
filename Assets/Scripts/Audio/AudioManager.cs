using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource bgSource; // Background music
        public AudioSource sfxSource; // Sound effects
        public AudioClip[] bgmClips;
        public AudioClip[] sfxClips;

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            PlayBGM(0);
        }

        public void PlayBGM(int index)
        {
            if (index >= 0 && index < bgmClips.Length)
            {
                bgSource.clip = bgmClips[index];
                bgSource.loop = true;
                bgSource.Play();
            }
        }

        // Phát hiệu ứng âm thanh
        public void PlaySFX(int index)
        {
            if (index >= 0 && index < sfxClips.Length)
            {
                sfxSource.PlayOneShot(sfxClips[index]);
            }
        }

        // Dừng nhạc nền
        public void StopBGM()
        {
            bgSource.Stop();
        }
    }
}
