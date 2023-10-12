
using Spine.Unity;
using UnityEngine;
using Spine;
using DG.Tweening;
using System;
using System.Collections;
namespace Units.Enemy 
{
    public class DyadFighter : MonoBehaviour
    {
        [SerializeField] float skill1Timmer = 1;
        [SerializeField] GameObject kick;
        [SerializeField] GameObject kickTarget;
        SkeletonAnimation sktAnim;
        private void Awake() {
            sktAnim = transform.GetComponentInChildren<SkeletonAnimation>();
        }
        private void OnEnable() {
            if (sktAnim != null)
                sktAnim.state.Event += OnKick;
        }

        private void OnKick(TrackEntry trackEntry, Spine.Event e)
        {
            StartCoroutine("DisableKickCol");
            kick.GetComponent<CircleCollider2D>().enabled = true;
            if (e.Data.Name == "hit"){
                Debug.Log("hit event");
            kick.transform.DOMove(kickTarget.transform.position, 0.2f, false).SetLoops(2, LoopType.Yoyo);
            }
        }

        float skill1Counter;
       private void Update() {
         skill1Counter += Time.deltaTime;
         if (skill1Counter > skill1Timmer) {
            CastSkill1();
            skill1Counter = 0;
         }
       }
       void CastSkill1() {
           
           sktAnim?.AnimationState.SetAnimation(0, "attack/melee/normal-attack", false);
       }
       
        IEnumerator DisableKickCol(){
            yield return new WaitForSeconds(0.3f);
            kick.GetComponent<CircleCollider2D>().enabled = false;
        }
    } 
   
}

