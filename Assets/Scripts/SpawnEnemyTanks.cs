using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyTanks : MonoBehaviour
{
    public GameObject BabyTank;
    // the place at which the tanks can spawn
    public Transform BirthLocation;
    public float timer;
    public float spawnDelay;
    // Is able to spawn another tank
    public bool CanBirth;

    GameObject gameManager;
    





    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        // amount of time before spawning in each child tank
        timer += Time.deltaTime;
        if (timer > spawnDelay && CanBirth)
        {

            timer = 0;
            if(GameObject.FindGameObjectWithTag("Player") != null)
            {
                gameManager.GetComponent<GameManager>().m_Tanks.Add(Instantiate(BabyTank, BirthLocation.position, Quaternion.identity));
            }
            
        }


    }
   // Makes it so the tank can only spawn babys while chasing the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanBirth = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanBirth = false;
        }
    }

}
