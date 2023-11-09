using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLineScript : MonoBehaviour
{

    public BonusLevelScript bonusCore;
    public int pointlossCheck;

    [Header("GameObjects")]
    public GameObject[] activators = new GameObject[4];
    public GameObject[] startingPos = new GameObject[4];
    public GameObject[] notePrefab = new GameObject[3];

    [Header("FUNCTION BOOLEAS")]
    public bool canStartSong;
    public bool song1Active;
    public bool song2Active;

    [Header("POSITION TRACKING")]
    public float songTimeTotal;
    public float songBeatsTotal;

    public float secPosition;
    public float beatsposition;
    public float beatsShownInAdvance;
    float secPerBeat;

    [Header("SONG INFORMATION")]
    public float bpm;
    public float noteSpeed;

    [Header("SUCCESS/FAIL VARIABLES")]
    public float startCountforFail;
    public bool countOn;
    public float failCount;

    //LEVEL NOTES AND SYNC ARRAYS
    public float[] notes; // song 1 sync notes
    public float[] notes2;// song 2 sync notes

    public int nextIndex = 0;


    private void Awake()
    {
        GameModeManager.instance.bonuslevelScoreTemp = 4000;

        bonusCore = GetComponentInParent<BonusLevelScript>();
        GameModeManager.StartLevel += StopMusic;
        GameModeManager.StartLevelCountOver += StartSong;
    }
    private void Start()
    {
        //reset booleans on default state on START
        canStartSong = false;
        song1Active = false;
        song1Active = false;
        countOn = false;
    }

    private void StopMusic()
    {
        AudioManager.instance.musicSource.Stop();
    }

    private void StartSong()
    {
        AudioManager.instance.musicSource.loop = false;
        canStartSong = true;

        if (GameModeManager.instance.levelIndex == 1)
        {
            song1Active = true;
            AudioManager.instance.PlayMusicFX(2);
            bpm = 164;
            noteSpeed = 9.5f;
            secPerBeat = 60f / bpm;
            songTimeTotal = AudioManager.instance.musicFX[2].length;
            songBeatsTotal = songTimeTotal / secPerBeat;
        }
        if (GameModeManager.instance.levelIndex == 2)
        {
            song2Active = true;
            //valitaan viel musa
            //bpm = 164;
            //noteSpeed = 9.5f;
            secPerBeat = 60f / bpm;
            songTimeTotal = AudioManager.instance.musicFX[0].length;
            songBeatsTotal = songTimeTotal / secPerBeat;
        }
    }


    void Update()
    {
        if(canStartSong)
        {

            if (song1Active)
            {
                if (!GameModeManager.instance.isPaused)
                {
                    Song1();
                    StartSongCount();
                    CheckScoring();
                }

            }
            if(song2Active)
            {
                if (!GameModeManager.instance.isPaused)
                {
                    Song2();
                    StartSongCount();
                    CheckScoring();
                }

            }
        }
    }

    public void StartSongCount()
    {
        secPosition = AudioManager.instance.musicSource.time;
        beatsposition = secPosition / secPerBeat;
        if (beatsposition >= songBeatsTotal)
        { beatsposition = songBeatsTotal; secPerBeat = songTimeTotal; }

    }

    public void Song1()
    {

        CheckFail();

        if (nextIndex < notes.Length && notes[nextIndex] < beatsposition + beatsShownInAdvance)
        {
            GameObject clone = Instantiate(notePrefab[Random.Range(0, notePrefab.Length)], startingPos[Random.Range(0, startingPos.Length)].transform.position, startingPos[0].transform.rotation);
            clone.GetComponent<NoteScript>().currentSpeed = noteSpeed;
            nextIndex++;
        }

        if(secPosition >= songTimeTotal)
        {
            song1Active = false;
            CheckSuccess();
        }
    }
    public void Song2()
    {
        CheckFail();

        //toisen laulun info t‰nne
    }


    public void CheckScoring() // tsekkaa aktiivisen pistem‰‰r‰n vaan ku song on aktiivinen
    {

        GameModeManager.instance.bonuslevelScoreTemp -= pointlossCheck;
        if (GameModeManager.instance.bonuslevelScoreTemp <= 0)
        { GameModeManager.instance.bonuslevelScoreTemp = 0; }
        if (GameModeManager.instance.bonuslevelScoreTemp >= 10000)
        { GameModeManager.instance.bonuslevelScoreTemp = 10000; }
    }

    public void CheckFail()
    {
        if(GameModeManager.instance.bonuslevelScoreTemp <= 1800 && GameModeManager.instance.bonuslevelScoreTemp >= 0)
        {
            failCount -= Time.deltaTime;

            GameModeManager.instance.InvokeBonusMeterAnimOn();
            if (failCount <= 0)
            {
                failCount = 0;
                song1Active = false;
                song2Active = false;
                GameModeManager.instance.levelActive = false;
                GameModeManager.instance.bonusLevelEnd = true;
                GameModeManager.instance.InvokeBonusFail();
                GameModeManager.instance.InvokeBonusMeterAnimOff();
            }
        }
        else
        {
            GameModeManager.instance.InvokeBonusMeterAnimOff();
            failCount = 10;
        }

    }


    public void CheckSuccess()
    {
        GameModeManager.instance.bonusLevelEnd = true;
    }


}
