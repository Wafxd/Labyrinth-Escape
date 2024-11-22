using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons; // Array untuk semua tombol level

    void Start()
    {
        // Ambil level yang terbuka dari PlayerPrefs
        int levelAt = PlayerPrefs.GetInt("levelat", 1); // Default level yang terbuka adalah level 1

        // Mengatur tombol level berdasarkan progres level yang terbuka
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = (i + 1 <= levelAt); // Hanya buka hingga level 'levelAt'
        }
    }

}
