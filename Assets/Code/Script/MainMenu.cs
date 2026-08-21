using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const string IntroFlag = "HasSeenIntro";
    public GameObject settingPanel;
    public Slider sfxSlider;       
    public Slider musicSlider;     
    public Toggle mobileUIToggle;


    private void Start()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }

        LoadSettings();
    }
    public void OnPlayButtonPressed()
    {
        // Check if the player has seen the intro. We use 0 as the default if the key doesn't exist.
        if (PlayerPrefs.GetInt(IntroFlag, 0) == 0)
        {
            SceneManager.LoadScene("Game_Intro");
        }
        else
        {
            SceneManager.LoadScene("Level_Panel");
        }
    }

    public void SettingButton()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }
    }

    public void CloseSettingButton()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }

    public void OnSFXVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        // TODO: Hubungkan dengan AudioMixer atau AudioSource untuk efek suara di sini
        Debug.Log("SFX Volume diubah ke: " + volume);
    }

    public void OnMusicVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        // TODO: Hubungkan dengan AudioMixer atau AudioSource untuk musik di sini
        Debug.Log("Music Volume diubah ke: " + volume);
    }

    public void OnMobileUIToggled(bool isMobile)
    {
        // Karena PlayerPrefs tidak mendukung tipe bool, kita gunakan 1 (true) dan 0 (false)
        PlayerPrefs.SetInt("MobileUI", isMobile ? 1 : 0);
        // TODO: Tambahkan logika untuk mengganti layout/kontrol menjadi mobile di sini
        Debug.Log("Mobile UI aktif: " + isMobile);
    }

    private void LoadSettings()
    {
        if (sfxSlider != null)
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (mobileUIToggle != null)
            mobileUIToggle.isOn = PlayerPrefs.GetInt("MobileUI", 1) == 1; // Default true (1)
    }

    public void QuitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
