using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject optionsCanvas;
    public GameObject loadingCanvas;   // 👈 NEW

    public void PlayGame()
    {
        // Menus hide
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(false);

        // Loading show
        loadingCanvas.SetActive(true);

        // Scene load start
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Environment");

        while (!operation.isDone)
        {
            yield return null; // wait till scene load
        }
    }

    public void Options()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void BackFromOptions()
    {
        optionsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
