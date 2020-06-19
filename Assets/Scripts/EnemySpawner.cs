using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] int waveSize;
    [SerializeField] float timeBetweenWaves = 10f;
    [SerializeField] Enemy[] waves = new Enemy[2];

    private Transform _transform;
    private float spawnTimer;
    private int waveNumber;

    private void Awake()
    {
        this._transform = transform;
    }

    void Start()
    {
        this.spawnTimer = this.timeBetweenWaves;
        this.waveNumber = 0;
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            if (this.spawnTimer > 0)
            {
                this.spawnTimer -= Time.deltaTime;
            }
            if (this.spawnTimer <= 0)
            {
                this.SpawnWave(this.waveNumber);
            }
        }
    }

    void SpawnWave(int waveNumber)
    {
        for (int i = 0; i < waveSize; i++)
        {
            Instantiate(this.waves[waveNumber], this._transform.position, Quaternion.identity).Init(GameObject.FindGameObjectWithTag("Nexus"), GameObject.FindGameObjectWithTag("Player"));
        }
        this.spawnTimer = this.timeBetweenWaves;
        this.waveNumber += 1;
    }
}

