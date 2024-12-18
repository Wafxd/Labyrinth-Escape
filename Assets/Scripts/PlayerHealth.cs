using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar; // Drag and drop HealthBar dari Editor
    public int maxHealth = 5;
    private int currentHealth;

    // Referensi ke DeathMenuController
    public DeathMenuController deathMenuController;
    private bool isPlayerAlive = true; // Variabel untuk memeriksa apakah player masih hidup

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isPlayerAlive) 
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void RestoreHealth(int amount)
    {
        if (isPlayerAlive) 
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
            healthBar.SetHealth(currentHealth); 
        }
    }

    // Tambahkan fungsi untuk mendapatkan darah saat ini
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        isPlayerAlive = false; 
        if (deathMenuController != null)
        {
            deathMenuController.ShowDeathMenu(); 
        }
        else
        {
            Debug.LogError("DeathMenuController not assigned!"); // Debugging
        }
    }
}
