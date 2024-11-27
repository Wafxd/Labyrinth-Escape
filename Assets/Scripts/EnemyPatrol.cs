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
            // Mulai mengejar pemain
            isChasing = true;
            agent.SetDestination(Player.position);

            // Mainkan musik tegang
            if (audioManager != null)
            {
                audioManager.PlayTensionMusic(true);
            }
        }
        else
        {
            // Kembali ke patroli jika pemain di luar jangkauan
            isChasing = false;
            Patrol();

            // Hentikan musik tegang
            if (audioManager != null)
            {
                audioManager.PlayTensionMusic(false);
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

    // Tambahkan interaksi saat menyentuh player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(2); // Kurangi health player sebanyak 2
            }
        }
    }
}
