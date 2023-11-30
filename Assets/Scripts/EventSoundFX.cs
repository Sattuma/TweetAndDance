using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundFX : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] walkSound;
    public AudioClip[] flySound;
    public AudioClip timeSound;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
        audioSource.enabled = false;
    }

    private void Start()
    {
        Cursor.visible = true;
    }
    public void PlayTimeSoundFX()
    {
        audioSource.enabled = true;
        audioSource.clip = timeSound;
        audioSource.Play();
    }
    public void WalkingSoundFX()
    {
        audioSource.enabled = true;
        audioSource.clip = walkSound[Random.Range(0, walkSound.Length)];
        audioSource.Play();
    }
    public void FlyingSoundFX()
    {
        audioSource.enabled = true;
        audioSource.clip = flySound[Random.Range(0, walkSound.Length)];
        audioSource.Play();
    }

    public void MainMenuButton()
    {
        GameModeManager.instance.ChangeLevel(GameModeManager.instance.levelName[0]);
    }
}
