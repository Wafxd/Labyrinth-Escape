using UnityEngine;
using UnityEngine.SceneManagement; // Untuk mereload scene
using UnityEngine.UI; // Untuk mengakses UI elements

public class DeathMenuController : MonoBehaviour
{
    public GameObject deathMenuCanvas; // Referensi ke Canvas death menu
    public Button restartButton; // Referensi ke tombol restart
    public Button exitButton; // Referensi ke tombol exit
    public GameObject pauseButton; // Referensi ke tombol pause

    void Start()
    {
        // Menyembunyikan death menu saat game dimulai
        deathMenuCanvas.SetActive(false);

        // Menambahkan listener ke tombol
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void ShowDeathMenu()
    {
        // Menampilkan death menu
        deathMenuCanvas.SetActive(true);
        
        // Menyembunyikan tombol pause
        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }

        // Menghentikan waktu
        Time.timeScale = 0;
    }

    private void RestartGame()
    {
        // Mereload scene saat ini
        Time.timeScale = 1; // Pastikan waktu kembali normal sebelum restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
        // Keluar dari game
        Time.timeScale = 1; // Pastikan waktu kembali normal sebelum berpindah scene
        Application.Quit();
    }
}
