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
    [SerializeField] float _range = 10f;
    public NavMeshAgent Agent => this._agent;
    [SerializeField] Animator _animator;
    public Animator Animator => this._animator;

    Damagable _target;
    Transform _transform;

    [SerializeField] Skill[] skills = new Skill[3];
    [SerializeField] SkillBarUI skillBarUI;
    Skill _currentSkill;

    void Awake() {
        this._transform = transform;    
    }

    void Start(){
        this._currentSkill = skills[0];
        this._range = this._currentSkill.range;
    }

    public void Move(Vector3 destination){
        this._target = null;
        this._agent.destination = destination;
        this._animator.SetBool("Attacking", false);
    }

    
    public void Attack(Damagable target){

        this._animator.SetBool("Attacking", true);
        this._target = target;
    }

    //invoked via Animation Event in Animator
    public void Shoot(){
        if(!this._target){
            this._animator.SetBool("Attacking", false);
            return;
        } 
        Instantiate(this._projectilePrefab, this._projectileSpawnPosition.position, Quaternion.identity).Init(this._target, this.gameObject, this._currentSkill);
    }
    

    void Update(){
        //set BlendTree value to a value 0-1 (0 if not moving, 1 if at max speed)

        this._animator.SetFloat("Speed", this._agent.velocity.magnitude / this._agent.speed);
        if(this._target != null){
            this._animator.SetBool("InRange", Vector3.Distance(this._target.transform.position, this._transform.position) <= this._currentSkill.range);
        }

        if(this._animator.GetBool("Attacking")){
            if(this._target){
                this._agent.destination = this._target.transform.position;
                this.RotateTowards(this._target.gameObject.transform);
            }else{
                this._animator.SetBool("Attacking", false);
            }
            
        }

        this.ListenToSwictchSkillInput();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, this._range);
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

    void ListenToSwictchSkillInput()
    {
        if (skills.Length <= 1)
        {
            return;
        }

        for (int i = 1; i <= skills.Length; ++i)
        {
            if (Input.GetKeyDown("" + i))
            {
                this.skillBarUI.GetUISkills()[i-1].ClickSkillButton();
                //this.SwitchSkill(i);
            }
        }
    }

    public void SwitchSkill(int skillId)
    {
        Debug.Log("Skill: " + skillId + " ausgewählt");
        this._currentSkill = skills[skillId - 1];
        this._range = this._currentSkill.range;
    }

    public Skill[] GetSkills()
    {
        return this.skills;
    }
}
