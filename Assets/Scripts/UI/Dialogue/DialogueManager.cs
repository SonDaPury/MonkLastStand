using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Ô hiển thị lời thoại
    public TextMeshProUGUI characterNameText; // Ô hiển thị tên nhân vật
    public GameObject dialoguePanel; // Panel chứa lời thoại
    public GameObject avatarImage; // Avatar
    public DialogueLine currentLine; // Câu thoại hiện tại
    private GameObject previousAvatar;

    public List<DialogueLine> dialogueLines; // Danh sách lời thoại
    private Queue<DialogueLine> dialogueQueue; // Hàng đợi lời thoại
    public GameObject boss;
    public GameObject player;
    public BossBehaviour bossBehaviour;
    public PlayerManager playerManager;
    public InputActionMap playerActionMap;
    public InputActionAsset inputActions;
    public bool IsDialogueFinished { get; private set; }

    private void Start()
    {
        bossBehaviour = boss.GetComponent<BossBehaviour>();
        bossBehaviour.enabled = false;

        playerManager = player.GetComponent<PlayerManager>();

        playerActionMap = inputActions.FindActionMap("Player");

        dialogueQueue = new Queue<DialogueLine>();
        IsDialogueFinished = false;

        foreach (DialogueLine line in dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }
    }

    public void StartDialogue()
    {
        IsDialogueFinished = false;
        bossBehaviour.enabled = false;
        playerManager.enabled = false;
        playerActionMap.FindAction("Move").Disable();
        playerActionMap.FindAction("Attack").Disable();
        playerActionMap.FindAction("Jump").Disable();
        playerActionMap.FindAction("Interact").Disable();
        playerActionMap.FindAction("FireballAttack").Disable();
        playerActionMap.FindAction("ShieldSkill").Disable();
        dialoguePanel.SetActive(true);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = dialogueQueue.Dequeue(); // Lấy câu thoại tiếp theo từ hàng đợi

        // Hiển thị tên nhân vật, avatar và nội dung câu thoại
        dialogueText.text = currentLine.dialogueText;
        characterNameText.text = currentLine.characterName;

        if (previousAvatar != null)
        {
            previousAvatar.SetActive(false);
        }

        if (currentLine.avatar != null)
        {
            currentLine.avatar.SetActive(true);
            previousAvatar = currentLine.avatar;
        }
        else
        {
            avatarImage.SetActive(false);
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        playerManager.enabled = true;
        bossBehaviour.enabled = true;
        IsDialogueFinished = true;
        playerActionMap.FindAction("Move").Enable();
        playerActionMap.FindAction("Attack").Enable();
        playerActionMap.FindAction("Jump").Enable();
        playerActionMap.FindAction("Interact").Enable();
        playerActionMap.FindAction("FireballAttack").Enable();
        playerActionMap.FindAction("ShieldSkill").Enable();
    }

    public void ContinueDialogueButton()
    {
        DisplayNextLine();
    }
}
