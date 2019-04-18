using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour {

    public Transform meleeEnemy;
    public Transform rangedEnemy;
    public Transform siegeBrute;
    public Transform rhino;
    public GameObject spawnPoint;
    public float spawnCooldown = 8f;
    private float timer = 0f;
    private Transform enemyToSpawn;

    public int wave = 0;
    private int round = 0;

    private void Update()
    {
        Debug.Log("wave" + wave);
        if (wave > round)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                GameObject.Find("FPPlayer").GetComponent<PlayerActions>().EndRound();
                Debug.Log("end round");
            }
            return;
        }
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

    public void SetRoundLimit(int limit)
    {
        round = limit;
        timer = 0f;
    }

    public void SpawnEnemy()
    {
        ChooseEnemy();
        Vector3 spawn = EnemyAI.RandomPointInBounds(spawnPoint.GetComponent<BoxCollider>().bounds);
        spawn = new Vector3(spawn.x, -1, spawn.z);
        Instantiate(enemyToSpawn, spawn, Quaternion.identity);
    }

    private void ChooseEnemy()
    {
        if (round < 5) enemyToSpawn = meleeEnemy;
        else if (round < 10)
        {
            if (Random.value < 0.6) enemyToSpawn = meleeEnemy;
            else enemyToSpawn = rangedEnemy;
        }
        else if (round < 15)
        {
            if (Random.value < 0.4) enemyToSpawn = meleeEnemy;
            else if (Random.value < 0.7) enemyToSpawn = rangedEnemy;
            else enemyToSpawn = siegeBrute;
        }
        else if (round < 20)
        {
            if (Random.value < 0.3) enemyToSpawn = meleeEnemy;
            else if (Random.value < 0.6) enemyToSpawn = rangedEnemy;
            else if (Random.value < 0.8) enemyToSpawn = siegeBrute;
            else enemyToSpawn = rhino;
        }
    }
}
