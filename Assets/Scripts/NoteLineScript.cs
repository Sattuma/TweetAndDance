using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLineScript : MonoBehaviour
{


    public GameObject[] activators = new GameObject[4];
    public GameObject[] startingPos = new GameObject[4];
    public GameObject[] notePrefab = new GameObject[3];


    //BPM INFO PER BONUS LEVEL
    //BonusLevel 1 bpm = 164
    //Bonuslevel 1 notespeed = 9.5f

    public bool canStartSong;
    public float beatsShownInAdvance;
    public float noteSpeed;

    [Header("POSITION TRACKING")]
    public float songTimeTotal;
    public float songBeatsTotal;

    public float secPosition;
    public float beatsposition;
    float secPerBeat;

    [Header("SONG INFORMATION")]
    public float bpm;
    public float[] notes;
    public int nextIndex = 0;

    private void Awake()
    {
        GameModeManager.StartLevelCountOver += StartSong;
    }

    private void StartSong()
    {
        AudioManager.instance.musicSource.loop = false;
        AudioManager.instance.PlayMusicFX(2);
        canStartSong = true;
    }

    private void Start()
    {


        canStartSong = false;

        secPerBeat = 60f / bpm;

        songTimeTotal = AudioManager.instance.musicFX[2].length;
        songBeatsTotal = songTimeTotal / secPerBeat;
    }

    void Update()
    {
        if(canStartSong)
        {
            secPosition = AudioManager.instance.musicSource.time;
            beatsposition = secPosition / secPerBeat;
            if (beatsposition >= songBeatsTotal)
            { beatsposition = songBeatsTotal; secPerBeat = songTimeTotal; }

            if (nextIndex < notes.Length && notes[nextIndex] < beatsposition + beatsShownInAdvance)
            {
                GameObject clone = Instantiate(notePrefab[Random.Range(0, notePrefab.Length)], startingPos[Random.Range(0, startingPos.Length)].transform.position, startingPos[0].transform.rotation);
                clone.GetComponent<NoteScript>().currentSpeed = noteSpeed;
                nextIndex++;
            }
        }
    }

    private void OnDestroy()
    {
        AudioManager.instance.musicSource.loop = true;
    }
}
