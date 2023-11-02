using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelNote : MonoBehaviour
{

    public GameObject noteObj;
    public GameObject startPos;
    public GameObject startPos2;
    public GameObject startPos3;
    public GameObject noteDestroyer;
    public GameObject[] notesInScene;

    public PlayerCore core;
    public HudScript hud;

    private void Awake()
    {
        AudioManager.instance.PlayMusicFX(2);
        StartCoroutine(GameMode2Start());
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameModeManager.BonusLevelEnd += StopInvoke;
        core.myAnim.SetTrigger("Level2");
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
        { Destroy(notesInScene[i]); }
    }
    public IEnumerator GameMode2Start()
    {

        // tähän game active pääll kun kaikki tarvittava on redi
        yield return new WaitForSeconds(2f);

        //yield return new WaitUntil(() => GameModeManager.instance.levelActive == true);
        yield return new WaitForSeconds(2f);
        GameModeManager.instance.levelActive = true;
        yield return new WaitForSeconds(2f);
        //noteLineImage.SetActive(true);
        //pointsText.SetActive(true);
        //pointsCountText.SetActive(true);
        yield return new WaitForSeconds(2f);
        InvokeStartLevel2();
    }

    public void InvokeStartLevel2()
    {
        InvokeRepeating("NoteSpawn1", 2f, 4f);
        InvokeRepeating("NoteSpawn2", 1f, 4f);
        InvokeRepeating("NoteSpawn3", 1.5f, 2f);
    }
}
