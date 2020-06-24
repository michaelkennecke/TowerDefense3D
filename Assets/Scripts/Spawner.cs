using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public delegate void LastWave();
    public LastWave onLastWave;
    [SerializeField] int _waveSize = 3;
    [SerializeField] float _timeBetweenWaves = 20f;
    [SerializeField] List<Enemy> _wavePrefabs;
    [SerializeField] GameObject _playersBase;
    
    int _waveIterator;
    bool _spawning;


    float _counter;

    void Start(){
        this._waveIterator = 0;
        this._counter = 0f;
        this._spawning = false;
    }

    void Update(){
        if(this._spawning || this._waveIterator >= this._wavePrefabs.Count) return;
        this._counter += Time.deltaTime;
        if(this._counter >= this._timeBetweenWaves){
            StartCoroutine(this.SpawnWave());
            this._counter = 0f;
            Score.AddScore(1);
        }
    }

    IEnumerator SpawnWave(){
        int spawned = 0;
        WaitForSeconds wait = new WaitForSeconds(.5f);
        while(spawned < this._waveSize){
            Instantiate(this._wavePrefabs[this._waveIterator], transform.position, Quaternion.identity).Init(this._playersBase);
            spawned++;
            yield return wait;
        }
        this._waveIterator++;
        this._spawning = false;
        if(this._waveIterator >= this._wavePrefabs.Count && this.onLastWave != null) this.onLastWave.Invoke();
    }
    
}
