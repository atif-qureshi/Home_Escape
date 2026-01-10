using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject dialoguePanel;

    Coroutine currentRoutine;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string text, float time)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(DialogueRoutine(text, time));
    }

    IEnumerator DialogueRoutine(string text, float time)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = text;

        yield return new WaitForSeconds(time);

        dialoguePanel.SetActive(false);
    }
}
