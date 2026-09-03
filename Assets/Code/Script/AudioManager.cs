using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    private AudioSource audioSource;

    private float SFXVolume;
    private float MusicVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
