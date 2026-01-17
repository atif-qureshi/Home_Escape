//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerHealth : MonoBehaviour
//{
//    [Header("Health Settings")]
//    public int maxHealth = 100;
//    public int currentHealth;

//    [Header("UI")]
//    public Slider healthBar;

//    [Header("Damage Feedback")]
//    public AudioSource damageSound;
//    public float damageFlashDuration = 0.2f;
//    public Image damageOverlay;

//    private void Start()
//    {
//        currentHealth = maxHealth;

//        if (healthBar != null)
//        {
//            healthBar.maxValue = maxHealth;
//            healthBar.value = currentHealth;
//        }
//    }

//    // Call this method to apply damage
//    public void TakeDamage(int amount)
//    {
//        currentHealth -= amount;
//        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

//        if (healthBar != null)
//            healthBar.value = currentHealth;

//        if (damageSound != null)
//            damageSound.Play();

//        if (damageOverlay != null)
//            StartCoroutine(FlashDamage());

//        if (currentHealth <= 0)
//            Die();
//    }

//    // **Updated Heal Method**
//    public void Heal(int amount)
//    {
//        currentHealth += amount;
//        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

//        if (healthBar != null)
//            StartCoroutine(UpdateHealthBarSmooth(currentHealth)); // <- call coroutine
//    }

//    // **Coroutine for smooth slider**
//    private System.Collections.IEnumerator UpdateHealthBarSmooth(float targetValue)
//    {
//        float elapsed = 0f;
//        float duration = 0.3f; // how fast slider fills
//        float startValue = healthBar.value;

//        while (elapsed < duration)
//        {
//            elapsed += Time.deltaTime;
//            healthBar.value = Mathf.Lerp(startValue, targetValue, elapsed / duration);
//            yield return null;
//        }

//        healthBar.value = targetValue; // ensure exact final value
//    }

//    private void Die()
//    {
//        Debug.Log("Player Died!");
//        // Add death logic here
//    }

//    private System.Collections.IEnumerator FlashDamage()
//    {
//        damageOverlay.color = new Color(1, 0, 0, 0.5f);
//        yield return new WaitForSeconds(damageFlashDuration);
//        damageOverlay.color = Color.clear;
//    }
//}


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public Slider healthBar;

    [Header("Damage Feedback")]
    public AudioSource damageSound;
    public float damageFlashDuration = 0.2f;
    public Image damageOverlay;

    [Header("Death UI")]
    public GameObject deadCanvas; // Dead screen canvas
    public Button tryAgainButton;
    public Button mainMenuButton;

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (deadCanvas != null)
            deadCanvas.SetActive(false); // Hide dead canvas at start

        // Assign button listeners
        if (tryAgainButton != null)
            tryAgainButton.onClick.AddListener(RestartLevel);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // -----------------------------
    // DAMAGE / HEAL METHODS
    // -----------------------------
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            StartCoroutine(UpdateHealthBarSmooth(currentHealth));

        if (damageSound != null)
            damageSound.Play();

        if (damageOverlay != null)
            StartCoroutine(FlashDamage());

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            StartCoroutine(UpdateHealthBarSmooth(currentHealth));
    }

    // Smooth health bar
    private System.Collections.IEnumerator UpdateHealthBarSmooth(float targetValue)
    {
        float elapsed = 0f;
        float duration = 0.3f;
        float startValue = healthBar.value;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // Use unscaled so it works even if timeScale = 0
            healthBar.value = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            yield return null;
        }

        healthBar.value = targetValue;
    }

    // Red damage flash
    private System.Collections.IEnumerator FlashDamage()
    {
        damageOverlay.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(damageFlashDuration);
        damageOverlay.color = Color.clear;
    }

    // -----------------------------
    // PLAYER DEATH
    // -----------------------------
    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Player Died!");

        // Show Dead Canvas
        if (deadCanvas != null)
            deadCanvas.SetActive(true);

        // -----------------------------
        // SAFE GENERIC MOVEMENT DISABLE
        // -----------------------------
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var s in scripts)
        {
            if (s != this) // Don't disable this health script
                s.enabled = false;
        }

        // Disable CharacterController if exists
        var controller = GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;

        // Optional: stop enemies or other gameplay scripts
        // var enemies = FindObjectsOfType<EnemyAI>();
        // foreach(var e in enemies) e.enabled = false;
    }

    // -----------------------------
    // BUTTON ACTIONS
    // -----------------------------
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume time
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reload current level
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("Main Menu"); // Replace with your main menu scene name
    }
}
