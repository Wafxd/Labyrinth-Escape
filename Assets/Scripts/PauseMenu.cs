using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0; 
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1; 
    }
    public void Restart()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    public void ExitToMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Map Level"); 
    }
}
