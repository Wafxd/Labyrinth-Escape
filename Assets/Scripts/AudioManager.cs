using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider; // Slider untuk musik
    public Slider sfxSlider; // Slider untuk SFX
    public AudioSource mainMenuAudio; // AudioSource untuk musik
    public AudioSource buttonSFX; // AudioSource untuk efek suara tombol

    private float sfxVolume = 1.0f; // Variabel untuk menyimpan volume SFX

    void Start()
    {
        // Ambil volume musik dari PlayerPrefs
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        if (volumeSlider != null)
        {
            volumeSlider.value = savedMusicVolume;
            volumeSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        }

        if (mainMenuAudio != null)
        {
            mainMenuAudio.volume = savedMusicVolume;
        }

        // Ambil volume SFX dari PlayerPrefs
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        sfxVolume = savedSFXVolume; // Set nilai awal SFX volume
        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFXVolume;
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);
        }

        if (buttonSFX != null)
        {
            buttonSFX.volume = savedSFXVolume;
        }
    }

    public void OnMusicVolumeChange(float volume)
    {
        if (mainMenuAudio != null)
        {
            mainMenuAudio.volume = volume;
        }

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void OnSFXVolumeChange(float volume)
    {
        sfxVolume = volume; // Update nilai volume SFX

        if (buttonSFX != null)
        {
            buttonSFX.volume = volume;
        }

        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    // Fungsi untuk mendapatkan volume SFX
    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}
