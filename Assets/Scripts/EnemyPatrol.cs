using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform Player; // Referensi ke posisi pemain
    public float detectionRange = 5f; // Jarak deteksi untuk mengejar pemain
    public Transform[] patrolPoints; // Titik-titik untuk patroli
    private NavMeshAgent agent;
    private int currentPatrolIndex = 0; // Indeks titik patroli saat ini
    private bool isChasing = false; // Apakah musuh sedang mengejar pemain?

    private AudioManager audioManager; // Referensi ke AudioManager
    private PlayerHealth playerHealth; // Referensi ke script PlayerHealth

    private float damageInterval = 2f; // Interval waktu untuk memberikan damage (dalam detik)
    private float damageTimer = 0f; // Timer untuk menghitung waktu

    private bool isPlayerInRange = false; // Apakah player sudah dalam jangkauan musuh?

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Nonaktifkan rotasi otomatis (jika 2D)
        agent.updateUpAxis = false; // Nonaktifkan rotasi sumbu atas (jika 2D)

        // Referensi ke AudioManager di scene menggunakan API terbaru
        audioManager = Object.FindFirstObjectByType<AudioManager>(); 

        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found in the scene. Make sure it is present.");
        }

        // Referensi ke script PlayerHealth
        if (Player != null)
        {
            playerHealth = Player.GetComponent<PlayerHealth>();
        }

        if (patrolPoints.Length > 0)
        {
            // Set destinasi awal ke titik patroli pertama
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            agent.SetDestination(Player.position);
            if (audioManager != null)
            {
                audioManager.PlayTensionMusic(true);
            }
        }
        else
        {
            isChasing = false;
            Patrol();
            if (audioManager != null)
            {
                audioManager.PlayTensionMusic(false);
            }
        }

        if (isPlayerInRange)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1); 
                    damageTimer = 0f; 
                }
            }
        }
    }

    void Patrol()
    {
        if (!isChasing && patrolPoints.Length > 0)
        {
            // Jika sudah dekat dengan titik patroli saat ini, pindah ke titik berikutnya
            if (agent.remainingDistance < 0.5f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }
    }

    // Ketika musuh mulai bersentuhan dengan player
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Ketika player bersentuhan dengan musuh, set flag isPlayerInRange ke true
            isPlayerInRange = true;
            damageTimer = 0f; // Reset timer ketika player masuk ke jangkauan
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (isPlayerInRange)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= damageInterval)
                {
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(1); 
                        damageTimer = 0f; 
                    }
                }
            }
        }
    }

    // Ketika player keluar dari jangkauan musuh
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Set flag isPlayerInRange ke false jika player keluar dari jangkauan
            isPlayerInRange = false;
            damageTimer = 0f; // Reset timer jika player keluar dari jangkauan
        }
    }
}
