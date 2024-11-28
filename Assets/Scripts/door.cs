using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool locked;
    private Animator anim;

    [SerializeField] GameObject player;
    [SerializeField] PlayerMovement playerMovement;  // Referensi ke script PlayerMovement

    public AudioSource openDoorSFX;  // Suara pintu terbuka
    public AudioSource closeDoorSFX; // Suara pintu tertutup

    void Start()
    {
        anim = GetComponent<Animator>();
        locked = true;

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

            // Jika pemain sudah di level 10 dan membuka pintu
            if (currentSceneIndex == 11)  // Cek jika pemain sudah di level 10
            {
                GoToTamatScene(); // Pergi ke scene tamat
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

    void GoToTamatScene()
    {
        // Jika sudah di level 10, langsung pergi ke scene Tamat
        SceneManager.LoadScene("TamatScene"); // Ganti dengan nama scene tamat yang sesuai
    }
}
