using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    [SerializeField] RespawnPosition _respawnPosition;
    [SerializeField] float _respawnTime = 5f;
    private void OnDisable() {
        if(this._respawnPosition != null)
            this._respawnPosition.Respawn(this._respawnTime, gameObject);
    }
}
