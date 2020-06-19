using System.Collections;
using UnityEngine;

namespace Entities{
        
    public class TopDownCameraController : MonoBehaviour
    {

          [SerializeField] Controller _player;
          [SerializeField] Camera _camera;
          [SerializeField] Transform _cameraArmTransform;
          [SerializeField] Vector3 _offset;
          [SerializeField] float _minimalDistanceToPlayer = 5f;
          [SerializeField] float _maximalDistanceToPlayer = 50f;
          [SerializeField] float _cameraRotateSensibility = 1f;
          [SerializeField] float _cameraScrollSensibility = 1f;
          Transform _playerTransform;
          Transform _camTransform;

          void Awake(){
               this._playerTransform = this._player.transform;
               this._camTransform = this._camera.transform;
          }
          
          void Update() {
               this.Rotate();
               this.Scroll();
               this._cameraArmTransform.position = this._playerTransform.position + _offset;


               this.MoveCharacter();
          }

          void MoveCharacter(){
               if(!Input.GetMouseButtonDown(0)) return;

               Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
               RaycastHit hit;
               if(Physics.Raycast(ray, out hit)){
                    Damagable damagable = hit.transform.GetComponent<Damagable>();
                    if(damagable != null){
                         this._player.Attack(damagable);
                    }else{
                         this._player.Move(hit.point); 
                    }
               }

          }

          void Rotate(){
               if(!Input.GetMouseButton(1)) return;

               this._camTransform.RotateAround(this._camTransform.position, this._camTransform.right, Input.GetMouseButton(1) ? -Input.GetAxis("Mouse Y") : 0);
               this._cameraArmTransform.RotateAround(this._playerTransform.position, Vector3.up, Input.GetMouseButton(1) ? Input.GetAxis("Mouse X") : 0);
               
          }

          void Scroll(){
               //not scrolling? return
               if(Input.mouseScrollDelta.y == 0) return;

               float movingDistance = Time.deltaTime * 10f * Input.mouseScrollDelta.y;
               float distanceToPlayer = Vector3.Distance(this._camTransform.position, this._playerTransform.position);
               
               //would be moving out of bounds? return
               if(movingDistance < 0){
                    if(distanceToPlayer - movingDistance > this._maximalDistanceToPlayer) return;
               }
               if(movingDistance > 0){
                    if(distanceToPlayer - movingDistance < this._minimalDistanceToPlayer) return;
               }

               //update position
               this._camTransform.position = this._camTransform.position + ((this._playerTransform.position - this._camTransform.position).normalized * movingDistance);


          }
          
     }
}