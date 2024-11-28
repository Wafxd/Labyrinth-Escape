using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;  // Tambahkan untuk mengakses VideoPlayer

public class videopause : MonoBehaviour
{
    public GameObject PausePanel;
    public VideoPlayer videoPlayer;  // Referensi ke VideoPlayer yang memutar video

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0; // Pause waktu
        if (videoPlayer.isPlaying) // Pastikan video dipause jika sedang diputar
        {
            videoPlayer.Pause();
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1; // Lanjutkan waktu
        if (!videoPlayer.isPlaying) // Pastikan video dilanjutkan jika tidak sedang diputar
        {
            videoPlayer.Play();
        }
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
