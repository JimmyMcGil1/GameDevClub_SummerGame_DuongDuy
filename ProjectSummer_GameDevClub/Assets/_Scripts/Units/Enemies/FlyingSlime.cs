using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Units.Bourbon;
using DG.Tweening;

namespace Units.Enemy
{
    public class FlyingSlime : EnemyTier3Script
    {
        SkeletonAnimation skltAnim;
        [SerializeField] LayerMask bourbonLayer;
        protected override void Awake()
        {
            base.Awake();
            skltAnim = GetComponentInChildren<SkeletonAnimation>();
        }
        protected override void Start()
        {
            //gameObject.GetComponent<AIDestinationSetter>().target = bourbon.transform;
            //gameObject.GetComponent<AIPath>().canMove = true;
            float dur = (bourbon.transform.position - transform.position).magnitude / speed;
            transform.DOMove(bourbon.transform.position, dur);
        }
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Bourbon"))
            {
                // StartCoroutine("StartExplode");
                Debug.Log("explode");
                TrackEntry _animation = skltAnim.AnimationState.SetAnimation(0, "attack/ranged/goo-destruct", false);
                _animation.Complete += Explode;
            }
        }
        private void Explode(TrackEntry trackEntry)
        {
            Debug.Log("Explode");
            BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
            //RaycastHit2D hit = Physics2D.CircleCast(box.bounds.center, box.radius, Vector2.down, 0.01f, bourbonLayer);
            RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down ,0.01f, bourbonLayer);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<BourbonController>().TakeDamage(-80);
            }
            Destroy(gameObject);
            //throw new NotImplementedException(); 
        }
        protected override void Dead()
        {
            base.Dead();
            TrackEntry dieEntry =  sktAnim.AnimationState.SetAnimation(0, "defense/hit-die", false);
            dieEntry.Complete += DestroyGameObject;
        }
        void DestroyGameObject(TrackEntry entry)
        {
            Destroy(gameObject);
        }

        //IEnumerator StartExplode()
        //{
        //    //TrackEntry _animation =  skltAnim.AnimationState.SetAnimation(0, "attack/ranged/goo-destruct", false);
        //    //_animation.Event += Explode;
        //    //yield return new WaitForSeconds(0.8f);
        //    CircleCollider2D cirCol = gameObject.GetComponent<CircleCollider2D>();



        //}
    }
}
