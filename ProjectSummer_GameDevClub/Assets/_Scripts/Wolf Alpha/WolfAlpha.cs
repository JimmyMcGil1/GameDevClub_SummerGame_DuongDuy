using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Units.Enemy
{
    public class WolfAlpha : EnemyTier3Script
    {
        [SerializeField] bool isMultiIdle;
        [SerializeField] int numberAnimIdle = 3;
        EnemySpawner enemySpawner;
        protected override void Awake()
        {
            base.Awake();
           // enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").gameObject.GetComponent<EnemySpawner>();
        }
        protected override void Start()
        {
            base.Start();
            sktAnim.AnimationState.SetAnimation(0, "action/move-forward", true);
            nameTxt.text = chimeraName;

        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void Dead()
        {
            base.Dead();
            //disable body bound
            CapsuleCollider2D bdBound = transform.Find("BodyBound").GetComponent<CapsuleCollider2D>();
            bdBound.enabled = false;
            Spine.TrackEntry die_entry = sktAnim.AnimationState.SetAnimation(0, "defense/hit-die", false);
            die_entry.End += DestroyGameObject;
        //    enemySpawner.SpawnWolf();
        }

        public override void TakeDamage(int _dmg)
        {
            base.TakeDamage(_dmg);
            sktAnim.AnimationState.SetAnimation(0, "defense/hit-by-normal", false);
        }
        protected override void Patrolling(float timeInOneDirect, float timeInIdle = 1)
        {
            if (condition == EnemyCondition.idle) return;
            else if (counterInOneDirect > timeInOneDirect)
            {
                condition = EnemyCondition.idle;
                if (isMultiIdle)
                    sktAnim.AnimationState.SetAnimation(0, "action/idle/random-0" + RandomIdle().ToString(), false);
                else 
                    sktAnim.AnimationState.SetAnimation(0, "action/idle/normal", false);

                StartCoroutine(IdleTime(timeInIdle));
            }
            else
            {
                condition = EnemyCondition.patroll;
                MoveInDirection(dirMove);
                counterInOneDirect += Time.deltaTime;
            }
        }
        protected override void MoveInDirection(Vector2 dir)
        {
            dir.Normalize();
            sktAnim.AnimationState.AddAnimation(0, "action/move-forward", false, 0);
            Vector2 currPos = transform.position;
            currPos.x += dir.x * speed * Time.deltaTime;
            currPos.y += dir.y * speed * Time.deltaTime;
            transform.position = currPos;

        }
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.CompareTag("Axie"))
                chimeraFigure.TakeDame();
            if (collision.gameObject.layer == 9)
            {
                if (condition != EnemyCondition.idle)
                    TurnAround();
            }
        }

        protected override void CheckIfHitCharacter()
        {
            base.CheckIfHitCharacter();
            BoxCollider2D bound = (BoxCollider2D)gameObject.GetComponentInChildren(typeof(BoxCollider2D));
            Vector2 dir = Vector2.right * allignAttackBound.x * dirMove.x + Vector2.up * allignAttackBound.y;
            Collider2D hit = Physics2D.OverlapCircle(bound.bounds.center + (Vector3)dir, attackRange, playerLayer);
            if (hit != null)
            {
                sktAnim.AnimationState.SetAnimation(0, "attack/melee/normal-attack", false);
            }
        }
        private void DestroyGameObject(TrackEntry trackEntry)
        {
            Destroy(gameObject);
        }
        protected void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            Gizmos.color = Color.gray;
            BoxCollider2D bound = (BoxCollider2D)gameObject.GetComponentInChildren(typeof(BoxCollider2D));
            Vector2 dir = Vector2.right * allignAttackBound.x * dirMove.x + Vector2.up * allignAttackBound.y;
            Gizmos.DrawWireSphere(bound.bounds.center + (Vector3)dir, attackRange);
        }

    }
    
}
