using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    public AudioClip[] menuFX;
    public AudioClip[] characterFX;
    public AudioClip[] soundFX;
    public AudioClip[] musicFX;

    public AudioSource menuFXSource;
    public AudioSource characterFXSource;
    public AudioSource soundFXSource;
    public AudioSource musicSource;

    private void Awake()
    {
        if (instance != null) { Debug.Log("trying to create another!"); }
        else { instance = this; DontDestroyOnLoad(instance);}

        menuFXSource = GameObject.Find("MenuFX").GetComponent<AudioSource>();
        characterFXSource = GameObject.Find("CharacterFX").GetComponent<AudioSource>();
        soundFXSource = GameObject.Find("SoundFX").GetComponent<AudioSource>();
        musicSource = GameObject.Find("Music").GetComponentInChildren<AudioSource>();
    }

    public void PlayMenuFX(int tracknumber)
    {
        menuFXSource.clip = menuFX[tracknumber];
        menuFXSource.Play();
    }
    public void Character(int tracknumber)
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
}
