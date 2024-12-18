using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMapLevel()
    {
        SceneManager.LoadSceneAsync("Map Level"); 
    }


    public void Playtutor(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName); 
    }
}
