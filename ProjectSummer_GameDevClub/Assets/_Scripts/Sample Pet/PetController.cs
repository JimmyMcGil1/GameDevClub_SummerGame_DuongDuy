using Game;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Units.Pet
{
    public class PetController : MonoBehaviour
    {
        [SerializeField] PetPool petPool;
        [SerializeField] LayerMask enemyLayer;
        public PetState currentState;
        public PetIdleState idleState = new PetIdleState();
        public FollowMasterState followMState = new FollowMasterState();
        public PetAttackState attackState = new PetAttackState();
        [SerializeField] public float normalAttackTimmer {get; private set;} = 1f;
        public float normalAttackCounter = 0f;
        Vector2 target = Vector2.zero;
        bool flyingPet;
        PetMovement _moveSet;
        public PetMovement moveSet => _moveSet;
        PetFigure _petFigure;
        public PetFigure petFigure => _petFigure;
        public SkeletonAnimation sktAnim {get; private set;} 
        GameObject bourbon;
        private void Awake()
        {
            GameObject pet = petPool?.InitSpawn(transform.position);
            pet.transform.SetParent(transform);
            _moveSet = gameObject.GetComponent<PetMovement>();
            _petFigure = gameObject.GetComponentInChildren<PetFigure>();
            sktAnim = gameObject.GetComponentInChildren<SkeletonAnimation>();
            flyingPet = petFigure.flyingPet;
            bourbon = GameObject.FindGameObjectWithTag("Bourbon");
        }
        private void Start()
        {
            ChangeState(idleState);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) {
                Vector3 pos = bourbon.transform.Find("PetApperPos").position;
                petFigure.transform.position = pos;
            }
            normalAttackCounter += Time.deltaTime;
            UpdateSM();
        }
        public void ChangeState(PetState newState)
        {
            currentState?.OnExit();
            newState.OnEnter(this);
            currentState = newState;
        }
        public void UpdateSM()
        {
            currentState.UpdateState();
           
        }
        private void OnGUI() {
            GUI.Label(new Rect(10, 150, 120, 40), $"{currentState}");
        }
        /// <summary>
        /// Check Enemy is in range and cast animation, then call attack 
        /// </summary>
        public void NormalAttack()
        {
            
            CircleCollider2D cirCol = petFigure.GetComponentInChildren<CircleCollider2D>();
            RaycastHit2D hitEnemy = Physics2D.CircleCast(cirCol.bounds.center, 6f, Vector2.down, 0.1f, enemyLayer);
            if (hitEnemy.collider != null)
            {
                target = (Vector2)hitEnemy.transform.position;
                TrackEntry attackTrack =  sktAnim.AnimationState.SetAnimation(0, "attack/ranged/cast-fly",false);
                attackTrack.Start += CastNormalAttack;
                attackTrack.End += EndNormalAttack;
                Debug.Log("end here");
            }
            else
            {
                ChangeState(followMState);
            }
        }

       
        void CastNormalAttack(TrackEntry entry)
        {
            petFigure.NormalAttack(target);
        }
        void EndNormalAttack(TrackEntry entry)
        {
            petFigure.NormalAttackDone();
        }
        public void CheckFlip()
        {
            //xoay dau 
            Vector2 _scale = moveSet.followPos.parent.transform.localScale;
            petFigure.transform.localScale = _scale;
        }
    }
}
