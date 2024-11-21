using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime;
    public float pickupRange = 2f;  // Jarak untuk mengambil kunci
    private bool canPickUp = false; // Menandakan apakah kunci bisa diambil

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp)
        {
            Vector3 offset = new Vector3(-0.5f, 0.5f, 0);
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref vel, smoothTime);

            // Drop key jika tombol "G" ditekan
            if (Input.GetKeyDown(KeyCode.G))
            {
                DropKey();
            }
        }
        else
        {
            // Memeriksa jika player cukup dekat untuk mengambil kunci
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer <= pickupRange)
            {
                canPickUp = true;
            }
            else
            {
                canPickUp = false;
            }

            // Ambil kunci jika tombol "E" ditekan
            if (canPickUp && Input.GetKeyDown(KeyCode.E))
            {
                PickUpKey();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPickUp = false;
        }
    }

    // Fungsi untuk mengambil kunci
    void PickUpKey()
    {
        isPickedUp = true;
        canPickUp = false;
    }

    // Fungsi untuk menjatuhkan kunci (sekarang hanya di tempat player)
    void DropKey()
    {
        isPickedUp = false;

        // Pindahkan kunci ke posisi player (di tempat yang sama)
        transform.position = player.transform.position;
    }
}
