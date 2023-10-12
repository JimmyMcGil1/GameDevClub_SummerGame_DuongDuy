using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Enemy {
    public class DryadRanger : EnemyTier2Script
    {
        [SerializeField] float attackTimmer;
        [SerializeField] GameObject bulletPref;
        [SerializeField] float checkAttackDis;
        Transform target;
        Transform firePos;
        float attackCounter = Mathf.Infinity;
        [SerializeField] Vector2 attackDirection;
        SkeletonAnimation sktAnim;
        TrackEntry attackTrack;
        protected void Awake() {
            sktAnim = gameObject.GetComponentInChildren<SkeletonAnimation>();
            firePos = transform.Find("firePos");
        }
        // private void Start() {
        //     sktAnim.state.Event += StartAttack;
        // }
        // private void StartAttack(TrackEntry trackEntry, Spine.Event e)
        // {
        //     if (e.Data.Name == "start-attac") {
        //         AttackInDiriction();
        //     }
        // }

        private void Update() {
            attackCounter += Time.deltaTime;
            if (attackCounter > attackTimmer) {
                Attack();
                attackCounter = 0;
            }
        }
       
        void Attack() {
            attackTrack = sktAnim.AnimationState.SetAnimation(0, "attack/ranged/cast", false);
            attackTrack.End += StartShoot;
            
        }

        private void StartShoot(TrackEntry trackEntry)
        {
                    AttackInDiriction();
        }

        public void AttackInDiriction() {
            GameObject bullet = Instantiate(bulletPref, firePos.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().ShootByVector(attackDirection);
        }
    }
}