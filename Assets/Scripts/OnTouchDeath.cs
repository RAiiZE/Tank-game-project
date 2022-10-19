using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchDeath : MonoBehaviour
{
    public GameManager gameManager;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.tag != "Player"|| other.tag != "Rock")
        {
            gameManager.RemoveTank(other.gameObject);
            Destroy(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
