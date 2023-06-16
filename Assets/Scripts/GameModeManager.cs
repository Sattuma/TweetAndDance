using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public delegate void GameAction();
    public static event GameAction Level2Score;
    public static event GameAction Level1End;
    public static event GameAction Level2End;
    //public static event GameAction Level3End;
    public static event GameAction Success;
    public static event GameAction Fail;

    public static GameModeManager instance;

    public GameMode activeGameMode;

    public bool levelActive;

    public bool level2Retry;

    public float timerLevel1;

    public int scoreLevel2 = 0;
    public int scoreEndCount = 0;
    public int scoreLevel3 = 0;

    public GameObject[] pickupPrefab;
    public GameObject[] pickupSpawnPoints;
    public GameObject[] pickupSpawnPointsAir;
    public GameObject[] pickupsInScene;

    public AinoSpawner ainoSpawner;

    public enum GameMode
    {
        cutScene1,
        level1,
        cutScene2,
        level2,
        cutScene3,
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

    private void Start()
    {
        GroundSpawnPickupLevel1();


    }

    //TÄHÄN JOKU PAREMPI SYSTEEMI SAISKO UPDATESTA CHECKIN POIS JA MUUTA KAUTTA?
    private void Update()
    {
        if (scoreEndCount >= 5)
        {
            levelActive = false;
            Level2End?.Invoke();
        }
    }

    public void InvokeLevel1End()
    {
        Level1End?.Invoke();
    }

    public void CutScene1Active()
    {
        activeGameMode = GameMode.cutScene1;
    }
    public void Level1Active()
    {
        activeGameMode = GameMode.level1;
        StartInvokeRepeating();
    }
    public void CutScene2Active()
    {
        activeGameMode = GameMode.cutScene2;
    }
    public void Level2Active()
    {
        activeGameMode = GameMode.level2;
    }
    public void CutScene3Active()
    {
        activeGameMode = GameMode.cutScene3;
    }
    public void Level3Active()
    {
        activeGameMode = GameMode.level3;
    }

    public void LevelOneCleared()
    {
        Success?.Invoke();
        if(activeGameMode == GameMode.level1)
        {
            StopInvoke();
            DestroyPickUpsWithTag();
        }

    }
    
    public void AddScore(int score)
    {
        scoreLevel2 += score;
        Level2Score?.Invoke();
    }

    public void LevelTwoCleared()
    {
        Success?.Invoke();
        levelActive = false;
    }

    public void LevelThreeCleared()
    {
        Success?.Invoke();
        levelActive = false;
    }

    public void LevelFailed()
    {
        Fail?.Invoke();
    }

    public void GroundSpawnPickupLevel1()
    {
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[0].transform.position,pickupSpawnPoints[0].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[1].transform.position,pickupSpawnPoints[1].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[2].transform.position,pickupSpawnPoints[2].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[3].transform.position,pickupSpawnPoints[3].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[4].transform.position,pickupSpawnPoints[4].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[5].transform.position,pickupSpawnPoints[5].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[6].transform.position, pickupSpawnPoints[6].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[7].transform.position, pickupSpawnPoints[7].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[8].transform.position, pickupSpawnPoints[8].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[9].transform.position, pickupSpawnPoints[9].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[10].transform.position, pickupSpawnPoints[10].transform.rotation);
        Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], pickupSpawnPoints[11].transform.position, pickupSpawnPoints[11].transform.rotation);

        Instantiate(pickupPrefab[0], pickupSpawnPoints[0].transform.position, pickupSpawnPoints[0].transform.rotation);
        Instantiate(pickupPrefab[0], pickupSpawnPoints[1].transform.position, pickupSpawnPoints[1].transform.rotation);
        Instantiate(pickupPrefab[1], pickupSpawnPoints[2].transform.position, pickupSpawnPoints[2].transform.rotation);
        Instantiate(pickupPrefab[1], pickupSpawnPoints[3].transform.position, pickupSpawnPoints[3].transform.rotation);
        Instantiate(pickupPrefab[2], pickupSpawnPoints[4].transform.position, pickupSpawnPoints[4].transform.rotation);
        Instantiate(pickupPrefab[3], pickupSpawnPoints[5].transform.position, pickupSpawnPoints[5].transform.rotation);
        Instantiate(pickupPrefab[3], pickupSpawnPoints[6].transform.position, pickupSpawnPoints[6].transform.rotation);
        Instantiate(pickupPrefab[2], pickupSpawnPoints[7].transform.position, pickupSpawnPoints[7].transform.rotation);
        Instantiate(pickupPrefab[1], pickupSpawnPoints[8].transform.position, pickupSpawnPoints[8].transform.rotation);
        Instantiate(pickupPrefab[1], pickupSpawnPoints[9].transform.position, pickupSpawnPoints[9].transform.rotation);
        Instantiate(pickupPrefab[0], pickupSpawnPoints[10].transform.position, pickupSpawnPoints[10].transform.rotation);
        Instantiate(pickupPrefab[0], pickupSpawnPoints[11].transform.position, pickupSpawnPoints[11].transform.rotation);
    }

    public void StartInvokeRepeating()
    {
        InvokeRepeating("AirSpawnPickup1", 5f, 10f);
        InvokeRepeating("AirSpawnPickup2", 3f, 9f);
    }

    public void AirSpawnPickup1()
    {
        Instantiate(pickupPrefab[(5)], pickupSpawnPointsAir[Random.Range(0,2)].transform.position, pickupSpawnPointsAir[Random.Range(0, 2)].transform.rotation);
        Instantiate(pickupPrefab[(5)], pickupSpawnPointsAir[Random.Range(6,7)].transform.position, pickupSpawnPointsAir[Random.Range(6, 7)].transform.rotation);

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
        { Destroy(pickupsInScene[i]); }
    }

    public void StopInvoke()
    {
        CancelInvoke();
    }

}
