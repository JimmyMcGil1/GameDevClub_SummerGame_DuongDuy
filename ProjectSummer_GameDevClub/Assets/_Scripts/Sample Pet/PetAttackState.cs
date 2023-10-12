using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Units.Pet
{
    public class PetAttackState : PetState
    {
        GameObject _target;
        public  bool _isAttacking = false;
        public float attackDelay = 0.5f;
        bool _isEndedAttack;
        public float posX;
        public override void OnEnter(PetController ctrler)
        {
            base.OnEnter(ctrler); 
            CircleCollider2D cirCol = controller.GetComponent<CircleCollider2D>();
            var enemyLayer = LayerMask.GetMask("Enemy");
            RaycastHit2D hitEnemy = Physics2D.CircleCast(cirCol.bounds.center, 6f, Vector2.down, 0.1f, enemyLayer);
            if (hitEnemy.collider != null)
            {
                
                _target = hitEnemy.transform.gameObject;
                _isAttacking = false;
                _isEndedAttack = false;
                // TrackEntry attackTrack =  sktAnim.AnimationState.SetAnimation(0, "attack/ranged/cast-fly",false);
                // attackTrack.Start += CastNormalAttack;
                // attackTrack.End += EndNormalAttack;
                // Debug.Log("end here");
            }
            else
            {
              _target = null;
            }
        }
        public override void UpdateState()
        {  
            if(_target == null && !_isAttacking){
                controller.ChangeState(controller.followMState);
                return;
            }
            //First attack
            else if (_target != null && _isAttacking == false) {
                _isAttacking = true;
               attackDelay = 0.5f; //time to attack
               posX = controller.petFigure.transform.position.x;
            }
            if (_isAttacking) {
                controller.normalAttackCounter += Time.deltaTime;
                if (_target != null)
                    if (controller.normalAttackCounter > controller.normalAttackTimmer) {
                        controller.petFigure.NormalAttack(_target.transform.position);  
                        controller.normalAttackCounter = 0;
                    }
                else controller.ChangeState(controller.followMState);
            }
            //////////
//             if(!_isAttacking)
//             {
//                     _isAttacking = true;
                    
//                     TrackEntry attackTrack =  controller.sktAnim.AnimationState.SetAnimation(0, "attack/ranged/cast-fly",false);
//                     controller.NormalAttack();
//                    // attackTrack.End += EndNormalAttack;
// //                    CastNormalAttack();
//             } 
//             else if(_isEndedAttack)
//             {
//                 controller.ChangeState(controller.followMState);
//             }
        }

        public override void OnExit()
        {
            _isAttacking = false;

        }

        void EndNormalAttack(TrackEntry entry)
        {
            _isEndedAttack = true;
            controller.petFigure.NormalAttackDone();
        }          

        ///test 
        void TestNormalAttack(float dist) {
            attackDelay -= Time.deltaTime;
            
            if (attackDelay < 0){
                controller.ChangeState(controller.followMState);
            }
            else {
                var currentX = posX +  dist *(0.625 -  (attackDelay - 0.25) * (attackDelay - 0.25)); 
                controller.petFigure.transform.position = new Vector2((float)currentX, controller.petFigure.transform.position.y);
            }
        }  
    }
}
