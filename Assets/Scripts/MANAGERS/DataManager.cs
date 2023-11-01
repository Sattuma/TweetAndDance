using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    public enum ControlSystem
    {
        Keyboard,
        Gamepad
    }

    public ControlSystem controls;

    private void Awake()
    {
        if(instance != null)
        { Destroy(gameObject); }
        else
        { instance = this; DontDestroyOnLoad(instance); }

        ActivateKeyboard();


    }

    public void CheckControllerNull() // ei toimi
    {
        string[] names = Input.GetJoystickNames();

        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if(names[x] == null)
            {
                names[x]  = null;
            }
        }
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

    //SECRETS LEVEL DATA
    public void SetLevelSecrets(int secrets)
    {
        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_1)
        { PlayerPrefs.SetInt("Secrets1_1", secrets); }

        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_2)
        { PlayerPrefs.SetInt("Secrets1_2", secrets); }

        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_3)
        { PlayerPrefs.SetInt("Secrets1_3", secrets); }
    }

    public void GetLevelSecrets()
    {
        GameModeManager.instance.secretLevel1_1 = PlayerPrefs.GetInt("Secrets1_1");
        GameModeManager.instance.secretLevel1_2 = PlayerPrefs.GetInt("Secrets1_2");
        GameModeManager.instance.secretLevel1_3 = PlayerPrefs.GetInt("Secrets1_3");
    }

    //POINTS LEVEL DATA
    public void SetLevelPoints(int points)
    {
        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_1)
        { PlayerPrefs.SetInt("HiScore1_1", points);}

        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_2)
        { PlayerPrefs.SetInt("HiScore1_2", points); }

        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_3)
        { PlayerPrefs.SetInt("HiScore1_3", points); }
    }

    public void GetLevelPoints()
    {
        GameModeManager.instance.highScoreLevel1_1 = PlayerPrefs.GetInt("HiScore1_1");
        GameModeManager.instance.highScoreLevel1_2 = PlayerPrefs.GetInt("HiScore1_2");
        GameModeManager.instance.highScoreLevel1_3 = PlayerPrefs.GetInt("HiScore1_3");
    }

    public void CurrentLevelCheck()
    {

    }

    public void ActivateKeyboard()
    {
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.DisableDevice(Gamepad.current);
        controls = ControlSystem.Keyboard;
    }
    public void ActivateGamePad()
    {
        InputSystem.EnableDevice(Gamepad.current);
        InputSystem.DisableDevice(Keyboard.current);
        controls = ControlSystem.Gamepad;
    }

}
