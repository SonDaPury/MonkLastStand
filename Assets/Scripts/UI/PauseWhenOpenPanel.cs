using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

namespace UI
{
    public class PauseWhenOpenPanel : MonoBehaviour
    {
        public static bool gameIsPaused;
        public GameObject pauseMenuUI;
        public GameObject resumeButton;
        public GameObject skillPointButton;
        public GameObject settingButton;
        public GameObject restartButton;
        public GameObject quitButton;
        public GameObject skillPointPanel;
        public Text healthPointText;
        public Text attackPointText;
        public Text defensePointText;
        public Text staminaPointText;
        public Text skillPointText;
        public GameObject gameOver;
        public bool isRestart = false;

        private void Start()
        {
            gameIsPaused = false;
            isRestart = true;
            Time.timeScale = 1;
        }

        void Update()
        {
            skillPointText.text = "Points: " + PlayerStats.Instance.skillPoints;
            healthPointText.text = "HP: " + PlayerStats.Instance.health;
            attackPointText.text = "Atk: " + PlayerStats.Instance.attackDamage;
            defensePointText.text = "Def: " + PlayerStats.Instance.def;
            staminaPointText.text = "Sta: " + PlayerStats.Instance.stamina;

            if (PlayerManager.Instance.currentHealth <= 0)
            {
                gameOver.SetActive(true);
                Time.timeScale = 0;
            }
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

        public void OpenSkillPointPanel()
        {
            resumeButton.SetActive(false);
            skillPointButton.SetActive(false);
            settingButton.SetActive(false);
            restartButton.SetActive(false);
            quitButton.SetActive(false);
            skillPointPanel.SetActive(true);
        }

        public void CancelToPauseMenu()
        {
            resumeButton.SetActive(true);
            skillPointButton.SetActive(true);
            settingButton.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            skillPointPanel.SetActive(false);
        }

        public void RestartGame()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            gameIsPaused = false;
            gameOver.SetActive(false);
            Time.timeScale = 1;
            isRestart = true;
        }
    }
}
