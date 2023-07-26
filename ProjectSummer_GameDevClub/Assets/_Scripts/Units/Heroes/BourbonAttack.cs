using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BourbonAttack : MonoBehaviour
{
    [SerializeField] float attackTimmer;
    float attackCounter;
    int attackNumber;
    int punchNumber;
    [SerializeField] float cannotMoveTime;
    Animator anim;
    BoxCollider2D box;
    [SerializeField] Vector2 attackAllign;
    [SerializeField] float attackRadiusRangeSword;
    [SerializeField] float attackRadiusRangePunch;
    [SerializeField] LayerMask bodyBoundLayer;
    int attackStats;
    [SerializeField] int swordBuffDmg;

    public AudioClip[] attackSound;
    public AudioClip[] punchSound;

    public bool canAttack;
    private void Awake()
    {
        attackCounter = Mathf.Infinity;
        attackNumber = 0;
        punchNumber = 0;
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        attackStats = BourbonMoveset.currStats.AttackPower;
        canAttack = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !BourbonUnitBase.isDead && !IsMouseOverUI() && canAttack)
        {
            if (attackCounter > attackTimmer)
            {
                if (anim.GetFloat("bringSword") == 1)
                {
                    Attack();
                    attackNumber++;
                    if (attackNumber > 2) attackNumber = 0;
                }
                else
                {
                    Punch();
                    punchNumber++;
                    if (punchNumber > 2) punchNumber = 0;
                }
              
            }
        }
            attackCounter += Time.deltaTime;
    }
    void Attack()
    {
        StartCoroutine(GetComponent<BourbonMoveset>().CannotMove(cannotMoveTime));
        anim.SetTrigger("attack");
        anim.SetFloat("attackNumber", attackNumber);
        attackCounter = 0;
        AttackSound();
    } 
    public void AttackSound()
    {
        AudioSystemScript.instance.PlaySound(attackSound[attackNumber], gameObject.transform.position, 1);
    }
    void CheckAttackHitEnemy()
    {
        float dir = anim.GetFloat("faceRight");
        Vector3 _attackAllign = attackAllign;
        if (dir < 0) _attackAllign.x = -attackAllign.x;
        else _attackAllign.x = attackAllign.x;
        float attckR = (anim.GetFloat("bringSword") == 0) ? attackRadiusRangePunch : attackRadiusRangeSword;
        Collider2D[] hits = Physics2D.OverlapCircleAll(box.bounds.center + _attackAllign, attckR, bodyBoundLayer);
        int attackDmg = (anim.GetFloat("bringSword") == 0) ? attackStats : attackStats + swordBuffDmg;
        foreach (var _hit in hits)
        {
            GameObject enemy = _hit.transform.parent.gameObject;
            if (enemy.CompareTag("Enemy"))
                enemy.GetComponent<EnemyUnitBase>().TakeDamage(attackDmg);
            
            Debug.Log("Attack enemy:" + attackDmg.ToString());
        }
    }
    void Punch()
    {
        StartCoroutine(GetComponent<BourbonMoveset>().CannotMove(cannotMoveTime));
        anim.SetTrigger("punch");
        anim.SetFloat("punch_number", punchNumber);
        attackCounter = 0;
        PunchSound();
    }
    public void PunchSound()
    {
        AudioSystemScript.instance.PlaySound(punchSound[punchNumber], gameObject.transform.position, 1);
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.magenta;
        float dir = anim.GetFloat("faceRight");
        Vector3 _attackAllign = attackAllign;
        if (dir < 0) _attackAllign.x = -attackAllign.x;
        else _attackAllign.x = attackAllign.x;
        float attckR = (anim.GetFloat("bringSword") == 0) ? attackRadiusRangePunch : attackRadiusRangeSword;
        Gizmos.DrawWireSphere(box.bounds.center + _attackAllign, attckR);
    }
    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    

}
