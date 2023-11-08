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

    //LEVEL NOTES AND SYNC ARRAYS
    public float[] notes; // song 1 sync notes
    public float[] notes2;// song 2 sync notes

    public int nextIndex = 0;


    private void Awake()
    {
        GameModeManager.instance.bonuslevelScoreTemp = 0;

        bonusCore = GetComponentInParent<BonusLevelScript>();
        GameModeManager.StartLevel += StopMusic;
        GameModeManager.StartLevelCountOver += StartSong;
    }
    private void Start()
    {
        canStartSong = false;
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
            { Song1(); StartSongCount(); }
            if(song2Active)
            { Song2(); StartSongCount(); }

            CheckScoring();
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
   
        if (nextIndex < notes.Length && notes[nextIndex] < beatsposition + beatsShownInAdvance)
        {
            GameObject clone = Instantiate(notePrefab[Random.Range(0, notePrefab.Length)], startingPos[Random.Range(0, startingPos.Length)].transform.position, startingPos[0].transform.rotation);
            clone.GetComponent<NoteScript>().currentSpeed = noteSpeed;
            nextIndex++;
        }
        
    }
    public void Song2()
    {

    }

    public void CheckScoring()
    {
        GameModeManager.instance.bonuslevelScoreTemp -= pointlossCheck;

        if(GameModeManager.instance.bonuslevelScoreTemp <= 0)
        { GameModeManager.instance.bonuslevelScoreTemp = 0; }
    }

}
