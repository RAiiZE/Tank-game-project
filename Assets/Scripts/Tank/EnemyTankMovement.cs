using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{ 
    // the tank will stop moving towards the player once it reaches this distance
    public float m_CloseDistance = 8f;
    // the tanks turret object
    public Transform m_Turret;

    // a reference to the player - this will be set when the enemy is loaded
    private GameObject m_Player;
    // a reference to the nav mesh agent
    private NavMeshAgent m_NavAgent;
    // a reference to the rigidbody
    private Rigidbody m_Rigidbody;
    // will be set to true when this tank should follow the player
    private bool m_Follow;
     Transform WaypointsParent;
    List<Vector3> wayPoints = new List<Vector3>();
    float wayPointTimer = 0;


    public bool canMove = true;

    private void Awake()
    {
        
        WaypointsParent = GameObject.Find("EnemyWaypoints").transform;
        foreach (Transform wayPoint in WaypointsParent)
        {
            wayPoints.Add(wayPoint.position);
        }

        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = false;
        m_NavAgent.isStopped = false;
        // picks a random waypoint on the map and enemy will begin to move towards it
        m_NavAgent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)]);
    }

    public void DisableFollow()
    {
        m_Follow = false;
    }
    private void OnEnable()
    {
        // when the tank is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Follow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Follow = false;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (!canMove)
        {
            m_NavAgent.isStopped = true;
        }
        if (m_Follow == false) // not following player
        {
            wayPointTimer += Time.deltaTime;
            
            if (wayPointTimer > 5)
            {
                m_NavAgent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)]);
                wayPointTimer = 0;
            }
        }
        else // following player
        {
            // get distance from player to enemy tank
            float distance = (m_Player.transform.position - transform.position).magnitude;
            // if distance is less than stop distance, then stop moving
            if (distance > m_CloseDistance && canMove)
            {
                
                m_NavAgent.SetDestination(m_Player.transform.position);
                m_NavAgent.isStopped = false;
            }
            else
            {
                m_NavAgent.isStopped = true;
            }
            if (m_Turret != null)
            {
                m_Turret.LookAt(m_Player.transform);
            }
        }
        





    }
}
