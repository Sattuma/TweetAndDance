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

    public GameObject button;
    public GameObject button2;
    public GameObject button3;
    public GameObject startPos;
    public GameObject startPos2;
    public GameObject startPos3;

    public int scoreLevel2 = 0;
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
        InvokeRepeating("NoteSpawn", 2f, 4f);
    }
    void NoteSpawn()
    {
        Instantiate(noteObj, startPos.transform.position, startPos.transform.rotation);
    }

    public void AddScore(int score)
    {
        scoreLevel2 += score;
        Level2Score.Invoke();
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
