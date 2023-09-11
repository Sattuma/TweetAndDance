using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelScript : MonoBehaviour
{

    public GameObject[] pickupPrefab;
    public GameObject[] pickupSpawnPoints;
    public GameObject[] pickupSpawnPointsAir;
    public GameObject[] pickupsInScene;


    private void Awake()
    {
        AudioManager.instance.PlayMusicFX(3);
    }

    private void Start()
    {
        GroundSpawnPickupLevel();
        //StartInvokeRepeating();
    }

    public void GroundSpawnPickupLevel()
    {
        for(int i = 0; i < pickupSpawnPoints.Length; i++)
        {
            Debug.Log(i);
            Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)], 
                pickupSpawnPoints[i].transform.position, pickupSpawnPoints[i].transform.rotation);
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
        { Destroy(pickupsInScene[i]); }
    }

    public void StopInvoke()
    {
        CancelInvoke();
    }

    public void LevelOneCleared()
    {
        StopInvoke();
        DestroyPickUpsWithTag();
    }
}
