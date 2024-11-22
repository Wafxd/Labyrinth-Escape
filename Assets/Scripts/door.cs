using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool locked;
    private Animator anim;

    [SerializeField] GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
        locked = true;
    }

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (!locked && distance < 0.5f)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Perbarui progres level sebelum memuat level berikutnya
            UpdateLevelProgress(currentSceneIndex);

            if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1) // Cek apakah belum di level terakhir
            {
                SceneManager.LoadScene(currentSceneIndex + 1); // Load scene berikutnya
            }
            else
            {
                // Jika level terakhir, reset level kecuali level 1
                // ResetLevels();
                SceneManager.LoadScene("Main Menu"); // Ganti dengan nama scene main menu
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            anim.SetTrigger("Open");
            locked = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            anim.SetTrigger("Closed");
            locked = true;
        }
    }

    void ResetLevels()
    {
        int totalLevels = SceneManager.sceneCountInBuildSettings;

        // Reset semua level ke status terkunci kecuali level 1 (indeks ke-2)
        for (int i = 2; i < totalLevels; i++) // Mulai dari level 2
        {
            PlayerPrefs.SetInt($"Level_{i}_Unlocked", 0); // 0 berarti terkunci
        }

        // Buka kembali level 1
        PlayerPrefs.SetInt("levelat", 1); // Reset progres ke level 1
        PlayerPrefs.SetInt("Level_2_Unlocked", 1); // Pastikan level 1 tetap terbuka
    }

    void UpdateLevelProgress(int currentSceneIndex)
    {
        // Ambil progres level saat ini
        int levelAt = PlayerPrefs.GetInt("levelat", 1);

        // Hitung indeks level berdasarkan urutan (level 1 = indeks 2 di Build Settings)
        int currentLevel = currentSceneIndex - 1; // Karena level 1 = indeks 2, level 2 = indeks 3, dst.

        // Buka level berikutnya hanya jika progres saat ini lebih kecil dari level aktif
        if (currentLevel + 1 > levelAt)
        {
            PlayerPrefs.SetInt("levelat", currentLevel + 1); // Perbarui ke level berikutnya
            Debug.Log($"Progres level diperbarui: {currentLevel + 1}");
        }
    }

}
