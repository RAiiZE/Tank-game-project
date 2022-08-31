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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonUp("Fire1"))
        {
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
}
