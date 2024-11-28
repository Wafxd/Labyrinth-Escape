using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar; // Drag and drop HealthBar dari Editor
    public int maxHealth = 10;
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
        if (isPlayerAlive) // Cek jika player masih hidup sebelum mengambil damage
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
        if (isPlayerAlive) // Cek jika player masih hidup
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Pastikan darah tidak melebihi maksimum
            healthBar.SetHealth(currentHealth); // Update UI HealthBar
        }
    }

    // Tambahkan fungsi untuk mendapatkan darah saat ini
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        isPlayerAlive = false; // Menandakan player sudah mati
        if (deathMenuController != null)
        {
            deathMenuController.ShowDeathMenu(); // Tampilkan Death Menu
        }
        else
        {
            Debug.LogError("DeathMenuController not assigned!"); // Debugging
        }
    }
}
