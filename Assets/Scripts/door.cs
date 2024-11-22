using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool locked;
    private Animator anim;

    [SerializeField] GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
        locked = true;
    }

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (!locked && distance < 0.5f)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1) // Cek apakah belum di level terakhir
            {
                SceneManager.LoadScene(currentSceneIndex + 1); // Load scene berikutnya
            }
            else
            {
                // Jika level terakhir, load Main Menu
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            anim.SetTrigger("Closed");
            locked = true;
        }
    }
}
