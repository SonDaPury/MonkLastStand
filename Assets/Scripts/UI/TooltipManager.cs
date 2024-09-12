using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;

    private void Start()
    {
        tooltipPanel.SetActive(false);
    }

    public void ShowTooltip(string skillDescription)
    {
        tooltipText.text = skillDescription;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}
