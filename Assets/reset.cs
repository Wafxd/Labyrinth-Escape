using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    // Fungsi untuk mereset level ke level 1
    public void ResetLevelProgress()
    {
        PlayerPrefs.SetInt("levelat", 1); // Reset level ke 1
        PlayerPrefs.Save(); // Pastikan perubahan disimpan

        // Tampilkan log untuk konfirmasi reset
        Debug.Log("Level progress telah di-reset ke level 1.");
        
        // Anda bisa mengarahkan pemain ke scene utama atau scene level 1 jika diperlukan
        // Contoh: SceneManager.LoadScene("Level1"); 
    }
}
