using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    public AudioClip[] menuFX;
    public AudioClip[] characterFX;
    public AudioClip[] soundFX;
    public AudioClip[] musicFX;


    public AudioSource menuFXSource;
    public AudioSource characterFXSource;
    public AudioSource movementFXSource;
    public AudioSource soundFXSource;
    public AudioSource musicSource;

    public float masterVolumeValue;
    public float effectsVolumeValue;
    public float musicVolumeValue;

    public bool playFootsteps;

    private void Awake()
    {
        if (instance != null) { Debug.Log("trying to create another!"); }
        else { instance = this; DontDestroyOnLoad(instance);}

        menuFXSource = GameObject.Find("MenuFX").GetComponentInChildren<AudioSource>();
        characterFXSource = GameObject.Find("CharacterFX").GetComponentInChildren<AudioSource>();
        movementFXSource = GameObject.Find("MovementFX").GetComponentInChildren<AudioSource>();
        soundFXSource = GameObject.Find("SoundFX").GetComponentInChildren<AudioSource>();
        musicSource = GameObject.Find("Music").GetComponentInChildren<AudioSource>();
    }

    public void PlayMenuFX(int tracknumber)
    {
        menuFXSource.clip = menuFX[tracknumber];
        menuFXSource.Play();
    }
    public void PlayCharacterFX(int tracknumber)
    {
        characterFXSource.clip = characterFX[tracknumber];
        characterFXSource.Play();
    }
    public void PlaySoundFX(int tracknumber)
    {
        soundFXSource.clip = soundFX[tracknumber];
        soundFXSource.Play();
    }
    public void PlayMusicFX(int tracknumber)
    {
        musicSource.clip = musicFX[tracknumber];
        musicSource.Play();
    }
    public void ActivateFootstepsFX()
    {
        if (playFootsteps)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;

        }
        else
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
        }

    }

    public void SetStoredData()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeValue);
        PlayerPrefs.SetFloat("EffectVolume", effectsVolumeValue);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeValue);
    }


}
