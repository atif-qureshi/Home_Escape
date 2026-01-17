using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class SceneMessage
{
    public string sceneName;
    [TextArea] public string message;
    public float delayBeforeShow = 0f;   // New: delay before message shows
    public float displayTime = 3f;
}

public class SceneMessageManager : MonoBehaviour
{
    public GameObject messagePanel;
    public Text messageText;
    public AudioClip dingSound;
    public List<SceneMessage> sceneMessages = new List<SceneMessage>();
    public float fadeDuration = 0.5f;

    private CanvasGroup canvasGroup;
    private AudioSource audioSource;

    void Start()
    {
        // CanvasGroup for fade
        if (messagePanel != null)
        {
            canvasGroup = messagePanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = messagePanel.AddComponent<CanvasGroup>();
        }

        // AudioSource for ding
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Show first scene message
        ShowMessageForScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ShowMessageForScene(scene.name);
    }

    void ShowMessageForScene(string sceneName)
    {
        var msg = sceneMessages.Find(m => m.sceneName == sceneName);
        if (msg != null)
            StartCoroutine(DisplayMessage(msg));
    }

    IEnumerator DisplayMessage(SceneMessage msg)
    {
        if (messagePanel == null || messageText == null) yield break;

        // Wait for delay before showing message
        if (msg.delayBeforeShow > 0f)
            yield return new WaitForSeconds(msg.delayBeforeShow);

        messageText.text = msg.message;

        // Play ding
        if (dingSound != null)
            audioSource.PlayOneShot(dingSound);

        messagePanel.SetActive(true);

        // Fade In
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(msg.displayTime);

        // Fade Out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - t / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 0;
        messagePanel.SetActive(false);
    }
}
