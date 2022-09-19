using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Damage : MonoBehaviour
{
    // health each tank starts with
    public float m_StartingHealth = 100f;

    // a prefab that will be instantiated in Awake, then used whenever the tank dies
    public GameObject m_ExplosionPrefab;

    public float m_CurrentHealth;
    private bool m_Dead;
    // the particle  system that will play when the tank is destroyed
    private ParticleSystem m_ExplosionParticles;

    private void Awake()
    {
        //instantiate the explosion prefab and get a reference to the particle system on it
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        //disable the prefab so it can be activated when its required
        m_ExplosionParticles.gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        //when the tank is enabled, reset the tanks health and whether or not it is dead
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }
    private void SetHealthUI()
    {
        //todo update the user interface showing the tanks hp
    }
    public void TakeDamage(float amount)
    {
        //reduce current hp by the amount of damage done
        m_CurrentHealth -= amount;

        //change the ui elements appropriately
        SetHealthUI();

        //if the current health is at or below 0 and it has not yet been registered, call OnDeath
        if(m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }
    private void OnDeath()
    {
        //set the flag so that this function is only called once
        m_Dead = true;

        //move the instantiated exploision prefab to the tanks position and turn it on
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        //play the particle system of the tank exploding
        m_ExplosionParticles.Play();

        EnemyTankMovement enemyMovement = GetComponent<EnemyTankMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.DisableFollow();
        }

        EnemyFiring enemyFiring = GetComponent<EnemyFiring>();
        if (enemyFiring != null)
        {
            enemyFiring.DisableFire();
        }
        //turn the tank off
        gameObject.SetActive(false);
    }






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
