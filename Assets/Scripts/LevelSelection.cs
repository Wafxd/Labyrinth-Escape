using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons; // Array to store all level buttons
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelat", 2);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 2 > levelAt)
                lvlButtons[i].interactable = false;
        }
    }

    // Update is called once per frame
    
}
