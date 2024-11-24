using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider; // Slider untuk musik
    public Slider sfxSlider; // Slider untuk SFX
    public AudioSource mainMenuAudio; // AudioSource untuk musik di menu utama / level
    public AudioSource buttonSFX; // AudioSource untuk efek suara tombol
    public AudioSource tensionMusic; // Musik tegang saat dikejar musuh
    public float fadeSpeed = 1f; // Kecepatan transisi volume

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

        if (tensionMusic != null)
        {
            tensionMusic.volume = 0f; // Pastikan musik tegang mulai dengan volume 0
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

    // Fungsi untuk transisi antara musik main menu / level dan musik tegang
    public void PlayTensionMusic(bool isTensionActive)
    {
        if (mainMenuAudio == null || tensionMusic == null) return;

        if (isTensionActive)
        {
            // Fade in musik tegang, fade out main menu / level musik
            tensionMusic.volume = Mathf.Lerp(tensionMusic.volume, volumeSlider.value, Time.deltaTime * fadeSpeed);
            mainMenuAudio.volume = Mathf.Lerp(mainMenuAudio.volume, 0f, Time.deltaTime * fadeSpeed);
        }
        else
        {
            // Fade out musik tegang, fade in main menu / level musik
            tensionMusic.volume = Mathf.Lerp(tensionMusic.volume, 0f, Time.deltaTime * fadeSpeed);
            mainMenuAudio.volume = Mathf.Lerp(mainMenuAudio.volume, volumeSlider.value, Time.deltaTime * fadeSpeed);
        }
    }
}
