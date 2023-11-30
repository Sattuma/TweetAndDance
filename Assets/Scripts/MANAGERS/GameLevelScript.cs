using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ACTIVATE FUNCTIONS WHICH ARE NEEDED ONLY ON !!GAMELEVEL!!
//PURPOSE OF THIS SCRIPT IS COUNT TEMPORARILY INFO AND FUNCTIONS WHICH GOES TO GAME MANAGER IF NEEDED WERE STORED 
public class GameLevelScript : MonoBehaviour
{

    [Header("NEST TO SPAWN AT START OBJECTS")]
    public GameObject nestObjNormal;
    public GameObject nestObjHard;

    [Header("GAMEOBJECTS TO SPAWN IN LEVEL")]
    public GameObject[] pickupPrefabGround;
    public GameObject[] pickupPrefabAir;
    public GameObject[] pickupPrefabStatic;
    public GameObject rewardItemPreFab;

    [Header("GOUND PICKUP SPAWN TIME INTERVALS")]
    public float groundIntervalSeconds;
    public float airIntervalSeconds;

    public float tutorialTimeGround;
    public float tutorialTimeAir;
    public float levelTimeGround;
    public float levelTimeAir;

    [Header("GROUND PICKUPS CURRENT/MAX")]
    public int pickUp1Spawn;
    public int pickUp2Spawn;
    public int pickUp3Spawn;
    public int pickUp4Spawn;
    public int Spawn1Max;
    public int Spawn2Max;
    public int Spawn3Max;
    public int Spawn4Max;

    [Header("MOVEABLE SPAWN POINTS FOR PICKUPS")]
    public Transform spawnPos1;
    public Transform spawnPos2;
    public Transform spawnPos1Air;
    public Transform spawnPos2Air;
    public GameObject[] rewardSpawnPoints;

    public Transform[] pickupSpawnPointsStatic;
    public List<GameObject> pickUpStaticList = new();
    public int pickUpCountVariation;

    [Header("SPAWN COORDINATES FOR PICKUPS")]
    public float groundMinX1 = -30;
    public float groundMaxX1 = -4;
    public float groundMinX2 = -4;
    public float groundMaxX2 = 30;
    public float airMinX1 = -30;
    public float airMaxX1 = 4;
    public float airMinX2 = -4;
    public float airMaxX2 = 30;
    public float groundLevelY = -6.5f;
    public float airLevelMinY = 4f;
    public float airLevelMaxY = 8f;

    [Header("Variables for pick ups in scene")]
    public GameObject[] pickupsInScene;
    public GameObject[] secretsFoundInScene;
    public GameObject[] secretsInScene;

    [Header("Variables for secrets")]
    public int secretsFound;
    public int secretsMissed;

    [Header("FX VARIABLES")]
    public ParticleSystem rewardSpawnFX;
    public ParticleSystem airSpawnFX;


    private void Awake()
    {
        GameModeManager.StartLevelCountOver += ActivateLevel;
        GameModeManager.Success += CountLevelSuccess;
        GameModeManager.Fail += StopSpawningObjects;
    }

    private void Start()
    {
        //Aktivoidaan Gamemodemanageriin oikea kenttä aktiiviseksi - INDEXIN VAIHTO RELEVANTTIA VAIN GAME LEVELISSÄ,
        //BONUS KENTTÄ TOIMII GAME LEVELIN INDEXIIN NOJAUTUEN. KAI :D

        //nämä siirretty startiin ja siirrä takas awakeen jos alkaa tökkii joskus?
        GameModeManager.instance.levelIndex = SceneManager.GetActiveScene().buildIndex;
        string level = GameModeManager.instance.levelName[GameModeManager.instance.levelIndex];
        GameModeManager.instance.ActivateCurrentLevel(level);
        GameModeManager.instance.CutSceneActive();

        GameModeManager.instance.bonusLevelActive = false;
        AudioManager.instance.musicSource.loop = true;
        GameModeManager.instance.secretCurrentForMenu = 0;

        CheckNestObjectForLevel();
        CountStartSecretsInLevel();

        if(GameModeManager.instance.levelIndex == 1)//TUTORIAL LEVEL SPAWN INTERVAL
        {
            groundIntervalSeconds = tutorialTimeGround;
            airIntervalSeconds = tutorialTimeAir;
        }
        else if(GameModeManager.instance.levelIndex != 1)//ALL OTHER LEVELS BESIDES TUTORIAL, SPAWN INTERVAL
        {
            groundIntervalSeconds = levelTimeGround;
            airIntervalSeconds = levelTimeAir;

            //Spawn static point pickups
            StaticPickUpSpawn();
        }

        if(GameModeManager.instance.difficulty == GameModeManager.Difficulty.Normal)
        {
            Spawn1Max = 5;
            Spawn2Max = 5;
            Spawn3Max = 5;
            Spawn4Max = 5;
            pickUpCountVariation = 2;
        }
        if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Hard)
        {
            Spawn1Max = 3;
            Spawn2Max = 3;
            Spawn3Max = 3;
            Spawn4Max = 3;
            pickUpCountVariation = 1;
        }

        //InvokeRepeating("GroundPickUpSpawnRepeat", 0f, 2f);

        StartCoroutine(StartLevelCheck());
    }

    IEnumerator StartLevelCheck()
    {

        AudioManager.instance.PlayMusicFX(1);
        yield return new WaitUntil(() => GameModeManager.instance.cutsceneActive == false);
        GameModeManager.instance.InvokeSecretCountForMenu();

        if (GameModeManager.instance.rewardClaimed == true)
        {
            SpawnRewardItems();
            GameModeManager.instance.RewardLevelInvoke();
            yield return new WaitForSecondsRealtime(1.2f);
            GameModeManager.instance.StartLevelInvoke();
            //GameModeManager.instance.rewardClaimed = false;
        }
        else if(GameModeManager.instance.rewardClaimed == false)
        {
            GameModeManager.instance.StartLevelInvoke();
        }
               
    }

    public void CheckNestObjectForLevel()
    {
        if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Normal)
        {
            nestObjNormal.SetActive(true);
            nestObjHard.SetActive(false);
        }
        if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Hard)
        {
            nestObjNormal.SetActive(false);
            nestObjHard.SetActive(true);
        }
    }

    public void CountStartSecretsInLevel()
    {
        GameObject[] total = GameObject.FindGameObjectsWithTag("Secret");
        for (int i = 0; i < total.Length; i++)
        {
            i = total.Length;
            GameModeManager.instance.secretTotalForMenu = i;
        }
    }
    public void SpawnRewardItems()
    {
        Instantiate(rewardItemPreFab, rewardSpawnPoints[0].transform.position, rewardSpawnPoints[0].transform.rotation);
        Instantiate(rewardItemPreFab, rewardSpawnPoints[1].transform.position, rewardSpawnPoints[1].transform.rotation);
        Instantiate(rewardItemPreFab, rewardSpawnPoints[2].transform.position, rewardSpawnPoints[2].transform.rotation);
        Instantiate(rewardSpawnFX, rewardSpawnPoints[0].transform.position, rewardSpawnPoints[0].transform.rotation);
        Instantiate(rewardSpawnFX, rewardSpawnPoints[1].transform.position, rewardSpawnPoints[1].transform.rotation);
        Instantiate(rewardSpawnFX, rewardSpawnPoints[2].transform.position, rewardSpawnPoints[2].transform.rotation);
    }

    private void ActivateLevel()
    {
        GameModeManager.instance.LevelActive();



        //activate pick up spawning repeat and the time of the interval is seet up on start based on what level it is
        InvokeRepeating("StartSpawnGroundOne", 1f, groundIntervalSeconds);
        InvokeRepeating("StartSpawnGroundTwo", 1f, groundIntervalSeconds);
        InvokeRepeating("StartSpawnAirOne", 1f, airIntervalSeconds);
        InvokeRepeating("StartSpawnAirTwo", 1f, airIntervalSeconds);

        //activate Spawn pick ups on static points based what level it is
        //InvokeRepeating("AirPickUpSpawnRepeat", 0f, 2f);

        StartMusic();
    }

    void StartMusic()
    {
        //valitaan kyseisen kentän musiikki
        if (GameModeManager.instance.levelIndex > 0 && GameModeManager.instance.levelIndex <= 3)
        { AudioManager.instance.PlayMusicFX(4); }
        if (GameModeManager.instance.levelIndex > 3 && GameModeManager.instance.levelIndex <= 6)
        { Debug.Log("musaa toiseen maailmaan puuttuu"); }
        if (GameModeManager.instance.levelIndex > 6 && GameModeManager.instance.levelIndex <= 9)
        { Debug.Log("musaa kolmanteen maailmaan puuttuu"); }
    }
    //LEFT SIDE OF NEST GROUND SPAWN SET UP
    public void StartSpawnGroundOne()
    {
        Vector2 min = new Vector2(groundMinX1, 0);
        Vector2 max = new Vector2(groundMaxX1, groundLevelY);
        float x = Random.Range(min.x, max.x);
        float y = max.y;

        spawnPos1.transform.position = new Vector2(x, y);
        GroundPickUpSpawnRepeat(spawnPos1);
    }
    //RIGHT SIDE OF NEST GROUND SPAWN SET UP
    public void StartSpawnGroundTwo()
    {
        Vector2 min = new Vector2(groundMinX2, 0);
        Vector2 max = new Vector2(groundMaxX2, groundLevelY);
        float x = Random.Range(min.x, max.x);
        float y = max.y;

        spawnPos2.transform.position = new Vector2(x, y);
        GroundPickUpSpawnRepeat(spawnPos2);
    }
    //LEFT SIDE OF NEST AIR SPAWN SET UP
    public void StartSpawnAirOne()
    {
        Vector2 min = new Vector2(airMinX1, airLevelMinY);
        Vector2 max = new Vector2(airMaxX1, airLevelMaxY);
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);

        spawnPos1Air.transform.position = new Vector2(x, y);
        AirPickUpSpawnRepeat(spawnPos1Air);
    }
    //RIGHT SIDE OF NEST AIR SPAWN SET UP
    public void StartSpawnAirTwo()
    {
        Vector2 min = new Vector2(airMinX2, airLevelMinY);
        Vector2 max = new Vector2(airMaxX2, airLevelMaxY);
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);

        spawnPos2Air.transform.position = new Vector2(x, y);
        AirPickUpSpawnRepeat(spawnPos2Air);
    }

    //GROUND PICK UP SPAWNING REPEAT
    public void GroundPickUpSpawnRepeat(Transform spawnPos)
    {
        GameObject clone = pickupPrefabGround[Random.Range(0, pickupPrefabGround.Length)];

        if (clone.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).CompareTag("Ground1PU"))
        { 
            if(pickUp1Spawn > Spawn1Max)
            { pickUp1Spawn = Spawn1Max; }
            else
            {
                Instantiate(clone, spawnPos.transform.position, spawnPos.transform.rotation);
                clone.GetComponent<CollectableCollision>().isAppeared = true;
                pickUp1Spawn += 1;
            }
        }
        if (clone.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).CompareTag("Ground2PU"))
        { 
            if (pickUp2Spawn >= Spawn2Max)
            { pickUp2Spawn = Spawn2Max; }
            else
            {
                Instantiate(clone, spawnPos.transform.position, spawnPos.transform.rotation);
                clone.GetComponent<CollectableCollision>().isAppeared = true;
                pickUp2Spawn += 1;
            }
        }
        if (clone.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).CompareTag("Ground3PU"))
        { 
            if (pickUp3Spawn >= Spawn3Max)
            { pickUp3Spawn = Spawn3Max; }
            else
            {
                Instantiate(clone, spawnPos.transform.position, spawnPos.transform.rotation);
                clone.GetComponent<CollectableCollision>().isAppeared = true;
                pickUp3Spawn += 1;
            }
        }
        if (clone.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).CompareTag("Ground4PU"))
        { 
            if (pickUp4Spawn >= Spawn4Max)
            { pickUp4Spawn = Spawn4Max; }
            else
            {
                Instantiate(clone, spawnPos.transform.position, spawnPos.transform.rotation);
                clone.GetComponent<CollectableCollision>().isAppeared = true;
                pickUp4Spawn += 1;
            }
        }
    }
    //AIR PICK UP SPAWNING REPEAT
    public void AirPickUpSpawnRepeat(Transform spawnPosAir)
    {
        GameObject clone = pickupPrefabAir[Random.Range(0, pickupPrefabAir.Length)];

        Instantiate(airSpawnFX, spawnPosAir.transform.position, spawnPosAir.transform.rotation);
        Instantiate(clone, spawnPosAir.transform.position, spawnPosAir.transform.rotation);

    }

    //Vanha systeemi alku spawn tiettyihin kohttiin PUFF
    public void StaticPickUpSpawn()
    {
        while (0 < pickUpCountVariation)
        {
            for (int i = 0; i < pickupSpawnPointsStatic.Length; i++)
            {
                GameObject pickupRange = pickupPrefabStatic[Random.Range(0, pickupPrefabStatic.Length)];
                Instantiate(pickupRange, pickupSpawnPointsStatic[i].transform.position, pickupSpawnPointsStatic[i].transform.rotation);
                pickUpStaticList.Add(pickupRange);
            }
            pickUpCountVariation--;
        }
    }


    public void CountLevelSuccess()
    {
        StopSpawningObjects();
        //DestroyPickUpsWithTag();
        SecretsCheck();
        PointsCheck(); 
    }
    public void SecretsCheck()
    {
        secretsInScene = GameObject.FindGameObjectsWithTag("Secret");
        secretsFoundInScene = GameObject.FindGameObjectsWithTag("SecretFound");
        for (int i = 0; i < secretsFoundInScene.Length; i++)
        {
            i = secretsFoundInScene.Length;
            secretsFound = i;
            GameModeManager.instance.secretFoundTemp = i;      
        }
        for (int i = 0; i < secretsInScene.Length; i++)
        {
            i = secretsInScene.Length;
            secretsMissed = i;
            GameModeManager.instance.secretMissedTemp = i;
        }
        GameModeManager.instance.secretTotalTemp = secretsFound + secretsMissed;
        GameModeManager.instance.SecretsCheck(secretsFound, secretsMissed);
    }

    public void PointsCheck()
    { 
        GameModeManager.instance.HighScoreCheck();
    }

    /*
    //NÄITÄ EI KÄYTETÄ MUTTA TARVITTAESSA JOO, JÄTETÄÄN VIELÄ, tuhoaa objectit esim pesän ympäriltä
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
    */

    public void StopSpawningObjects()
    { 
        CancelInvoke(); 
    }

    private void OnDestroy()
    {
        //Reset variables which only count in current level
        //GameModeManager.instance.levelScore = 0;
        secretsFound = 0;
        secretsMissed = 0;
        GameModeManager.instance.secretCurrentForMenu = 0;
        GameModeManager.instance.secretTotalForMenu = 0;
        GameModeManager.instance.secretFoundTemp = 0;
        GameModeManager.instance.secretMissedTemp = 0;
        GameModeManager.instance.secretTotalTemp = 0;
    }

}
