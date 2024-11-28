using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; // VideoPlayer untuk memutar video tamat

    void Start()
    {
        // Pastikan VideoPlayer sudah di-set pada inspector
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer tidak ditemukan di scene ini.");
            return;
        }

        // Tambahkan listener untuk mendeteksi ketika video selesai diputar
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Callback ketika video selesai diputar
    void OnVideoEnd(VideoPlayer vp)
    {
        // Setelah video selesai, pergi ke Main Menu
        SceneManager.LoadScene("Main Menu"); // Ganti dengan nama scene Main Menu yang sesuai
    }
}
