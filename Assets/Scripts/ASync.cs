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

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LevelLoadDelay(sceneName));
    }

    IEnumerator LevelLoadDelay(string sceneName)
    {
        yield return new WaitForSecondsRealtime(staticTimeForScreen);
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone && !canChange)
        {
            GetData();
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

    public void GetData()
    {
        DataManager.instance.GetLevelTimers();
        DataManager.instance.GetLevelPoints();
        DataManager.instance.GetLevelSecrets();
    }


}
