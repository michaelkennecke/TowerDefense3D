using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [SerializeField] Spawner _spawner;
    [SerializeField] Nexus _nexus;
    [SerializeField] SceneController _sceneController;

    List<Enemy> _enemies;
    // Start is called before the first frame update
    void Start()
    {
        this._spawner.onLastWave += this.WaitForEndOfGame;
        this._nexus._onLifeLoss += this.CheckNexusLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WaitForEndOfGame(){
        StartCoroutine(this.WaitForEndOfGame(GameObject.FindObjectsOfType<Enemy>().ToList()));
    }

    IEnumerator WaitForEndOfGame(List<Enemy> enemies){
        List<Enemy> livingEnemies = enemies;
        WaitForSeconds wait = new WaitForSeconds(2);
        while(livingEnemies.Count > 0){
            livingEnemies = livingEnemies.Where(e => e != null).ToList();
            yield return wait;
        }
        Score.SetWinOrLoss(true);
        this._sceneController.ToEndMenu();
        Debug.Log("win");
    }

    void CheckNexusLife(int maxlife, int currentLife){
        if(currentLife <= 0) {
            Score.SetWinOrLoss(false);
            this._sceneController.ToEndMenu();
            Debug.Log("Loss");
        }            
    }
}
