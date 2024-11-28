using UnityEngine;

public class Heart : MonoBehaviour
{
    public int healthRestore = 2; // Jumlah darah yang ditambahkan ketika diambil

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Cek jika yang menyentuh adalah player
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Cek apakah darah player kurang dari maksimum
                if (playerHealth.GetCurrentHealth() < playerHealth.maxHealth)
                {
                    // Tambahkan darah player
                    playerHealth.RestoreHealth(healthRestore);
                    // Hancurkan (hilangkan) heart dari map
                    Destroy(gameObject);
                }
            }
        }
    }
}
