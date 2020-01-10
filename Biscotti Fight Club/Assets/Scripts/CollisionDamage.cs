using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 5f;
    [SerializeField] private float takeDamageInterval = 3f;
    [SerializeField] private string compareTag = "Enemy";
    [SerializeField] private HealthSystem healthSystem;

    private bool collided = false;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        if(healthSystem == null) 
            healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timePassed - takeDamageInterval > 0f && collided)
        {
            collided = false;
            timePassed = 0f;
        }
        timePassed += Time.unscaledDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(compareTag) && !collided)
        {
            collided = true;
            healthSystem.Health(-damageAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(compareTag) && !collided)
        {
            collided = true;
            healthSystem.Health(-damageAmount);
        }
    }

}
