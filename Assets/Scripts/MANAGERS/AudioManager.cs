using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    [Header("MENU AUDIO FX")]
    public AudioClip[] menuFX;

    [Header("CHARACTER FX")]
    public AudioClip[] characterFX;
    public AudioClip[] soundFX;

    [Header("LEVEL/BONUS FX")]
    public AudioClip[] mainLevelFX;
    public AudioClip[] levelBonusOneFX;
    public AudioClip[] levelBonusTwoFX;
    public AudioClip[] levelBonusThreeFX;

    [Header("MUSIC FX")]
    public AudioClip[] musicFX;

    [Header("AUDIO SOURCES")]
    public AudioSource menuFXSource;
    public AudioSource characterFXSource;
    public AudioSource movementFXSource;
    public AudioSource soundFXSource;
    public AudioSource mainLevelFXSource;
    public AudioSource bonusOneFXSource;
    public AudioSource bonusTwoFXSource;
    public AudioSource bonusThreeFXSource;
    public AudioSource musicSource;

    [Header("VOLUME SETTINGS")]
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
        mainLevelFXSource = GameObject.Find("MainLevelFX").GetComponentInChildren<AudioSource>();
        bonusOneFXSource = GameObject.Find("BonusOneFX").GetComponentInChildren<AudioSource>();
        bonusTwoFXSource = GameObject.Find("BonusTwoFX").GetComponentInChildren<AudioSource>();
        bonusThreeFXSource = GameObject.Find("BonusThreeFX").GetComponentInChildren<AudioSource>();
        musicSource = GameObject.Find("Music").GetComponentInChildren<AudioSource>();

        Debug.Log("AUDIO MANAGERISSA STARTVALUESIT POISSA PÄÄLTÄ - TESTIN VUOKSI, LAITA PÄÄLLE KN OIKEA PELI");
        //StartValues();

    }

    public void Start()
    {
        Debug.Log("AUDIO MANAGERISSA startista awakeen kun testATTU KAKKOSKENTTÄ");
        StartValues();// TÄMÄ TURHA VAIN TESTIÄ VARTEN
    }
    public void StartValues()
    {
        //WHEN GAME IS STARTED VALUES HERE
        masterVolumeValue = 1;
        effectsVolumeValue = 1;
        musicVolumeValue = 1;
        DataManager.instance.SetLevelAudio(masterVolumeValue, effectsVolumeValue, musicVolumeValue);
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
    public void PlayMainLevelFX(int tracknumber)
    {
        mainLevelFXSource.clip = mainLevelFX[tracknumber];
        mainLevelFXSource.Play();
    }
    public void PlayBonusOneFX(int tracknumber)
    {
        bonusOneFXSource.clip = levelBonusOneFX[tracknumber];
        bonusOneFXSource.Play();
    }
    public void PlayBonusTwoFX(int tracknumber)
    {
        bonusTwoFXSource.clip = levelBonusTwoFX[tracknumber];
        bonusTwoFXSource.Play();
    }
    public void PlayBonusThreeFX(int tracknumber)
    {
        bonusThreeFXSource.clip = levelBonusThreeFX[tracknumber];
        bonusThreeFXSource.Play();
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


}
