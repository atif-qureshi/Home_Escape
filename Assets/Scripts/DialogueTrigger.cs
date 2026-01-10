using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3, 5)]
    public string dialogueText;

    public float showTime = 3f;
    public bool playOnce = true;

    bool hasPlayed = false;

    public DialogueManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (playOnce && hasPlayed) return;

        manager.ShowDialogue(dialogueText, showTime);
        hasPlayed = true;
    }
}
