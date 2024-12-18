using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 2;
    private Rigidbody2D characterBody;
    private Vector2 velocity;
    private Vector2 inputMovement;

    public AudioSource footstepAudio; // Audio untuk langkah kaki
    public AudioManager audioManager; // Referensi AudioManager untuk mengatur volume SFX

    public PlayerHealth playerHealth; // Referensi ke PlayerHealth

    void Start()
    {
        velocity = new Vector2(speed, speed);
        characterBody = GetComponent<Rigidbody2D>();

        // Pastikan playerHealth tidak null
        if (playerHealth == null)
        {
            playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth not found! Attach the PlayerHealth script to the Player object.");
            }
        }

        // Log error jika AudioSource atau AudioManager tidak diatur
        if (footstepAudio == null)
        {
            Debug.LogWarning("Footstep AudioSource is not assigned. No footstep sound will play.");
        }

        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager is not assigned. SFX volume will not be synchronized.");
        }
    }

    void Update()
    {
        inputMovement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        if (footstepAudio != null)
        {
            if (inputMovement.magnitude > 0 && !footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
            else if (inputMovement.magnitude == 0 && footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }

            if (audioManager != null)
            {
                footstepAudio.volume = audioManager.GetSFXVolume();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            else
            {
                Debug.LogError("PlayerHealth is null! Cannot reduce health.");
            }
        }
    }
}
