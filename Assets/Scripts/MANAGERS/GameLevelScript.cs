using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ACTIVATE FUNCTIONS WHICH ARE NEEDED ONLY ON !!GAMELEVEL!!
//PURPOSE OF THIS SCRIPT IS COUNT TEMPORARILY INFO AND FUNCTIONS WHICH GOES TO GAME MANAGER IF NEEDED WERE STORED 
public class GameLevelScript : MonoBehaviour
{

    [Header("GameObject Nest for Level")]
    public GameObject nestObjNormal;
    public GameObject nestObjHard;

    [Header("GameObejcts To SPawn In level")]
    public GameObject[] pickupPrefabGround;
    public GameObject[] pickupPrefabAir;
    public GameObject rewardItemPreFab;

    [Header("SpawnPoints")]
    public Transform spawnPos1;
    public Transform spawnPos2;
    public Transform spawnPos1Air;
    public Transform spawnPos2Air;
    public GameObject[] rewardSpawnPoints;

    [Header("PickUps CURRENT/MAX in level")]
    public int pickUp1Spawn;
    public int pickUp2Spawn;
    public int pickUp3Spawn;
    public int pickUp4Spawn;
    public int Spawn1Max;
    public int Spawn2Max;
    public int Spawn3Max;
    public int Spawn4Max;

    [Header("spawn values pick up in level")]
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

    //public GameObject[] pickupSpawnPointsGround;
    //public GameObject[] pickupSpawnPointsAir;
    //public List<GameObject> pickUpGroundList = new();
    //public int pickUpCountVariation;


    private void Awake()
    {
        GameModeManager.StartLevelCountOver += ActivateLevel;
        GameModeManager.Success += CountLevelSuccess;
        GameModeManager.Fail += StopSpawningObjects;

        //Aktivoidaan Gamemodemanageriin oikea kenttä aktiiviseksi - INDEXIN VAIHTO RELEVANTTIA VAIN GAME LEVELISSÄ,
        //BONUS KENTTÄ TOIMII GAME LEVELIN INDEXIIN NOJAUTUEN. KAI :D

        GameModeManager.instance.levelIndex = SceneManager.GetActiveScene().buildIndex;
        string level = GameModeManager.instance.levelName[GameModeManager.instance.levelIndex];
        GameModeManager.instance.ActivateCurrentLevel(level);
        GameModeManager.instance.CutSceneActive();       
    }



    private void Start()
    {
        GameModeManager.instance.bonusLevelActive = false;
        AudioManager.instance.musicSource.loop = true;
        GameModeManager.instance.secretCurrentForMenu = 0;

        CheckNestObjectForLevel();

        CountStartSecretsInLevel();

        //GroundSpawnPickupLevel();
        //StartInvokeRepeating();

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
            GameModeManager.instance.rewardClaimed = false;
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
        InvokeRepeating("StartSpawnGroundOne", 0f, 4f);
        InvokeRepeating("StartSpawnGroundTwo", 0f, 5);
        InvokeRepeating("StartSpawnAirOne", 0f, 8f);
        InvokeRepeating("StartSpawnAirTwo", 0f, 8f);
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

    public void StartSpawnGroundOne()
    {
        Vector2 min = new Vector2(groundMinX1, 0);
        Vector2 max = new Vector2(groundMaxX1, groundLevelY);
        float x = Random.Range(min.x, max.x);
        float y = max.y;

        spawnPos1.transform.position = new Vector2(x, y);
        GroundPickUpSpawnRepeat(spawnPos1);
    }
    public void StartSpawnGroundTwo()
    {
        Vector2 min = new Vector2(groundMinX2, 0);
        Vector2 max = new Vector2(groundMaxX2, groundLevelY);
        float x = Random.Range(min.x, max.x);
        float y = max.y;

        spawnPos2.transform.position = new Vector2(x, y);
        GroundPickUpSpawnRepeat(spawnPos2);
    }
    public void StartSpawnAirOne()
    {
        Vector2 min = new Vector2(airMinX1, airLevelMinY);
        Vector2 max = new Vector2(airMaxX1, airLevelMaxY);
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);

        spawnPos1Air.transform.position = new Vector2(x, y);
        AirPickUpSpawnRepeat(spawnPos1Air);
    }
    public void StartSpawnAirTwo()
    {
        Vector2 min = new Vector2(airMinX2, airLevelMinY);
        Vector2 max = new Vector2(airMaxX2, airLevelMaxY);
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);

        spawnPos2Air.transform.position = new Vector2(x, y);
        AirPickUpSpawnRepeat(spawnPos2Air);
    }

    //maasta spawnautuu yksitellen maa pickupit
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
    public void AirPickUpSpawnRepeat(Transform spawnPosAir)
    {
        GameObject clone = pickupPrefabAir[Random.Range(0, pickupPrefabAir.Length)];

        Instantiate(airSpawnFX, spawnPosAir.transform.position, spawnPosAir.transform.rotation);
        Instantiate(clone, spawnPosAir.transform.position, spawnPosAir.transform.rotation);

    }

    /*
    //Vanha systeemi alku spawn tiettyi8hin kohittin PUFF
    public void GroundSpawnPickupLevel()
    {
        while (0 < pickUpCountVariation)
        {
            for (int i = 0; i < pickupSpawnPointsGround.Length; i++)
            {
                GameObject pickupRange = pickupPrefabGround[Random.Range(0, pickupPrefabGround.Length)];
                Instantiate(pickupRange, pickupSpawnPointsGround[i].transform.position, pickupSpawnPointsGround[i].transform.rotation);
                pickUpGroundList.Add(pickupRange);
            }
            pickUpCountVariation--;
        }
    }
    */

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
