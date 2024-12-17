using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons; 

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelat", 1); 

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = (i + 1 <= levelAt); 
        }
    }

}
