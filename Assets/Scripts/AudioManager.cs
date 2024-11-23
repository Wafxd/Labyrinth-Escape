using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider; // Drag and drop slider di Inspector untuk musik
    public Slider sfxSlider; // Drag and drop slider di Inspector untuk SFX
    public AudioSource mainMenuAudio; // Drag and drop AudioSource di Inspector untuk musik
    public AudioSource buttonSFX; // Drag and drop AudioSource di Inspector untuk SFX

    void Start()
    {
        // Ambil nilai volume musik dari PlayerPrefs, default 1.0 jika belum ada
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        if (volumeSlider != null)
        {
            volumeSlider.value = savedMusicVolume; // Set slider musik sesuai dengan nilai tersimpan
            volumeSlider.onValueChanged.AddListener(OnMusicVolumeChange); // Tambahkan listener
        }

        if (mainMenuAudio != null)
        {
            mainMenuAudio.volume = savedMusicVolume; // Atur volume AudioSource untuk musik
        }

        // Ambil nilai volume SFX dari PlayerPrefs, default 1.0 jika belum ada
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFXVolume; // Set slider SFX sesuai dengan nilai tersimpan
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange); // Tambahkan listener
        }

        if (buttonSFX != null)
        {
            buttonSFX.volume = savedSFXVolume; // Atur volume AudioSource untuk SFX
        }
    }

    public void OnMusicVolumeChange(float volume)
    {
        if (mainMenuAudio != null)
        {
            mainMenuAudio.volume = volume; // Ubah volume AudioSource untuk musik
        }

        // Simpan nilai volume musik ke PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void OnSFXVolumeChange(float volume)
    {
        if (buttonSFX != null)
        {
            buttonSFX.volume = volume; // Ubah volume AudioSource untuk SFX
        }

        // Simpan nilai volume SFX ke PlayerPrefs
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}
