using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public delegate void GameScore();
    public static event GameScore Level2Score;

    public static GameModeManager instance;
    public GameMode activeGameMode;

    public bool level1Over;
    public bool level2Over;
    public bool level3Over;

    public GameObject noteLine;
    public GameObject noteObj;

    public GameObject startPos;
    public GameObject startPos2;
    public GameObject startPos3;
    public GameObject noteDestroyer;

    public int scoreLevel2 = 0;
    public int scoreEndCount = 0;
    public int scoreLevel3 = 0;

    public enum GameMode
    {
        level1,
        level2,
        level3
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public void LevelOneCleared()
    {
        level1Over = true;
        activeGameMode = GameMode.level2;
        StartCoroutine(GameMode2Start());
    }

    public IEnumerator GameMode2Start()
    {
        yield return new WaitForSeconds(2f);
        noteLine.SetActive(true);
        yield return new WaitForSeconds(2f);
        InvokeStartLevel2();
    }

    void InvokeStartLevel2()
    {
        InvokeRepeating("NoteSpawn1", 2f, 4f);
        InvokeRepeating("NoteSpawn2", 1f, 4f);
        InvokeRepeating("NoteSpawn3", 1.5f, 2f);
    }
    void NoteSpawn1()
    { Instantiate(noteObj, startPos2.transform.position, startPos2.transform.rotation);}
    void NoteSpawn2()
    { Instantiate(noteObj, startPos.transform.position, startPos.transform.rotation);}
    void NoteSpawn3()
    { Instantiate(noteObj, startPos3.transform.position, startPos3.transform.rotation);}

    public void AddScore(int score)
    {
        scoreLevel2 += score;
        //Level2Score.Invoke();
    }

    public void LevelTwoCleared()
    {
        level2Over = true;
        activeGameMode = GameMode.level3;
    }

    public void LevelFailed()
    {
        //pelaaja fail anim
        //level pisteet
    }

}
