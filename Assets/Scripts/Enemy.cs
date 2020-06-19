using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

/*
 Wir benutzen einen allgemeinen Controller für Gegner und Spieler. Den Input bekommt der Gegner aus dieser Klasse, was aktuell lediglich ein Move Befehl zu Beginn ist.
*/
public class Enemy : MonoBehaviour
{
    [SerializeField] Controller _controller;
    [SerializeField] GameObject _target;

    [SerializeField] GameObject _player;
    [SerializeField] float _attackRange;

    public Enemy Init(GameObject target, GameObject player){
        this._target = target;
        this._player = player;
        this._controller.Move(this._target.transform.position);
        return this;
    }

    void Start()
    {
        this.Init(this._target, this._player);
    }

    void Update()
    {
        Damagable damagable = this._player.transform.GetComponent<Damagable>();
        if (Vector3.Distance(this._player.transform.position, this.transform.position) <= this._attackRange && damagable.isActiveAndEnabled == true)
        {
            this._controller.Attack(damagable);
        }
        else
        {
            this._controller.Move(this._target.transform.position);
        }
    }
}
