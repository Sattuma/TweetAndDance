using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelScript : MonoBehaviour
{
    public GameObject[] pickupPrefab;
    public GameObject[] pickupSpawnPoints;

    public List<GameObject> pickUpGroundList = new List<GameObject>();
    public int pickUpCountVariation;

    public GameObject[] pickupSpawnPointsAir;
    public GameObject[] pickupsInScene;

    public GameObject[] secretsFoundInScene;
    public GameObject[] secretsInScene;


    private void Awake()
    {
        GameModeManager.instance.levelIndex = SceneManager.GetActiveScene().buildIndex;
        string level = GameModeManager.instance.levelName[GameModeManager.instance.levelIndex];
        GameModeManager.instance.ActivateCurrentLevel(level);

        GameModeManager.Success += CountLevelSuccess;

        GameModeManager.instance.levelActive = false;
        GameModeManager.instance.cutsceneActive = true;
        GameModeManager.instance.activeGameMode = GameModeManager.GameMode.cutScene;
        AudioManager.instance.PlayMusicFX(3);


    }

    private void Start()
    {
        GroundSpawnPickupLevel();
        //StartInvokeRepeating();
        StartCoroutine(StartLevelCheck());

    }

    IEnumerator StartLevelCheck()
    {
        yield return new WaitUntil(() => GameModeManager.instance.cutsceneActive == false);
        GameModeManager.instance.activeGameMode = GameModeManager.GameMode.gameLevel;
        GameModeManager.instance.StartLevelInvoke();
    }


    public void GroundSpawnPickupLevel()
    {
        while(0 < pickUpCountVariation)
        {
            for (int i = 0; i < pickupSpawnPoints.Length; i++)
            {
                GameObject pickupRange = pickupPrefab[Random.Range(0, pickupPrefab.Length)];
                Instantiate(pickupRange, pickupSpawnPoints[i].transform.position, pickupSpawnPoints[i].transform.rotation);
                pickUpGroundList.Add(pickupRange);
            }
            pickUpCountVariation--;
        }
    }

    public void StartInvokeRepeating()
    {
        InvokeRepeating("AirSpawnPickup1", 5f, 10f);
        InvokeRepeating("AirSpawnPickup2", 3f, 9f);
    }

    public void AirSpawnPickup1()
    {
        Instantiate(pickupPrefab[(5)], pickupSpawnPointsAir[Random.Range(0, 2)].transform.position, pickupSpawnPointsAir[Random.Range(0, 2)].transform.rotation);
        Instantiate(pickupPrefab[(5)], pickupSpawnPointsAir[Random.Range(6, 7)].transform.position, pickupSpawnPointsAir[Random.Range(6, 7)].transform.rotation);

    }
    public void AirSpawnPickup2()
    {
        Instantiate(pickupPrefab[(6)], pickupSpawnPointsAir[Random.Range(3, 5)].transform.position, pickupSpawnPointsAir[Random.Range(3, 5)].transform.rotation);
        Instantiate(pickupPrefab[(6)], pickupSpawnPointsAir[Random.Range(8, 10)].transform.position, pickupSpawnPointsAir[Random.Range(8, 10)].transform.rotation);
    }

    public void DestroyPickUpsWithTag()
    {
        pickupsInScene = GameObject.FindGameObjectsWithTag("Collectable");
        for (int i = 0; i < pickupsInScene.Length; i++)
        { Destroy(pickupsInScene[i]); }
    }
    public void DestroyPickUpsWithTag2()
    {
        pickupsInScene = GameObject.FindGameObjectsWithTag("NestObject");
        for (int i = 0; i < pickupsInScene.Length; i++)
        { Destroy(pickupsInScene[i]);  }
    }

    //EI KUTSUTA VIELÄ MISTÄÄN - LAITA TSEKKI KUN KENTTTÄ MENEE LÄPI JA TÄSTÄ SITTEN SUCCES MENUUN INFOT!
    public void CountLevelSuccess()
    {
        SecretsCheck();
        PointsCheck();
    }
    public void SecretsCheck()
    {
        secretsInScene = GameObject.FindGameObjectsWithTag("Secret");
        secretsFoundInScene = GameObject.FindGameObjectsWithTag("SecretFound");

        for (int i = 0; i < secretsInScene.Length; i++)
        { i = secretsInScene.Length; GameModeManager.instance.secretsMissed = i; }

        GameModeManager.instance.SecretsCheck(secretsFoundInScene);
    }
    public void PointsCheck()
    { GameModeManager.instance.HighScoreCheck();}

    public void StopInvoke()
    {
        //cancel invokerepating system if decide to use this system ?
        CancelInvoke();
    }
    public void LevelCleared()
    {
        //cancel invokerepating system if decide to use this system and destroy pick ups around the nest?
        StopInvoke();
        DestroyPickUpsWithTag();
    }

    private void OnDestroy()
    {
        //Reset variables which only count in current level
        GameModeManager.instance.levelScore = 0;
        GameModeManager.instance.secretsMissed = 0;
    }

}
