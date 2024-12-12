using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;  // Pastikan ini ada untuk menggunakan UI elements seperti Button
using UnityEngine.SceneManagement;

public class EndOfVideo : MonoBehaviour
{
    public GameObject EndTutorialPanel;    // Panel untuk tombol Exit, Replay, dan Next
    public Button exitButton;              // Tombol Exit
    public Button replayButton;            // Tombol Replay
    public Button nextButton;              // Tombol Next Tutorial
    public VideoPlayer videoPlayer;        // Referensi ke VideoPlayer

    void Start()
    {
        // Daftarkan event untuk ketika video selesai
        videoPlayer.loopPointReached += OnVideoFinished;

        // Pastikan panel akhir tutorial (EndTutorialPanel) disembunyikan saat awal
        EndTutorialPanel.SetActive(false);
    }

    // Event yang dipanggil ketika video selesai
    void OnVideoFinished(VideoPlayer vp)
    {
        // Menampilkan tombol End Tutorial setelah video selesai
        EndTutorialPanel.SetActive(true);
        Time.timeScale = 0;  // Pastikan waktu terhenti saat video selesai
    }

    // Tombol Next Tutorial
    public void NextTutorial()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // Ambil index scene saat ini
        int nextSceneIndex = currentSceneIndex + 1;  // Ambil scene berikutnya (index + 1)

        // Cek apakah scene berikutnya ada (pastikan tidak keluar dari urutan scene yang ada)
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);  // Muat scene dengan index + 1
        }
        else
        {
            Debug.Log("No next scene available.");
            // Bisa menambahkan logika lain jika tidak ada scene berikutnya
        }
    }

    // Tombol Replay (untuk mengulang video)
    public void Replay()
    {
        Time.timeScale = 1; // Pastikan waktu kembali normal sebelum restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Muat ulang scene saat ini
    }

    // Tombol Exit (untuk keluar ke Main Menu atau scene lainnya)
    public void ExitToMainMenu()
    {
        Time.timeScale = 1; // Pastikan waktu kembali normal sebelum berpindah scene
        SceneManager.LoadScene("Menu Tutor"); // Ganti dengan nama scene Main Menu
    }
}
