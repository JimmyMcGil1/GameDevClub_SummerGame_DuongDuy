using System.Collections;
using UnityEngine;
using Spine.Unity;
using Units.Bourbon;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEditor;
using System.Security.Cryptography;

namespace Units.Enemy
{
    public class EnemyTier3Script : EnemyUnitBase
    {
        [SerializeField] protected string chimeraName = "Untitled Chimera";

        [SerializeField] public float speed;
        [SerializeField] public Vector2 dirMove;
        [SerializeField] public float timeInOneDirect = 3;
        protected float counterInOneDirect;
        [SerializeField] protected float timeInOnStep = 1;
        protected float counterInOneStep = Mathf.Infinity;

        [Header("Attack")]
        
        protected float visionRange = 2f;
        [SerializeField] protected float attackRange;
        [SerializeField] protected Vector2 allignAttackBound;
        [SerializeField] protected LayerMask playerLayer;
        [SerializeField] protected float attackTimmer = 0.4f;

        [SerializeField] float fallSpeed = 2;
        [SerializeField] LayerMask groundLayer;
        protected float currentGravity = 0;
        public float Gravity = -30f;

        protected Vector2 _newPosition;
        /// a multiplier applied to the character's gravity when going down
        public float FallMultiplier = 1f;
        /// a multiplier applied to the character's gravity when going up
        public float AscentMultiplier = 1f;

        protected Vector2 _speed;
        protected float attackCounter = Mathf.Infinity;
        [SerializeField] protected ChimeraFigure chimeraFigure;
        protected SkeletonAnimation sktAnim;
        bool isHitIdle = false;
        BoxCollider2D box;
        event Action<CharacterConditions> attack;

        protected virtual void Awake()
        {
            enemyGraphix = transform.Find("EnemyGraphix").gameObject;
            if (enemyGraphix != null)
            {
                sktAnim = enemyGraphix.GetComponent<SkeletonAnimation>();
                anim = enemyGraphix.GetComponent<Animator>();
            }
            counterInOneDirect = 0;
            bourbon = GameObject.FindGameObjectWithTag("Bourbon").gameObject;
            attackCounter = Mathf.Infinity;
            box = GetComponent<BoxCollider2D>();
            healSld = transform.Find("Enemy_t1_2_Canvas").Find("healSld").gameObject.GetComponent<Slider>();
            nameTxt = transform.Find("Enemy_t1_2_Canvas").Find("Name").gameObject.GetComponent<Text>();
        }
        protected virtual void Start()
        {
            currentHp = maxHp;
            healSld.maxValue = maxHp;
            healSld.value = maxHp;
            nameTxt.text = chimeraName;
            condition = EnemyCondition.patroll;
        }



        protected virtual void Update()
        {

            attackCounter += Time.deltaTime;
            visionRange = allignAttackBound.x + attackRange;
            //check if character is near 
            if (bourbon != null && condition != EnemyCondition.dead)
            {
                if ((Mathf.Abs(box.bounds.center.x - bourbon.transform.position.x) < visionRange))
                {
                    condition = EnemyCondition.attack;
                    if (attackCounter > attackTimmer)
                    {
                        CheckIfHitCharacter();
                        attackCounter = 0;
                    }
                    return;
                }
            }
            //if not, then patrol 
            Patrolling(timeInOneDirect, 3f);

        }

        protected virtual void MoveInDirection(Vector2 dir)
        {
            dir.Normalize();
            Vector2 currPos = transform.position;
            currPos.x += dir.x * speed * Time.deltaTime;
            currPos.y += dir.y * speed * Time.deltaTime;
            transform.position = currPos;

        }


        //Enemy patrolling (tuan` tra) in a axis
        protected virtual void Patrolling(float timeInOneDirect, float timeInIdle = 1)
        {
            if (condition == EnemyCondition.idle) return;
            else if (counterInOneDirect > timeInOneDirect)
            {
                condition = EnemyCondition.idle;
                StartCoroutine(IdleTime(timeInIdle));
            }
            else
            {
                condition = EnemyCondition.patroll;
                MoveInDirection(dirMove);
                counterInOneDirect += Time.deltaTime;
            }

        }
        protected IEnumerator IdleTime(float sec)
        {
            yield return new WaitForSeconds(sec);
            //turn arround
            TurnAround();
            counterInOneDirect = 0;
            condition = EnemyCondition.patroll;
            //
        }
        protected void TurnAround()
        {
            
            counterInOneDirect = 0;
            Vector2 scale = enemyGraphix.transform.localScale;
            scale.x *= -1;
            dirMove.x *= -1;
            enemyGraphix.transform.localScale = scale;
        }
        protected virtual void CheckIfHitCharacter()
        {
            BoxCollider2D bound = (BoxCollider2D)gameObject.GetComponentInChildren(typeof(BoxCollider2D));
            Vector2 dir = Vector2.right * allignAttackBound.x * dirMove.x + Vector2.up * allignAttackBound.y;
            Collider2D hit = Physics2D.OverlapCircle(bound.bounds.center + (Vector3)dir, attackRange, playerLayer);
        }
       
        
        public override void TakeDamage(int _dmg)
        {
           
            if (isHitIdle) return;
            base.TakeDamage(_dmg);
            healSld.value = currentHp;
            //check neu hp = 0 thi chet
            if (currentHp == 0)
            {
                Dead();
                return;
            }
            StartCoroutine("StartHitIdle", 0.8f);

        }
        protected virtual void  Dead()
        {
            //sktAnim.AnimationState.SetAnimation(0, "defense/hit-die", false);
            condition = EnemyCondition.dead;
        }

        protected IEnumerator StartHitIdle(float sec)
        {
            isHitIdle = true;
            float initSpeed = speed;
            speed = 0;
            yield return new WaitForSeconds(sec);
            speed = initSpeed;
            isHitIdle = false;
        }
        //funny: random idle 
        protected int RandomIdle(int numberAnimIdle = 3)
        {
            System.Random random = new System.Random();
            return  1 + random.Next(numberAnimIdle);

        }
        protected bool IsGround()
        {
            RaycastHit2D hit =  Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0f, groundLayer);
            return hit.collider != null;
        }
       
    }
}

