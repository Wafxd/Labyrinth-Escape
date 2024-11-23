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

    // Tambahkan referensi untuk AudioManager dan AudioSource
    public AudioManager audioManager;
    public AudioSource pickUpSFX; // Drag and drop AudioSource untuk suara mengambil kunci
    public AudioSource dropSFX; // Drag and drop AudioSource untuk suara menjatuhkan kunci

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

        // Sinkronkan volume efek suara dengan SFX volume dari AudioManager
        if (audioManager != null)
        {
            if (pickUpSFX != null) pickUpSFX.volume = audioManager.GetSFXVolume();
            if (dropSFX != null) dropSFX.volume = audioManager.GetSFXVolume();
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

        // Mainkan suara mengambil kunci
        if (pickUpSFX != null)
        {
            pickUpSFX.Play();
        }
    }

    // Fungsi untuk menjatuhkan kunci
    void DropKey()
    {
        isPickedUp = false;

        // Pindahkan kunci ke posisi player
        transform.position = player.transform.position;

        // Mainkan suara menjatuhkan kunci
        if (dropSFX != null)
        {
            dropSFX.Play();
        }
    }
}
