using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; // VideoPlayer untuk memutar video tamat
    [SerializeField] private AudioSource audioSource; // AudioSource untuk memutar suara dari video

    void Start()
    {
        // Pastikan VideoPlayer dan AudioSource sudah di-set pada inspector
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer tidak ditemukan di scene ini.");
            return;
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource tidak ditemukan di scene ini.");
            return;
        }

        // Pastikan audio source diset untuk mendengar audio dari video
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource); // Menghubungkan audio dari VideoPlayer ke AudioSource

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
