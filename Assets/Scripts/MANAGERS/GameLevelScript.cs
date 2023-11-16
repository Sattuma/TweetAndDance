using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ACTIVATE FUNCTIONS WHICH ARE NEEDED ONLY ON !!GAMELEVEL!!
//PURPOSE OF THIS SCRIPT IS COUNT TEMPORARILY INFO AND FUNCTIONS WHICH GOES TO GAME MANAGER IF NEEDED WERE STORED 
public class GameLevelScript : MonoBehaviour
{
    public GameObject[] pickupPrefab;
    public GameObject[] pickupSpawnPoints;

    public List<GameObject> pickUpGroundList = new();
    public int pickUpCountVariation;

    public GameObject[] pickupSpawnPointsAir;
    public GameObject[] pickupsInScene;

    public GameObject[] secretsFoundInScene;
    public GameObject[] secretsInScene;
    public int secretsFound;
    public int secretsMissed;


    private void Awake()
    {


        GameModeManager.instance.bonusLevelActive = false;

        GameModeManager.StartLevelCountOver += ActivateLevel;
        //Aktivoidaan Gamemodemanageriin oikea kenttä aktiiviseksi - INDEXIN VAIHTO RELEVANTTIA VAIN GAME LEVELISSÄ,
        //BONUS KENTTÄ TOIMII GAME LEVELIN INDEXIIN NOJAUTUEN. KAI :D
        GameModeManager.instance.levelIndex = SceneManager.GetActiveScene().buildIndex;
        string level = GameModeManager.instance.levelName[GameModeManager.instance.levelIndex];
        GameModeManager.instance.ActivateCurrentLevel(level);
        GameModeManager.instance.CutSceneActive();
        GameModeManager.Success += CountLevelSuccess;

        AudioManager.instance.musicSource.loop = true;

        //valitaan kyseisen kentän musiikki
        if (GameModeManager.instance.levelIndex > 0 && GameModeManager.instance.levelIndex <= 3)
        { AudioManager.instance.PlayMusicFX(1); }
        if (GameModeManager.instance.levelIndex > 3 && GameModeManager.instance.levelIndex <= 6)
        { Debug.Log("musaa toiseen maailmaan puuttuu"); }
        if (GameModeManager.instance.levelIndex > 6 && GameModeManager.instance.levelIndex <= 9)
        { Debug.Log("musaa kolmanteen maailmaan puuttuu"); }
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

        if (GameModeManager.instance.rewardClaimed == true)
        {
            SpawnRewardItems();
            GameModeManager.instance.StartLevelInvoke();
            yield return new WaitForSecondsRealtime(2f);
            GameModeManager.instance.rewardClaimed = false;

        }
        else if(GameModeManager.instance.rewardClaimed == false)
        {
            GameModeManager.instance.StartLevelInvoke();
        }
                
            


    }
    private void ActivateLevel()
    { GameModeManager.instance.LevelActive(); }


    public void SpawnRewardItems()
    {
        Debug.Log("SPAWNAAN REWARDIN");
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
        //GameModeManager.instance.levelScore = 0;
        secretsFound = 0;
        secretsMissed = 0;
        GameModeManager.instance.secretFoundTemp = 0;
        GameModeManager.instance.secretMissedTemp = 0;
        GameModeManager.instance.secretTotalTemp = 0;
    }

}
