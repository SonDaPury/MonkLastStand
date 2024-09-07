using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public Slider slider;
    public Text levelText;
    public Text expText;

    void Start()
    {
        slider.maxValue = PlayerStats.Instance.expToLevelUp;
        slider.value = PlayerStats.Instance.exp;
        levelText.text = PlayerStats.Instance.level.ToString();
        expText.text =
            Mathf
                .Floor(PlayerStats.Instance.exp / PlayerStats.Instance.expToLevelUp * 100)
                .ToString() + "%";
    }

    void Update()
    {
        slider.maxValue = PlayerStats.Instance.expToLevelUp;
        slider.value = PlayerStats.Instance.exp;
        levelText.text = PlayerStats.Instance.level.ToString();
        expText.text =
            Mathf
                .Floor(PlayerStats.Instance.exp / PlayerStats.Instance.expToLevelUp * 100)
                .ToString() + "%";
    }
}
