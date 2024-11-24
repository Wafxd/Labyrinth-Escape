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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Nonaktifkan rotasi otomatis (jika 2D)
        agent.updateUpAxis = false; // Nonaktifkan rotasi sumbu atas (jika 2D)

        // Set destinasi awal ke titik patroli pertama
        if (patrolPoints.Length > 0)
        {
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
        }
        else
        {
            // Kembali ke patroli jika pemain di luar jangkauan
            isChasing = false;
            Patrol();
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
}
