using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar; // Drag and drop HealthBar dari Editor
    public int maxHealth = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Tambahkan logika kematian pemain, seperti restart level atau game over.
    }
}
