using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plan_t2 : EnemyTier2Script
{

    [SerializeField] float attackTimmer;
    [SerializeField] GameObject bulletPref;
    [SerializeField] float checkAttackDis;
    Transform target;
    Transform firePos;
    float attackCounter;

    private void Awake()
    {
        attackCounter = Mathf.Infinity;
        bourbon = GameObject.FindGameObjectWithTag("Bourbon").gameObject;
        target = transform.Find("ParabolRoot").Find("Target");
        firePos = transform.Find("ParabolRoot").Find("FirePos");
        SetStats(initStats);
        healSld = transform.Find("Enemy_t1_2_Canvas").Find("healSld").gameObject.GetComponent<Slider>();
        enemyGraphix = transform.Find("EnemyGraphix").gameObject;
        anim = enemyGraphix.GetComponent<Animator>();

    }
    private void Start()
    {
        healSld.maxValue = _Stats.Health;
        healSld.value = _Stats.Health;
        healSld.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (attackCounter > attackTimmer)
        {
            float checkDis = enemyGraphix.transform.position.x - bourbon.transform.position.x;
            if (checkDis <= 7 && checkDis >= 0)
            {
                anim.SetTrigger("attack");
                attackCounter = 0;
            }
        }
        attackCounter += Time.deltaTime;
    }
    public override void Attack()
    {
        target.transform.position = bourbon.transform.position; 
        GameObject bullet = Instantiate(bulletPref, firePos.position, Quaternion.identity);
        bullet.GetComponent<ParabolCtrller>().ParabolaRoot = transform.Find("ParabolRoot").gameObject;
    }
}
