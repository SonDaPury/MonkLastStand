using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource bgSource; // Background music
        public AudioSource sfxSource; // Sound effects
        public AudioClip[] bgmClips;
        public AudioClip[] sfxClips;
        public Slider backgroundVolumeSlider;

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
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (backgroundVolumeSlider != null)
            {
                float savedVolume = PlayerPrefs.GetFloat("BackgroundVolume", 0.2f);
                backgroundVolumeSlider.value = savedVolume;
                bgSource.volume = savedVolume;

                backgroundVolumeSlider.onValueChanged.AddListener(SetVolume);
            }

            PlayBGM(0);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenu")
            {
                PlayBGM(0);
            }
            else if (scene.name == "GamePlay")
            {
                PlayBGM(1);
            }
        }

        public void SetVolume(float volume)
        {
            bgSource.volume = volume;
            // sfxSource.volume = volume;
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
