using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject playerTank;
    public GameObject enemyPrefab;
    public GameObject littleTank;
    public GameObject turretTank;
    public GameObject cloakerTank;
    public GameObject birtha;
    public GameObject bossTank;

    public GameObject lighting;

    public int wave = 0;

    public Transform spawnPointsParent;
    List<Vector3> spawnPoints = new List<Vector3> ();

    public Text waveCounter;

    public GameManager gameManager;

    public List<GameObject> allTanks = new List<GameObject>();




    public void Start()
    {
    }

    private void Awake()
    {
        allTanks.Add(enemyPrefab);
        allTanks.Add(littleTank);
        allTanks.Add(turretTank);
        allTanks.Add(cloakerTank);
        allTanks.Add(birtha);

        // loop to run through each potential spawn point
        foreach (Transform spawnPoint in spawnPointsParent)
        {
            spawnPoints.Add(spawnPoint.position);
        }
    }


    public void NextWave()
    {
        // on next wave, wave goes up by 1
        wave += 1;

        for (int i = 1; i < gameManager.m_Tanks.Count; i++)
        {
            Destroy(gameManager.m_Tanks[i]);
        }

        if (wave >= 5)
        {
            lighting.GetComponent<Light>().color = new Color(0.6500f, 0.6500f, 0.6500f, 1);
        }

        gameManager.m_Tanks = new List<GameObject>();
        gameManager.m_Tanks.Add(playerTank);
        
        
        playerTank.SetActive(true);

        // on new wave, check how many tanks, if its less than the number of the wave, spawn 1 more at the potential spawn points.

        if (wave <= 9) {
            for (int i = 1; i <= wave; i++)
            {
                gameManager.m_Tanks.Add(Instantiate(allTanks[Random.Range(0, allTanks.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)], Quaternion.identity));
            }
        }
        else
        {
            gameManager.m_Tanks.Add(Instantiate(bossTank, spawnPoints[Random.Range(0, spawnPoints.Count)], Quaternion.identity));
            lighting.GetComponent<Light>().color = new Color(0.2358f, 0.2358f, 0.2358f, 1);
        }

    }

    // on death, wave becomes 0
    public void PlayerDeath()
    {
        wave = 0;
    }

    public void Update()
    {
        // displays wave counter at the top of scene
        waveCounter.text = "Wave: " + wave;
    }
}
