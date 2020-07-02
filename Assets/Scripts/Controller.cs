using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
Ein allgemeiner Controller mit öffentlichen Methoden um ihn zu steuern.
Der Spielercharakter bekommt seinen Input primär aus dem CameraController, währende Gegner ihren Input aus der Enemy-Klasse erhalten
*/
public class Controller : MonoBehaviour
{
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] Transform _projectileSpawnPosition;
    [SerializeField] NavMeshAgent _agent;
    public List<Skill> _skills;
    public Skill _skill;
    public NavMeshAgent Agent => this._agent;
    [SerializeField] Animator _animator;
    public Animator Animator => this._animator;

    public List<int> _startSkillDamages;

    Damagable _target;
    public Damagable Target => this._target;
    Transform _transform;

    public int _money; 

    public AudioSource _audio;

    public Shop _shop;

    void Awake() {
        this._transform = transform;  
        this._startSkillDamages = new List<int>();
        for (int i=0; i < this._skills.Count; i++) {
            this._startSkillDamages.Add(this._skills[i]._damageEffect);
            Debug.Log("Start value: " + this._startSkillDamages[i]);
        }
    }

    void Start() {
        this._audio = this.GetComponent<AudioSource>();
    }

    public void Move(Vector3 destination){
        this._animator.speed = 1;
        if(!isActiveAndEnabled) return;
        this._target = null;
        this._agent.destination = destination;
        this._animator.SetBool("Attacking", false);
    }

    
    public void Attack(Damagable target){
        this._animator.speed = this._skill._attackSpeed;
        this._animator.SetBool("Attacking", true);
        this._target = target;
    }

    //invoked via Animation Event in Animator
    public void Shoot(){
        //just a small hack so if you change the skill while attacking, the attackSpeed gets updated
        this._animator.speed = this._skill._attackSpeed;
        if(!this._target){
            this._animator.SetBool("Attacking", false);
            return;
        }
        this._audio.clip = this._skill._audioClip;
        this._audio.Play();
        this._skill.Execute(this, this._projectileSpawnPosition.position, this._target);
    }
    

    void Update(){
        if(this._target && !this._target.isActiveAndEnabled) this._target = null;
        //set BlendTree value to a value 0-1 (0 if not moving, 1 if at max speed)

        this._animator.SetFloat("Speed", this._agent.velocity.magnitude / this._agent.speed);
        if(this._target != null){
            this._animator.SetBool("InRange", Vector3.Distance(this._target.transform.position, this._transform.position) <= this._skill._range);
        }

        if(this._animator.GetBool("Attacking")){
            if(this._target){
                this._agent.destination = this._target.transform.position;
                this.RotateTowards(this._target.gameObject.transform);
            }else{
                this._animator.SetBool("Attacking", false);
            }
        }

        if(Input.GetKeyDown("b") && this.gameObject.CompareTag("Player")) {
            if (!this._shop.gameObject.activeInHierarchy) {
                this._shop.gameObject.SetActive(true);
            } else {
                this._shop.gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, this._skill._range);
    }

    void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - this._transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        _transform.rotation = Quaternion.Slerp(this._transform.rotation, lookRotation, Time.deltaTime * 30f);
    }

    //Invoked from Animator
    public void StopAgent(){
        this._agent.isStopped = true;
    }
    public void StartAgent(){
        this._agent.isStopped = false;
    }

    public void Reset() {
        Money.Reset();
        for (int i=0; i < this._skills.Count; i++) {
            Debug.Log("End value: " + this._startSkillDamages[i]);
            this._skills[i]._damageEffect = this._startSkillDamages[i];
        }
    }
}
