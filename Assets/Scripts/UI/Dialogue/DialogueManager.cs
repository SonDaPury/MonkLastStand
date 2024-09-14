using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Ô hiển thị lời thoại
    public TextMeshProUGUI characterNameText; // Ô hiển thị tên nhân vật
    public GameObject dialoguePanel; // Panel chứa lời thoại
    public GameObject avatarImage; // Avatar
    public DialogueLine currentLine; // Câu thoại hiện tại

    public List<DialogueLine> dialogueLines; // Danh sách lời thoại
    private Queue<DialogueLine> dialogueQueue; // Hàng đợi lời thoại

    private void Start()
    {
        dialogueQueue = new Queue<DialogueLine>();

        foreach (DialogueLine line in dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }
    }

    public void StartDialogue()
    {
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

        currentLine?.avatar.SetActive(false); // Ẩn avatar của câu thoại trước

        currentLine = dialogueQueue.Dequeue(); // Lấy câu thoại tiếp theo từ hàng đợi

        // Hiển thị tên nhân vật, avatar và nội dung câu thoại
        dialogueText.text = currentLine.dialogueText;
        characterNameText.text = currentLine.characterName;

        if (currentLine.avatar != null)
        {
            currentLine.avatar.SetActive(true);
        }
        else
        {
            avatarImage.SetActive(false);
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue Ended");
    }

    public void ContinueDialogueButton()
    {
        DisplayNextLine();
    }
}
