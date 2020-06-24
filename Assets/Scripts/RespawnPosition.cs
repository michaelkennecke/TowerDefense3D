using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPosition : MonoBehaviour
{
    public void Respawn(float respawnTime, GameObject obj){
        StartCoroutine(this.CountToRespawn(respawnTime, obj));
    }
    IEnumerator CountToRespawn(float time, GameObject obj){
        yield return new WaitForSeconds(time);
        obj.transform.position = this.transform.position;
        obj.SetActive(true);

    }
}
