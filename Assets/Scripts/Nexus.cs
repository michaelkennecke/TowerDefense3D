using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [SerializeField] int _playerLive = 10;
    public int PlayerLife => this._playerLive;
    private int _maxLive;

    public delegate void OnLifeLoss(int maxlife, int currentLife);
    public OnLifeLoss _onLifeLoss;
    
    void Start(){
        this._maxLive = this._playerLive;
    }

    private void OnCollisionEnter(Collision other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy){
            Destroy(enemy.gameObject);
            this._playerLive--;
            if(this._onLifeLoss != null)
                this._onLifeLoss.Invoke(this._maxLive, this._playerLive);
            
        }
    }


}
