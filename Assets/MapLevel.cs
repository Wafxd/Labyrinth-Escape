using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLevel : MonoBehaviour
{
    // Fungsi untuk memulai level berdasarkan nama scene level yang dipilih
    public void PlayGame(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName); // Memuat scene sesuai dengan nama level
    }
    
    // Fungsi untuk kembali ke menu utama
    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu"); // Memuat scene Main Menu
    }
}
