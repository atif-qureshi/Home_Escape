using UnityEngine;
using UnityEngine.SceneManagement;

public class BedInteract : MonoBehaviour
{
    public string level1SceneName = "Level1";

    public void Sleep()
    {
        SceneManager.LoadScene(level1SceneName);
    }
}
