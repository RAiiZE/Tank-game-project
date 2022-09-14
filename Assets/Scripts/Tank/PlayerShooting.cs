using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // prefab of the shell
    public Rigidbody m_Shell;
    // a child of the tank when the shells are spawned
    public Transform m_FireTransform;
    // the force given to the shell when fired
    public float m_LaunchForce = 30f;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;

    private bool dualFire = false;
    public GameObject secondSpawner;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }
    private void Fire()
    {
        //create an instance of the shell and store a reference to its rigidbody
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        //Set the shell's velocity to launch force in the fire positions forward direction
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;

        // shooting from second spawner
        if (dualFire)
        {
            
            shellInstance = Instantiate(m_Shell, secondSpawner.transform.position, secondSpawner.transform.rotation) as Rigidbody;
            shellInstance.velocity = m_LaunchForce * secondSpawner.transform.forward;
        }
     }

    // picking up power ups
    private void OnTriggerEnter(Collider other)
    {
        // Rapid fire upgrade
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            fireRate = 0f;
            StartCoroutine("WaitTimeFire");
        }

        // Dual fire upgrade
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            dualFire = true;
            secondSpawner.SetActive(true);
            StartCoroutine("WaitTimeDual");
        }

        // Sniper shot upgrade
        if (other.gameObject.layer == 11)
        {
            Destroy(other.gameObject);
            m_LaunchForce = 80f;
            StartCoroutine("WaitTimeForce");

        }
    }

    // timers for each power ups
    IEnumerator WaitTimeFire()
    {
        yield return new WaitForSeconds (10);
        fireRate = 0.5f;
    }
    IEnumerator WaitTimeDual()
    {
        yield return new WaitForSeconds(10);
        dualFire = false;
        secondSpawner.SetActive(false);
    }

    IEnumerator WaitTimeForce()
    {
        yield return new WaitForSeconds(10);
        m_LaunchForce = 30f;
    }
}
