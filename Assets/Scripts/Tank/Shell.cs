using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float m_MaxLifeTime = 2f;
    public float m_MaxDamage = 34f;
    public float m_ExplosionRadius = 5;
    public float m_ExplosionForce = 100f;
    public ParticleSystem m_ExplosionParticles;

    public float CalculateDamage(Vector3 targetPosition)
    {
        //create a vector from the shell to the target
        Vector3 explosionToTarget = targetPosition - transform.position;

        //calculate the distance from the shell ot the target
        float exploisionDistance = explosionToTarget.magnitude;

        //calculate the proportion of the maximum distance the target is away
        float relativeDistance = (m_ExplosionRadius - exploisionDistance) / m_ExplosionRadius;

        // calculate damage as this proportion of the maximum possible damage
        float damage = relativeDistance * m_MaxDamage;

        // make sure that the minimum damage is always 0
        damage = Mathf.Max(0f, damage);

        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

        //only tanks will have rigidbody scripts
        if (targetRigidbody != null)
        {
            // add an explosion force
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
            //find the tankhealth script associated with the rigidbody
            Damage targetHealth = targetRigidbody.GetComponent<Damage>();

            if (targetHealth != null)
            {
                //calculate the amount of damage the target should take based on its distance from the shell
                float damage = CalculateDamage(targetRigidbody.position);
                targetHealth.TakeDamage(damage);

                   
            }
        }



        // unparent the particles from the shell
        m_ExplosionParticles.transform.parent = null;
        // play the particle system
        m_ExplosionParticles.Play();
        // once the particles have finished, destroy the gameObject they are on
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        // destroy the shell
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
