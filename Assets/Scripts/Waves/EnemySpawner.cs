using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject playerTank;
    public GameObject enemyPrefab;

    public int wave = 0;

    public GameManager gameManager;
    public void NextWave()
    {
        wave += 1;

        for (int i = 1; i < gameManager.m_Tanks.Length; i++)
        {
            Destroy(gameManager.m_Tanks[i]);
        }

        gameManager.m_Tanks = new GameObject[wave + 1];
        gameManager.m_Tanks[0] = playerTank;

        playerTank.SetActive(true);
        for (int i = 1; i <= wave; i++)
        {
            gameManager.m_Tanks[i] = Instantiate(enemyPrefab);
        }
    }

    public void PlayerDeath()
    {
        wave = 0;
    }
}
