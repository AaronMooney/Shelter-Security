using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Aaron Mooney
 * 
 * SpawnWave script that spawns enemies while a round is active
 * */
public class SpawnWave : MonoBehaviour {

    [Header("Enemies")]
    public Transform meleeEnemy;
    public Transform rangedEnemy;
    public Transform siegeBrute;
    public Transform rhino;
    public Transform drone;

    [Header("Setup fields")]
    public GameObject spawnPoint;
    public float spawnCooldown = 8f;
    public int wave = 0;
    public int round = 0;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject VRConsole;

    private float timer = 0f;
    private Transform enemyToSpawn;
    private bool VR;

    private void Start()
    {
        // check if VR
        VR = VRConfig.VREnabled;
    }

    private void Update()
    {
        // if the wave number is greater than the round number then wait until no enemies are left and end the round
        if (wave > round)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Aerial").Length == 0)
            {
                if (!VR)
                    player.GetComponent<PlayerActions>().EndRound();
                else
                    VRConsole.GetComponent<VRWaveManager>().EndRound();
            }
            return;
        }

        // Spawn enemies on a cooldown
        if (timer <= 0)
        {
            StartCoroutine(SpawnEnemies());
            timer = spawnCooldown;
        }

        timer -= Time.deltaTime;
    }

    // Spawn enemies coroutine
    private IEnumerator SpawnEnemies()
    {
        wave++;
        for (int i = 0; i < wave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Set the round and reset timer
    public void SetRoundLimit(int limit)
    {
        round = limit;
        timer = 0f;
    }

    // Spawn an enemy at a random point in spawner collider
    public void SpawnEnemy()
    {
        ChooseEnemy();
        Vector3 spawn = EnemyAI.RandomPointInBounds(spawnPoint.GetComponent<BoxCollider>().bounds);
        spawn = new Vector3(spawn.x, -1, spawn.z);
        Instantiate(enemyToSpawn, spawn, Quaternion.identity);
    }

    // Choose enemy to spawn based on round number and probability
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
            else if (Random.value < 0.5) enemyToSpawn = rangedEnemy;
            else if (Random.value < 0.8) enemyToSpawn = siegeBrute;
            else enemyToSpawn = rhino;
        }
        else
        {
            if (Random.value < 0.2) enemyToSpawn = meleeEnemy;
            else if (Random.value < 0.4) enemyToSpawn = rangedEnemy;
            else if (Random.value < 0.6) enemyToSpawn = siegeBrute;
            else if (Random.value < 0.8) enemyToSpawn = drone;
            else enemyToSpawn = rhino;
        }
    }
}
