using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;  

public class videopause : MonoBehaviour
{
    public GameObject PausePanel;
    public VideoPlayer videoPlayer;  

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0; 
        if (videoPlayer.isPlaying) 
        {
            videoPlayer.Pause();
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1; 
        if (!videoPlayer.isPlaying) 
        {
            videoPlayer.Play();
        }
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
