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
     }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            fireRate = 0f;
            StartCoroutine("waitTime");
        }
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds (10);
        fireRate = 0.5f;
    }
}
