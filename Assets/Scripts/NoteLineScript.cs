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
    public GameObject notePrefab;

    [Header("FUNCTION BOOLEAS")]
    public bool songActive;

    [Header("POSITION TRACKING")]
    private float songTimeTotal;
    public float songBeatsTotal;

    private float secPosition;
    public float beatsposition;
    public float songLastBeat;
    public float beatsShownInAdvance;
    float secPerBeat;

    [Header("SONG INFORMATION")]
    public float bpm;
    public float noteSpeed;

    [Header("SUCCESS/FAIL VARIABLES")]
    public bool countOn;
    public float failCount;
    float startCount = 1;


    //LEVEL NOTES AND SYNC ARRAYS
    public float[] notes; // song 1 sync notes
    public float[] songNotes;

    public int nextIndex = 0;


    private void Awake()
    {
        bonusCore = GetComponentInParent<BonusLevelScript>();
        GameModeManager.StartLevel += StopMusic;
        GameModeManager.StartLevelCountOver += StartSong;
    }
    private void Start()
    {
        GameModeManager.instance.bonusOnelevelScoreTemp = 4000;
        GameModeManager.instance.bonusLevelEnd = false;


        if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Normal)
        {
            GetNotesForNormalDifficulty();
        }
        else if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Hard)
        {
            GetNotesForHardDifficulty();
        }

        songActive = false;
        countOn = false;
    }

    public void GetNotesForNormalDifficulty()
    {
        if(GameModeManager.instance.levelIndex == 1)
        {
            Debug.Log("Normal level 1 bonus");
            songNotes = new float[DataManager.instance.bonusOneSong1Normal.Length];
            GetNotesSyncForSong(songNotes, DataManager.instance.bonusOneSong1Normal);
        }
        if (GameModeManager.instance.levelIndex == 2)
        {
            Debug.Log("Normal level 2 bonus");
            songNotes = new float[DataManager.instance.bonusOneSong2Normal.Length];
            GetNotesSyncForSong(songNotes, DataManager.instance.bonusOneSong2Normal);
        }
    }

    public void GetNotesForHardDifficulty()
    {
        if (GameModeManager.instance.levelIndex == 1)
        {
            Debug.Log("HArd level 1 bonus");
            songNotes = new float[DataManager.instance.bonusOneSong1Hard.Length];
            GetNotesSyncForSong(songNotes, DataManager.instance.bonusOneSong1Hard);
        }
        if (GameModeManager.instance.levelIndex == 2)
        {
            Debug.Log("HArd level 2 bonus");
            songNotes = new float[DataManager.instance.bonusOneSong2Hard.Length];
            GetNotesSyncForSong(songNotes, DataManager.instance.bonusOneSong2Hard);
        }
    }

    private void StopMusic()
    {
        AudioManager.instance.musicSource.Stop();
    }

    private void StartSong()
    {
        AudioManager.instance.musicSource.loop = false;

        if (GameModeManager.instance.levelIndex == 1)
        {
            songActive = true;
            AudioManager.instance.PlayMusicFX(2); 
            //SONG 1 INFORMATION FORVARIABLES
            bpm = 164;
            noteSpeed = 7f;
            secPerBeat = 60f / bpm;
            songLastBeat = 441;
            songTimeTotal = AudioManager.instance.musicFX[2].length;
            songBeatsTotal = songTimeTotal / secPerBeat;
            GameModeManager.instance.bonusOneSongTimeTotalTemp = songTimeTotal;
        }
        if (GameModeManager.instance.levelIndex == 2)
        {
            songActive = true;
            //SONG 2 INFORMATION FORVARIABLES
            AudioManager.instance.PlayMusicFX(0);
            //valitaan viel musa
            //bpm = ???;
            //noteSpeed = ??f;
            //songLastBeat = ??;
            secPerBeat = 60f / bpm;
            songTimeTotal = AudioManager.instance.musicFX[0].length;
            songBeatsTotal = songTimeTotal / secPerBeat;
            GameModeManager.instance.bonusOneSongTimeTotalTemp = songTimeTotal;
        }
    }


    void Update()
    {
        if(songActive)
        {
            if (!GameModeManager.instance.isPaused)
            {
                SongRunning();
                StartSongCount();
                CheckScoring();
            }
        }
    }

    public void StartSongCount()
    {
        secPosition = AudioManager.instance.musicSource.time;
        beatsposition = secPosition / secPerBeat;

        if (beatsposition >= songBeatsTotal)
        { 
            beatsposition = songBeatsTotal; 
            secPerBeat = songTimeTotal; 
        }

        GameModeManager.instance.bonusOneSongTimeTemp = secPosition;

    }

    public void SongRunning()
    {

        CheckFail();

        if (nextIndex < songNotes.Length && songNotes[nextIndex] < beatsposition + beatsShownInAdvance)
        {
            GameObject clone = Instantiate(notePrefab, startingPos[Random.Range(0, startingPos.Length)].transform.position, startingPos[0].transform.rotation);
            clone.GetComponent<NoteScript>().currentSpeed = noteSpeed;
            nextIndex++;
        }

        if(beatsposition >= songLastBeat)
        {
            songActive = false;
            CheckSuccess();
        }
    }

    public void CheckScoring() // tsekkaa aktiivisen pistem‰‰r‰n vaan ku song on aktiivinen
    {

        GameModeManager.instance.bonusOnelevelScoreTemp -= pointlossCheck;
        if (GameModeManager.instance.bonusOnelevelScoreTemp <= 0)
        { GameModeManager.instance.bonusOnelevelScoreTemp = 0; }
        if (GameModeManager.instance.bonusOnelevelScoreTemp >= 10000)
        { GameModeManager.instance.bonusOnelevelScoreTemp = 10000; }
    }

    public void CheckFail()
    {

        startCount -= Time.deltaTime;

        if(GameModeManager.instance.bonusOnelevelScoreTemp <= 1800 && GameModeManager.instance.bonusOnelevelScoreTemp >= 0)
        {

            failCount -= Time.deltaTime;
            GameModeManager.instance.InvokeBonusMeterAnimOn();

            if (failCount <= 0)
            {
                failCount = 0.01f;
                songActive = false;
                GameModeManager.instance.levelActive = false;
                GameModeManager.instance.bonusLevelEnd = true;
                GameModeManager.instance.InvokeBonusFail();
            }
        }
        else if(GameModeManager.instance.bonusOnelevelScoreTemp > 1800)
        {
            failCount = 10;

            if (startCount <= 0)
            {
                startCount = 0;
                GameModeManager.instance.InvokeBonusMeterAnimOff(); 
            }

        }
    }

    public void CheckSuccess()
    {
        GameModeManager.instance.bonusLevelEnd = true;

        if(GameModeManager.instance.bonusOnelevelScoreTemp >= 8200)
        {
            StartCoroutine(endSuccessDelay());
        }
        else if(GameModeManager.instance.bonusOnelevelScoreTemp < 8200)
        {
            StartCoroutine(endFailDelay());
        }

    }

    public void GetNotesSyncForSong(float[] values, string[] info)
    {
        for (int i = 0; i < info.Length; i++)
        {
            values[i] = float.Parse(info[i]);
        }
    }

    IEnumerator endSuccessDelay()
    {
        GameModeManager.instance.InvokeBonusOneEnd();
        yield return new WaitForSecondsRealtime(1f);
        GameModeManager.instance.levelActive = false;
        yield return new WaitForSecondsRealtime(3f);
        GameModeManager.instance.InvokeBonusSuccess();
    }
    IEnumerator endFailDelay()
    {
        GameModeManager.instance.InvokeBonusOneEnd();
        yield return new WaitForSecondsRealtime(1f);
        GameModeManager.instance.levelActive = false;
        yield return new WaitForSecondsRealtime(4f);
        GameModeManager.instance.InvokeBonusFail();
    }


}
