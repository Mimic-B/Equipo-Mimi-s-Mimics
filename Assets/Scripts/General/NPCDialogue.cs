using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogues; // Array de diálogos
    public int currentDialogueIndex = 0;
    private bool isPlayerNearby = false;
    private bool isDialogueActive = false;

    public GameObject dialogueUI; // UI del diálogo
    public Text dialogueText; // Texto del diálogo

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                CompleteDialogue();
            }
        }

        if (isDialogueActive && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.E))
        {
            CompleteDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        ShowDialogue();
    }

    private void CompleteDialogue()
    {
        if (dialogueText.text == dialogues[currentDialogueIndex])
        {
            NextDialogue();
        }
        else
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
    }

    private void ShowDialogue()
    {
        dialogueText.text = "";
        StartCoroutine(TypeSentence(dialogues[currentDialogueIndex]));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void NextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Length)
        {
            ShowDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);
        currentDialogueIndex = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}

