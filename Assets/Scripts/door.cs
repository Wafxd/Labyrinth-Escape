using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Door : MonoBehaviour
{
    public bool locked;
    private Animator anim;

    [SerializeField] GameObject player;
    [SerializeField] VideoPlayer videoPlayer;  // VideoPlayer untuk memutar video tamat
    [SerializeField] PlayerMovement playerMovement;  // Referensi ke script PlayerMovement

    public AudioSource openDoorSFX;  // Suara pintu terbuka
    public AudioSource closeDoorSFX; // Suara pintu tertutup

    void Start()
    {
        anim = GetComponent<Animator>();
        locked = true;

        // Pastikan videoPlayer di-set, jika tidak maka bisa muncul error
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer belum di-assign pada inspector.");
        }

        // Cek apakah player memiliki PlayerMovement
        if (playerMovement == null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement script tidak ditemukan pada player.");
            }
        }
    }

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (!locked && distance < 0.5f)
        {
            // Perbarui progres level sebelum memuat level berikutnya
            UpdateLevelProgress(currentSceneIndex);

            if (currentSceneIndex == 11)  // Cek jika pemain sudah di level 10
            {
                PlayEndVideo(); // Mainkan video tamat
            }
            else if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1) // Cek apakah belum di level terakhir
            {
                SceneManager.LoadScene(currentSceneIndex + 1); // Load scene berikutnya
            }
            else
            {
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

            // Mainkan suara pintu terbuka
            if (openDoorSFX != null)
            {
                openDoorSFX.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            anim.SetTrigger("Closed");
            locked = true;

            // Mainkan suara pintu tertutup
            if (closeDoorSFX != null)
            {
                closeDoorSFX.Play();
            }
        }
    }

    void UpdateLevelProgress(int currentSceneIndex)
    {
        int levelAt = PlayerPrefs.GetInt("levelat", 1);
        int currentLevel = currentSceneIndex - 1; // Menyesuaikan urutan level

        if (currentLevel + 1 > levelAt)
        {
            PlayerPrefs.SetInt("levelat", currentLevel + 1); // Update progres level
            Debug.Log($"Progres level diperbarui: {currentLevel + 1}");
        }
    }

    void PlayEndVideo()
    {
        // Pastikan videoPlayer sudah ada dan video sudah di-assign
        if (videoPlayer != null)
        {
            videoPlayer.Play(); // Mainkan video tamat

            // Nonaktifkan player movement dan interaksi saat video diputar
            DisablePlayerMovement();

            // Setelah video selesai, bisa menampilkan Main Menu atau tindakan lain
            videoPlayer.loopPointReached += EndOfVideo; // Memanggil method ketika video selesai
        }
    }

    void EndOfVideo(VideoPlayer vp)
    {
        // Setelah video selesai, kembali ke Main Menu atau scene lain
        SceneManager.LoadScene("Main Menu"); // Ganti dengan nama scene Main Menu yang sesuai

        // Mengaktifkan kembali kontrol player setelah video selesai
        EnablePlayerMovement();
    }

    void DisablePlayerMovement()
    {
        // Menonaktifkan kontrol pergerakan player
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Nonaktifkan collider player agar tidak bisa berinteraksi
        player.GetComponent<Collider2D>().enabled = false;
    }

    void EnablePlayerMovement()
    {
        // Mengaktifkan kembali kontrol pergerakan player
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Mengaktifkan kembali collider player
        player.GetComponent<Collider2D>().enabled = true;
    }
}
