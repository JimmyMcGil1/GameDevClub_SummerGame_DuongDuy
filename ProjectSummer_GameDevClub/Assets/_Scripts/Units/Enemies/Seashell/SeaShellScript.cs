using UnityEngine;
using UnityEngine.UI;
namespace Units.Enemy
{
    public class SeaShellScript : EnemyTier2Script
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
            firePos = transform.Find("FirePos");
            healSld = transform.Find("Enemy_t1_2_Canvas").Find("healSld").gameObject.GetComponent<Slider>();
            enemyGraphix = transform.Find("EnemyGraphix").gameObject;
            anim = enemyGraphix.GetComponent<Animator>();
        }


        // Update is called once per frame
        void Update()
        {
            if (attackCounter > attackTimmer)
            {
                anim.SetTrigger("attack");
                attackCounter = 0;
            }
            attackCounter += Time.deltaTime;
        }
        public void Attack()
        {
            GameObject bullet = Instantiate(bulletPref, firePos.position, Quaternion.identity);
            Vector2 dir = (Vector2)(firePos.transform.position - transform.position);
            bullet.GetComponent<PearlScript>().Fire(dir.normalized);
        }
    }
}