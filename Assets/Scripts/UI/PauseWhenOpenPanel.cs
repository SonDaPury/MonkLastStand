using System;
using UnityEngine;

namespace UI
{
    public class PauseWhenOpenPanel : MonoBehaviour
    {
        public static bool gameIsPaused;
        public GameObject pauseMenuUI;

        private void Start()
        {
            gameIsPaused = false;
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
    }
}
