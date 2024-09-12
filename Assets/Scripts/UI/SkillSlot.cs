using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string skillDescription;
    public TooltipManager tooltipManager;
    public GameObject tooltipPanel;
    public Vector3 offset;

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            tooltipPanel.transform.position = transform.position + offset;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipManager.ShowTooltip(skillDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipManager.HideTooltip();
    }
}
