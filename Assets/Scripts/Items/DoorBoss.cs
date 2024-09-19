using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DoorBoss : MonoBehaviour
{
    public GameObject door3;
    public Door door3Script;
    public bool isDoorUsed = false;
    public DialogueManager dialogueManager;
    public GameObject bossHealthBar;

    // Switch camera
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera bossCamera;

    private void Start()
    {
        door3Script = door3.GetComponent<Door>();
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDoorUsed)
        {
            isDoorUsed = true;
            SwitchToBossCamera();
            dialogueManager.StartDialogue();
            StartCoroutine(
                door3Script.MoveDoor(door3Script.openPosition, door3Script.closedPosition)
            );
            StartCoroutine(WaitForDialogueToEnd());
        }
    }

    private void SwitchToBossCamera()
    {
        playerCamera.Priority = 0;
        bossCamera.Priority = 10;
    }

    private void SwitchToPlayerCamera()
    {
        bossCamera.Priority = 0;
        playerCamera.Priority = 10;
        bossHealthBar.SetActive(true);
    }

    private IEnumerator WaitForDialogueToEnd()
    {
        while (!dialogueManager.IsDialogueFinished)
        {
            yield return null;
        }

        SwitchToPlayerCamera();
    }
}
