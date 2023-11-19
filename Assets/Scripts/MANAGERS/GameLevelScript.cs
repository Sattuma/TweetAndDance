using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ACTIVATE FUNCTIONS WHICH ARE NEEDED ONLY ON !!GAMELEVEL!!
//PURPOSE OF THIS SCRIPT IS COUNT TEMPORARILY INFO AND FUNCTIONS WHICH GOES TO GAME MANAGER IF NEEDED WERE STORED 
public class GameLevelScript : MonoBehaviour
{
    [Header("GameObejcts To SPawn In level")]
    public GameObject[] pickupPrefabGround;
    public GameObject[] pickupPrefabAir;
    public GameObject rewardItemPreFab;

    [Header("SpawnPoints")]
    public GameObject[] pickupSpawnPointsGround;
    public GameObject[] pickupSpawnPointsAir;
    public GameObject[] rewardSpawnPoints;
    public ParticleSystem rewardSpawnFX;

    public List<GameObject> pickUpGroundList = new();
    public int pickUpCountVariation;

    [Header("Variables for Pick ups")]

    public int pickUp1Spawn;
    public int pickUp2Spawn;
    public int pickUp3Spawn;
    public int pickUp4Spawn;

    public int Spawn1Max;
    public int Spawn2Max;
    public int Spawn3Max;
    public int Spawn4Max;

    [Header("Variables for pick ups in scene")]
    public GameObject[] pickupsInScene;
    public GameObject[] secretsFoundInScene;
    public GameObject[] secretsInScene;

    [Header("Variables for secrets")]
    public int secretsFound;
    public int secretsMissed;



    private void Awake()
    {
        GameModeManager.StartLevelCountOver += ActivateLevel;
        GameModeManager.Success += CountLevelSuccess;

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

        //GroundSpawnPickupLevel();
        //StartInvokeRepeating();

        //InvokeRepeating("GroundPickUpSpawnRepeat", 0f, 2f);

        StartCoroutine(StartLevelCheck());
    }

    IEnumerator StartLevelCheck()
    {

        AudioManager.instance.PlayMusicFX(1);
        yield return new WaitUntil(() => GameModeManager.instance.cutsceneActive == false);
        AudioManager.instance.musicSource.Stop();

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
        InvokeRepeating("GroundPickUpSpawnRepeat", 0f, 2f);
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

    //maasta spawnautuu yksitellen maa pickupit
    public void GroundPickUpSpawnRepeat()
    {
        GameObject clone = pickupPrefabGround[Random.Range(0, pickupPrefabGround.Length)];


        if (clone.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).CompareTag("Ground1PU"))
        { 

            if(pickUp1Spawn > Spawn1Max)
            { pickUp1Spawn = Spawn1Max; }
            else
            {
                Instantiate(clone, pickupSpawnPointsGround[3].transform.position, pickupSpawnPointsGround[3].transform.rotation);
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
                Instantiate(clone, pickupSpawnPointsGround[3].transform.position, pickupSpawnPointsGround[3].transform.rotation);
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
                Instantiate(clone, pickupSpawnPointsGround[3].transform.position, pickupSpawnPointsGround[3].transform.rotation);
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
                Instantiate(clone, pickupSpawnPointsGround[3].transform.position, pickupSpawnPointsGround[3].transform.rotation);
                clone.GetComponent<CollectableCollision>().isAppeared = true;
                pickUp4Spawn += 1;
            }
        }
    }

    //Lehdille & koivunoksille?
    public void AirPickupSpawnRepeat()
    {
        Instantiate(pickupPrefabGround[(5)], pickupSpawnPointsAir[Random.Range(0, 2)].transform.position, pickupSpawnPointsAir[Random.Range(0, 2)].transform.rotation);
        Instantiate(pickupPrefabGround[(5)], pickupSpawnPointsAir[Random.Range(6, 7)].transform.position, pickupSpawnPointsAir[Random.Range(6, 7)].transform.rotation);

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
    
    //NÄITÄ EI KÄYTETÄ MUTTA TARVITTAESSA JOO, JÄTETÄÄN VIELÄ
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

    public void CountLevelSuccess()
    {
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
    { GameModeManager.instance.HighScoreCheck();}

    public void LevelCleared()
    {
        StopInvoke();
        //DestroyPickUpsWithTag();
    }
    public void StopInvoke()
    { CancelInvoke(); }

    private void OnDestroy()
    {
        //Reset variables which only count in current level
        //GameModeManager.instance.levelScore = 0;
        secretsFound = 0;
        secretsMissed = 0;
        GameModeManager.instance.secretFoundTemp = 0;
        GameModeManager.instance.secretMissedTemp = 0;
        GameModeManager.instance.secretTotalTemp = 0;
    }

}
