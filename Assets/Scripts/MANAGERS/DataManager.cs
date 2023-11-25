using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    //controller connected bool
    public bool controllerConnected = false;

    //Bonus one song sync data
    public string[] bonusOneSong1Normal;
    public string[] bonusOneSong1Hard;
    public string[] bonusOneSong2Normal;
    public string[] bonusOneSong2Hard;

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

        //tsek aktiivinen kontrolleri
        CheckControls();
        //tsekki onko kontrolleri kytketty vai ei 
        StartCoroutine(CheckForControllers());

        //Bonus level one song infos and difficulties 
        bonusOneSong1Normal = File.ReadAllLines(@"Assets/Scripts/Data/song1medium.txt");
        bonusOneSong1Hard = File.ReadAllLines(@"Assets/Scripts/Data/song1hard.txt");
        bonusOneSong2Normal = File.ReadAllLines(@"Assets/Scripts/Data/song2medium.txt");
        bonusOneSong2Hard = File.ReadAllLines(@"Assets/Scripts/Data/song2hard.txt");
    }

    IEnumerator CheckForControllers()
    {
        while (true)
        {
            string[] names = Input.GetJoystickNames();

            for (int x = 0; x < names.Length; x++)
            {
                //print(names[x].Length);
                if (names[x] == null)
                {
                    names[x] = null;
                }
                if (!controllerConnected && names[x].Length > 0)
                {
                    controllerConnected = true;
                    CheckControls();

                }
                else if (controllerConnected && names[x].Length == 0)
                {
                    controllerConnected = false;
                    controls = DataManager.ControlSystem.Keyboard;
                    CheckControls();
                    GameModeManager.instance.ControllerCheckInvoke();
                }
            }
            yield return new WaitForSeconds(1f);   
        }   
    }
    //LEVEL TIMERS DATA
    public void SetLevelTimers(float timer1,float timer2)
    {
        PlayerPrefs.SetFloat("TimerNormal", timer1);
        PlayerPrefs.SetFloat("TimerHard", timer2);
    }
    public void GetLevelTimers()
    {
        GameModeManager.instance.timerNormalMode = PlayerPrefs.GetFloat("TimerNormal");
        GameModeManager.instance.timerHardMode = PlayerPrefs.GetFloat("TimerHard");
    }
    //-----------------

    //AUDIO LEVELS DATA
    public void SetLevelAudio(float value1,float value2,float value3)
    {
        Debug.Log("SAVE ÄÄNI");
        PlayerPrefs.SetFloat("MasterVolume", value1);
        PlayerPrefs.SetFloat("EffectVolume", value2);
        PlayerPrefs.SetFloat("MusicVolume", value3);


        Debug.Log(value1);
        Debug.Log(value2);
        Debug.Log(value3);

    }
    public void GetLevelAudio(float value1, float value2, float value3)
    {
        Debug.Log("LOAD ÄÄNI");
        PlayerPrefs.GetFloat("MasterVolume",value1);
        PlayerPrefs.GetFloat("EffectVolume",value2);
        PlayerPrefs.GetFloat("MusicVolume",value3);


        Debug.Log(value1);
        Debug.Log(value2);
        Debug.Log(value3);
    }
    //-----------------

    //SECRETS LEVEL DATA
    public void SetLevelSecrets(int secrets)
    {
        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_1)
        { 
            PlayerPrefs.SetInt("Secrets1_1", secrets);
            PlayerPrefs.SetInt("SecretsMissed1_1", secrets);
        }

        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_2)
        {
            PlayerPrefs.SetInt("Secrets1_2", secrets);
            PlayerPrefs.SetInt("SecretsMissed1_2", secrets);
        }

        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_3)
        { 
            PlayerPrefs.SetInt("Secrets1_3", secrets);
            PlayerPrefs.SetInt("SecretsMissed1_2", secrets);
        }
    }

    public void GetLevelSecrets()
    {
        GameModeManager.instance.secretInLevel[1] = PlayerPrefs.GetInt("Secrets1_1");
        GameModeManager.instance.secretInLevel[2] = PlayerPrefs.GetInt("Secrets1_2");
        GameModeManager.instance.secretInLevel[3] = PlayerPrefs.GetInt("Secrets1_3");

        GameModeManager.instance.secretMissedInLevel[1] = PlayerPrefs.GetInt("SecretsMissed1_1");
        GameModeManager.instance.secretMissedInLevel[2] = PlayerPrefs.GetInt("SecretsMissed1_2");
        GameModeManager.instance.secretMissedInLevel[3] = PlayerPrefs.GetInt("SecretsMissed1_3");
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

    public void CheckControls()
    {
        if (controls == DataManager.ControlSystem.Gamepad)
        { ActivateGamePad(); }
        if (controls == DataManager.ControlSystem.Keyboard)
        { ActivateKeyboard(); }
    }

    public void ActivateKeyboard()
    {
        Keyboard.current?.IsActuated(0);
        Gamepad.current?.IsActuated(0);

        if (Gamepad.current?.IsActuated(0) == null)
        {
            InputSystem.EnableDevice(Keyboard.current);
            controls = ControlSystem.Keyboard;
        }
        else if(Gamepad.current?.IsActuated(0) != null)
        {
            InputSystem.EnableDevice(Keyboard.current);
            InputSystem.DisableDevice(Gamepad.current);
            controls = ControlSystem.Keyboard;
        }

    }
    public void ActivateGamePad()
    {
        Keyboard.current?.IsActuated(0);
        Gamepad.current?.IsActuated(0);

        if (Gamepad.current?.IsActuated(0) == null)
        {
            InputSystem.EnableDevice(Keyboard.current);
            controls = ControlSystem.Keyboard;
        }
        else if (Gamepad.current?.IsActuated(0) != null)
        {
            InputSystem.EnableDevice(Gamepad.current);
            InputSystem.DisableDevice(Keyboard.current);
            controls = ControlSystem.Gamepad;
        }
    }

    public void CheckBonusLevelInfo(GameObject obj, GameObject obj2, GameObject obj3)
    {
        if (GameModeManager.instance.levelIndex > 0 && GameModeManager.instance.levelIndex <= 3)
        {
            string level = GameModeManager.instance.bonusLevelName[1];
            GameModeManager.instance.ActivateCurrentLevel(level);
            AudioManager.instance.PlayMusicFX(3);
            obj.SetActive(true);
        }
        if (GameModeManager.instance.levelIndex > 3 && GameModeManager.instance.levelIndex <= 6)
        {
            string level = GameModeManager.instance.bonusLevelName[2];
            GameModeManager.instance.ActivateCurrentLevel(level);
            AudioManager.instance.PlayMusicFX(1);
            obj2.SetActive(true);
        }
        if (GameModeManager.instance.levelIndex > 6 && GameModeManager.instance.levelIndex <= 9)
        {
            string level = GameModeManager.instance.bonusLevelName[3];
            GameModeManager.instance.ActivateCurrentLevel(level);
            AudioManager.instance.PlayMusicFX(1);
            obj3.SetActive(true);
        }
    }




}
