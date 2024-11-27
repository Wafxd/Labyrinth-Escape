using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 2;
    private Rigidbody2D characterBody;
    private Vector2 velocity;
    private Vector2 inputMovement;

    public AudioSource footstepAudio; // Drag and drop AudioSource untuk langkah kaki
    public AudioManager audioManager; // Reference ke AudioManager untuk mengatur volume SFX

    public PlayerHealth playerHealth; // Reference ke script PlayerHealth

    void Start()
    {
        velocity = new Vector2(speed, speed);
        characterBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputMovement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        // Periksa jika player bergerak dan mainkan suara langkah kaki
        if (inputMovement.magnitude > 0)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play(); // Mainkan suara langkah kaki
            }
        }
        else
        {
            footstepAudio.Stop(); // Hentikan suara langkah kaki
        }

        // Sinkronkan volume langkah kaki dengan volume SFX dari AudioManager
        if (audioManager != null && footstepAudio != null)
        {
            footstepAudio.volume = audioManager.GetSFXVolume();
        }
    }

    private void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }

    // Contoh interaksi untuk mengurangi health
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth.TakeDamage(1);
        }
    }
}
