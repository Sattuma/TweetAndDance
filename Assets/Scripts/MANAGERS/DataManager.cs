using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    private void Awake()
    {
        if(instance != null)
        { Destroy(gameObject); }
        else
        { instance = this; DontDestroyOnLoad(instance); }
    }


    //LEVEL TIMERS DATA
    public void SetLevelTimers(float timer1,float timer2,float timer3)
    {
        PlayerPrefs.SetFloat("Timer1_1", timer1);
        PlayerPrefs.SetFloat("Timer1_2", timer2);
        PlayerPrefs.SetFloat("Timer1_3", timer3);
    }
    public void GetLevelTimers()
    {
        GameModeManager.instance.timerLevel1 = PlayerPrefs.GetFloat("Timer1_1");
        GameModeManager.instance.timerLevel2 = PlayerPrefs.GetFloat("Timer1_2");
        GameModeManager.instance.timerLevel3 = PlayerPrefs.GetFloat("Timer1_3");
    }
    //-----------------

    //AUDIO LEVELS DATA
    public void SetLevelAudio(float value1,float value2,float value3)
    {
        PlayerPrefs.SetFloat("MasterVolume", value1);
        PlayerPrefs.SetFloat("EffectVolume", value2);
        PlayerPrefs.SetFloat("MusicVolume", value3);
    }
    public void GetLevelAudio()
    {
        AudioManager.instance.masterVolumeValue = PlayerPrefs.GetFloat("MasterVolume");
        AudioManager.instance.effectsVolumeValue = PlayerPrefs.GetFloat("EffectVolume");
        AudioManager.instance.musicVolumeValue = PlayerPrefs.GetFloat("MusicVolume");
    }
    //-----------------

}
