using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0; // Pause waktu
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1; // Lanjutkan waktu
    }

    public void Restart()
    {
        Time.timeScale = 1; // Pastikan waktu kembali normal sebelum restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Muat ulang scene saat ini
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1; // Pastikan waktu kembali normal sebelum berpindah scene
        SceneManager.LoadScene("Map Level"); // Muat scene Map Level
    }
}
