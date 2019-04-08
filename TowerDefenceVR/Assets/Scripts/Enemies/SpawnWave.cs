using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour {

    public Transform enemy;
    public GameObject spawnPoint;
    public float spawnCooldown = 8f;
    private float timer = 0f;

    public int wave = 0;

    private void Update()
    {

        if (wave > 25) return;
        if (timer <= 0)
        {
            StartCoroutine(SpawnEnemies());
            timer = spawnCooldown;
        }

        timer -= Time.deltaTime;
    }

    private IEnumerator SpawnEnemies()
    {
        wave++;
        for (int i = 0; i < wave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawn = EnemyAI.RandomPointInBounds(spawnPoint.GetComponent<BoxCollider>().bounds);
        spawn = new Vector3(spawn.x, -1, spawn.z);
        Instantiate(enemy, spawn, Quaternion.identity);
    }
}
