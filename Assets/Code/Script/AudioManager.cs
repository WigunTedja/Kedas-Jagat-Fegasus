using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.SpriteMask;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    // audioSource[0] for Music, audioSource[1] for SFX
    public AudioSource[] audioSource;

    public AudioSource walkSource;

    [Header("Audio Clips")]
    public AudioClip mainMenuMusic; 
    public AudioClip collectSFX;    
    public AudioClip winSFX;        
    public AudioClip walkSFX;       
    private float SFXVolume;
    private float MusicVolume;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f); 
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (audioSource.Length > 0 && audioSource[0] != null)
        {
            audioSource[0].volume = MusicVolume; 
        }

        if (audioSource.Length > 1 && audioSource[1] != null)
        {
            audioSource[1].volume = SFXVolume; 
        }

        if (walkSource != null)
        {
            walkSource.volume = SFXVolume; 
        }
    }

    public void PlayMainMenuMusic()
    {
        if (audioSource[0].clip == mainMenuMusic) return; 

        audioSource[0].clip = mainMenuMusic;
        audioSource[0].volume = MusicVolume;
        audioSource[0].loop = true;
        audioSource[0].Play();
    }

    public void PlayCollectSFX()
    {
        audioSource[1].PlayOneShot(collectSFX, SFXVolume);
    }

    public void PlayWinSFX()
    {
        audioSource[1].PlayOneShot(winSFX, SFXVolume);
    }

    public void StartWalking()
    {
        if (!walkSource.isPlaying)
        {
            walkSource.clip = walkSFX;
            walkSource.loop = true;
            walkSource.volume = SFXVolume;
            walkSource.Play();
        }
    }

    public void StopWalking()
    {
        if (walkSource.isPlaying)
        {
            walkSource.Stop();
        }
    }

    public void UpdateMusicVolume(float volume)
    {
        MusicVolume = volume;
        audioSource[0].volume = MusicVolume;
    }

    public void UpdateSFXVolume(float volume)
    {
        SFXVolume = volume;
        if (walkSource != null)
        {
            walkSource.volume = SFXVolume;
        }
    }

}