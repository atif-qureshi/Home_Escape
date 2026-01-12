using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    // Call this method to apply damage
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (damageSound != null)
            damageSound.Play();

        if (damageOverlay != null)
            StartCoroutine(FlashDamage());

        if (currentHealth <= 0)
            Die();
    }

    // **Updated Heal Method**
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            StartCoroutine(UpdateHealthBarSmooth(currentHealth)); // <- call coroutine
    }

    // **Coroutine for smooth slider**
    private System.Collections.IEnumerator UpdateHealthBarSmooth(float targetValue)
    {
        float elapsed = 0f;
        float duration = 0.3f; // how fast slider fills
        float startValue = healthBar.value;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            healthBar.value = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            yield return null;
        }

        healthBar.value = targetValue; // ensure exact final value
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        // Add death logic here
    }

    private System.Collections.IEnumerator FlashDamage()
    {
        damageOverlay.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(damageFlashDuration);
        damageOverlay.color = Color.clear;
    }
}
