using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject playerTank;
    public GameObject enemyPrefab;

    public int wave = 0;

    public Transform spawnPointsParent;
    List<Vector3> spawnPoints = new List<Vector3> ();

    public GameManager gameManager;

    private void Awake()
    {
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

        for (int i = 1; i < gameManager.m_Tanks.Length; i++)
        {
            Destroy(gameManager.m_Tanks[i]);
        }

        gameManager.m_Tanks = new GameObject[wave + 1];
        gameManager.m_Tanks[0] = playerTank;
        
        
        playerTank.SetActive(true);

        // on new wave, check how many tanks, if its less than the number of the wave, spawn 1 more at the potential spawn points.
        for (int i = 1; i <= wave; i++)
        {
            gameManager.m_Tanks[i] = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)], Quaternion.identity);
        }
    }

    // on death, wave becomes 0
    public void PlayerDeath()
    {
        wave = 0;
    }
}
