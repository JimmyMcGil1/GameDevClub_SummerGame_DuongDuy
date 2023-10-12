using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Units.Enemy
{
    public class WizardScrpt : EnemyTier2Script
    {
        [SerializeField] int attackPower = 40;
        [SerializeField] float attackTimmer = 1.5f;
        [SerializeField] GameObject bulletPref;
        [SerializeField] float checkAttackDis;
        Transform target;
        Transform firePos;
        float attackCounter;
        [SerializeField] GameObject magicSpherePref;
        bool summon = false;
        Transform left;
        Transform right;
        bool isLeft = false;
        bool isChangingPos = false;
        private void Awake()
        {
            attackCounter = Mathf.Infinity;
            bourbon = GameObject.FindGameObjectWithTag("Bourbon").gameObject;
            healSld = transform.Find("Enemy_t1_2_Canvas").Find("healSld").gameObject.GetComponent<Slider>();
            enemyGraphix = transform.Find("EnemyGraphix").gameObject;
            anim = enemyGraphix.GetComponent<Animator>();
            left = transform.Find("left");
            right = transform.Find("right");
        }
        private void Start()
        {
            healSld.maxValue = maxHp;
            healSld.value = maxHp;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C)) summon = true;
            if (attackCounter > attackTimmer)
            {
                SummonSphere();
                attackCounter = 0;
            }
            attackCounter += Time.deltaTime;
        }
        private void FixedUpdate()
        {
            if (summon)
            {
                summon = !summon;
                SummonSphere();
            }
        }
        void SummonSphere()
        {
            GameObject sphere = Instantiate(magicSpherePref, bourbon.GetComponent<BoxCollider2D>().bounds.center, Quaternion.identity);
            StartCoroutine(StartAppear(sphere, 1.5f));
            sphere.GetComponent<Animator>().SetBool("preAppear", true);
            sphere.GetComponent<CircleCollider2D>().enabled = false;

        }
        IEnumerator StartAppear(GameObject _sphere, float sec)
        {
            yield return new WaitForSeconds(sec);
            _sphere.GetComponent<Animator>().SetBool("preAppear", false);
            _sphere.GetComponent<MagicSphereScript>().damage = attackPower;
            _sphere.GetComponent<Animator>().SetTrigger("appear");
            _sphere.GetComponent<CircleCollider2D>().enabled = true;
        }
        void ChangePos(float sec = 2)
        {
            if (isChangingPos) return;
            isChangingPos = true;
            StartCoroutine(StartChangePos(sec));
        }
        IEnumerator StartChangePos(float sec = 2)
        {

            yield return new WaitForSeconds(sec);
            Transform bodyBound = transform.Find("BodyBound");
            if (isLeft)
            {
                enemyGraphix.transform.position = right.transform.position;
                bodyBound.position = right.transform.position;
            }
            else
            {
                enemyGraphix.transform.position = left.transform.position;
                bodyBound.position = left.transform.position;

            }
            isChangingPos = false;
            isLeft = !isLeft;
            Vector3 lScale = enemyGraphix.transform.localScale;
            lScale.x *= -1;
            enemyGraphix.transform.localScale = lScale;
        }
        public override void TakeDamage(int _dmg)
        {
            ChangePos();
            enemyGraphix.GetComponent<Animator>().SetTrigger("hit");
            currentHp = currentHp - _dmg <= 0 ? 0 : currentHp - _dmg;
            healSld.value = currentHp;
            if (currentHp == 0) Dead();

        }
        protected void Dead()
        {
            Systems.isEndGame = true;
        }
    }
}
