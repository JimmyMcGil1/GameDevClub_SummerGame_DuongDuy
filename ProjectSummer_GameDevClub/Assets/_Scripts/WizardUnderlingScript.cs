using System.Collections;
using System.Collections.Generic;
using Units.Bourbon;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Enemy
{
    public class WizardUnderlingScript : EnemyTier3Script
    {
        [SerializeField] float checkAttackDis;
        bool isPlayerOnHead = false;
        bool isIdle = false;
        GameObject master;
        protected override void Awake()
        {
            base.Awake();
            healSld = transform.Find("Enemy_t1_2_Canvas").Find("healSld").gameObject.GetComponent<Slider>();
        }
        public void SetMaster(GameObject _master)
        {
            master = _master;
        }
        private void Start()
        {
            healSld.maxValue = maxHp;
            healSld.value = maxHp;
            healSld.gameObject.SetActive(false);
        }
        private void Update()
        {
            anim.SetBool("isWalk", true);
            UpdateDir();
            if (IsPlayerOnHead())
                isPlayerOnHead = true;
            ChangeDir();
            if (Mathf.Abs(enemyGraphix.transform.position.x - bourbon.transform.position.x) <= checkAttackDis)
            {
                if (attackCounter > attackTimmer)
                {
                    CheckIfHitCharacter();
                    attackCounter = 0;
                }
            }
            attackCounter += Time.deltaTime;
        }
        private void FixedUpdate()
        {
            if (isPlayerOnHead)
            {
                PlayerOnHead();
                isPlayerOnHead = false;
            }
            if (!isIdle)
                MoveInDirection(dirMove);
        }
        void UpdateDir()
        {
            dirMove = Vector2.right * Mathf.Sign(bourbon.transform.position.x - transform.position.x);
        }
        void ChangeDir()
        {
            Vector2 loc = enemyGraphix.transform.localScale;
            if (dirMove.x > 0)
            {
                loc.x = -1;
                enemyGraphix.transform.localScale = loc;
            }
            else
            {
                loc.x = 1;
                enemyGraphix.transform.localScale = loc;
            }
        }
        //private void OnDrawGizmos()
        //{
        //    if (!Application.isPlaying) return;
        //    Gizmos.color = Color.yellow;
        //    BoxCollider2D bound = (BoxCollider2D)gameObject.GetComponentInChildren(typeof(BoxCollider2D));
        //    Vector2 dir = Vector2.right * allignAttackBound.x * dirMove.x + Vector2.up * allignAttackBound.y;
        //    Gizmos.DrawWireSphere(bound.bounds.center + (Vector3)dir, attackRange);
        //    Gizmos.DrawLine(bound.bounds.center, bourbon.transform.position);
        //}

        bool IsPlayerOnHead()
        {
            CapsuleCollider2D cap = GetComponent<CapsuleCollider2D>();
            RaycastHit2D hit = Physics2D.BoxCast(cap.bounds.center, cap.bounds.size, 0, Vector2.up, 0.01f, playerLayer);
            return hit.collider != null;
        }
        void PlayerOnHead()
        {
            if (isIdle) return;
            StartCoroutine(WhenPlayerOnHead());
        }
        IEnumerator WhenPlayerOnHead()
        {
            isIdle = true;
            anim.SetBool("isWalk", false);
            Rigidbody2D rigit = bourbon.GetComponent<Rigidbody2D>();
            float dirMove = bourbon.GetComponent<Animator>().GetFloat("faceRight");
            rigit.velocity = Vector2.up * rigit.mass * 1.5f + Vector2.right * Mathf.Sign(dirMove) * 8;
            yield return new WaitForSeconds(0.005f);
            rigit.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            isIdle = false;
            anim.SetBool("isWalk", true);

        }
        protected override void Dead()
        {
            base.Dead();
            bourbon.GetComponent<BourbonController>().BuffHeal(5);
            master.GetComponent<SummonUnderlingScript>().numberUnderlingInScene--;
        }
        public IEnumerator AppearTime()
        {
            Rigidbody2D rigit = GetComponent<Rigidbody2D>();
            CapsuleCollider2D cap = GetComponent<CapsuleCollider2D>();
            cap.enabled = false;
            float oldGra = rigit.gravityScale;
            rigit.gravityScale = 0;
            anim.SetBool("appear", true);
            yield return new WaitForSeconds(4f);
            cap.enabled = true;
            anim.SetBool("appear", false);
            rigit.gravityScale = oldGra;
        }
    }
}