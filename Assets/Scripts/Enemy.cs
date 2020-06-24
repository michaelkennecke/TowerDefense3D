using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using System.Linq;

/*
 Wir benutzen einen allgemeinen Controller für Gegner und Spieler. Den Input bekommt der Gegner aus dieser Klasse, was aktuell lediglich ein Move Befehl zu Beginn ist.
*/
public class Enemy : MonoBehaviour
{
    [SerializeField] Controller _controller;
    [SerializeField] GameObject _target;
    Transform _transform;

    public Enemy Init(GameObject target){
        this._target = target;
        this._controller.Move(this._target.transform.position);
        //To save on Performance we invoke the Check only one every X Seconds
        InvokeRepeating("CheckForPlayerInRange", 1f, .5f);
        return this;
    }

    void Awake(){
        this._transform = transform;
    }
    void Start()
    {
        this.Init(this._target);

    }

    void Update(){
    }

    //Alternatively you could use Mechanics Like OnTriggerEnter, Check directly the Distance Enemy<->Player,...
    void CheckForPlayerInRange(){
        Collider[] colsInRange = Physics.OverlapSphere(this._transform.position, this._controller._skill._range);
        if(colsInRange.Any(c => c.CompareTag("Player"))){
            this._controller.Attack(colsInRange.First(c => c.CompareTag("Player")).GetComponent<Damagable>());
        }else{
            this._controller.Move(this._target.transform.position);
        }
    }
}
