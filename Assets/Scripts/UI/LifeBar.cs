using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] Damagable _damagable;
    Image _image;

    void Awake(){
        this._image = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this._image.fillAmount = this._damagable.HealthRatio;

        //this.transform.LookAt(Camera.main.transform);
    }
}
