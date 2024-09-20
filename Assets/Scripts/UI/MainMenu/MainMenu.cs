using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject volumeSetting;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject volumeSettingButton;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("GamePlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VolumeSetting()
    {
        volumeSetting.SetActive(true);
        playButton.SetActive(false);
        quitButton.SetActive(false);
        volumeSettingButton.SetActive(false);
    }

    public void CloseVolumeSetting()
    {
        volumeSetting.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
        volumeSettingButton.SetActive(true);
    }
}
