using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float SpawnCost;
    public float health;
    private void Start()
    {
        health = 10;
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        if(health <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("EnemySpawner").GetComponent<EnemyWaveSpawner>().enemiesInScene--;
        }
    }
}
