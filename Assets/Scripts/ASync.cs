 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ASync : MonoBehaviour
{

    public Slider progressBar;
    public TextMeshProUGUI percentText;
    public bool canChange;
    public float staticTimeForScreen;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LevelLoadDelay(sceneIndex));
    }

    IEnumerator LevelLoadDelay(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(staticTimeForScreen);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone && !canChange)
        {
            GameModeManager.instance.ResetEvents();
            Time.timeScale = 1;
            GameModeManager.instance.isPaused = false;
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            percentText.text = progress * 100f + ("%");
            if(progressBar.value == 100f)
            {
                progressBar.value = 100f;

            }
            yield return null;
        }
    }





}
