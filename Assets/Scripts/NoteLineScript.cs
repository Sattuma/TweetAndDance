using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLineScript : MonoBehaviour
{
    public GameObject noteObj;
    public GameObject startPos;
    public GameObject startPos2;
    public GameObject startPos3;
    public GameObject noteDestroyer;
    public GameObject[] notesInScene;

    public HudScript hud;

    // Start is called before the first frame update
    void Start()
    {
        GameModeManager.Success += LevelOneCleared;
        GameModeManager.Level2End += StopInvoke;
    }

    public void InvokeStartLevel2()
    {
        InvokeRepeating("NoteSpawn1", 2f, 4f);
        InvokeRepeating("NoteSpawn2", 1f, 4f);
        InvokeRepeating("NoteSpawn3", 1.5f, 2f);
    }
    void NoteSpawn1()
    { Instantiate(noteObj, startPos2.transform.position, startPos2.transform.rotation); }
    void NoteSpawn2()
    { Instantiate(noteObj, startPos.transform.position, startPos.transform.rotation); }
    void NoteSpawn3()
    { Instantiate(noteObj, startPos3.transform.position, startPos3.transform.rotation); }
    public void StopInvoke()
    {
        CancelInvoke();
        notesInScene = GameObject.FindGameObjectsWithTag("Perfect"); //muuttujalle arvo, etsii viholliset tagin avulla
        for (int i = 0; i < notesInScene.Length; i++)
        { Destroy(notesInScene[i]);}
        GameModeManager.instance.scoreEndCount = 0;
    }

    public void LevelOneCleared()
    {
        GameModeManager.instance.levelActive = false;
        GameModeManager.instance.activeGameMode = GameModeManager.GameMode.level2;
    }
}
