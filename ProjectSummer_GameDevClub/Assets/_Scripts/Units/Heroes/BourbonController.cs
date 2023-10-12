using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

namespace Units.Bourbon
{
    public partial class BourbonController : BourbonUnitBase
    {
        public float hor;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask wallLayer;
        [SerializeField] LayerMask ropeBoundLayer;

        public float maxTimeCringe = 0.1f;
        public float timeCringeCounter = 0;
        
        public bool attached = false;
        public Transform attachedTo;
        private GameObject disregard;
        float initGravity;
        

        [SerializeField] AudioClip landingSound;

        private BourbonMoveset _moveSet;
        public BourbonMoveset moveSet => _moveSet;

        public bool isGround = false;
        public bool isHitWall = false;
        private void Awake()
        {
            Initialization();
        }
        public void Initialization()
        {
            anim = GetComponent<Animator>();
            //box = GetComponent<BoxCollider2D>();
            capCol = gameObject.GetComponent<CapsuleCollider2D>();
            rigit = GetComponent<Rigidbody2D>();
            _moveSet = GetComponent<BourbonMoveset>();
            _canMove = true;
            _bringSword = 0;
            _runLeft = 0;
            bourbonEffect = transform.Find("Effect").gameObject.GetComponent<BourbonEffectScript>();
            SetStats(scriptableHero);
            BourbonUI.instance.HealTxtChange(currStats.Health);
            condition = CharacterConditions.Normal;
            moveStates = CharacterMoveStates.Idle;
;        }

        #region Boxcast Handle move state
        public bool IsGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(capCol.bounds.center.x, capCol.bounds.min.y), Vector2.down, 0.1f, groundLayer);
            return hit.collider != null;
        }
        // public bool IsClingingWall()
        // {
        //     RaycastHit2D rhit = Physics2D.Raycast(new Vector2(capCol.bounds.max.x, capCol.bounds.center.y), Vector2.right, 0.1f, wallLayer);
        //     RaycastHit2D lhit = Physics2D.Raycast(new Vector2(capCol.bounds.min.x, capCol.bounds.center.y), Vector2.left, 0.1f, wallLayer);
        //     if (rhit.collider != null) {
        //         moveSet.faceDir = Vector2.right;
        //     }
        //     else if (lhit.collider != null) {
        //         moveSet.faceDir = Vector2.left;
        //     }
        //     return rhit.collider != null || lhit.collider != null;

        // }
        public bool HitWallRight()
        {
            RaycastHit2D rhit = Physics2D.Raycast(new Vector2(capCol.bounds.max.x, capCol.bounds.center.y), Vector2.right, 0.1f, wallLayer);
            if (rhit.collider != null) {
                moveSet.faceDir = Vector2.right;
            }
            return rhit.collider != null;
        }
        public bool HitWallLeft()
        {
            RaycastHit2D lhit = Physics2D.Raycast(new Vector2(capCol.bounds.min.x, capCol.bounds.center.y), Vector2.left, 0.1f, wallLayer);
            if (lhit.collider != null) {
                moveSet.faceDir = Vector2.left;
            }
            return lhit.collider != null;
        }

        //private void OnGUI()
        //{
        //    GUI.Label(new Rect(10, 100, 100, 20), $"box:{box.bounds.center.x}, {box.bounds.center.y}");
        //}

        #endregion
        public void IntoWater()
        {
            ChangeCharacterCondition(CharacterConditions.Water);
        }
        public void OutWater()
        {
            anim.SetBool("isSwimming", false);
            ChangeCharacterCondition(CharacterConditions.Normal);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
           
            if (!attached)
            {
                if (collision.gameObject.CompareTag("Rope"))
                {
                    if (attachedTo != collision.gameObject.transform.parent)
                    {
                        if (disregard == null || collision.gameObject.transform.parent.gameObject != disregard)
                        {
                            Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                        }
                    }
                }
            }
            if (collision.gameObject.layer == 6)
            {
                if (!AudioSystemScript.instance.IsSoundPlaying())
                    AudioSystemScript.instance.PlaySound(landingSound, transform.position, 0.6f);
                 bourbonEffect.CastGrounding();
            }

        }


        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 12)
            {
                AudioSystemScript.instance.PlaySound(hitSound, transform.position, 1);
                TakeDamage(-10000000);

            }
            if (!attached)
            {
                if (collision.gameObject.CompareTag("RopeBound"))
                {
                    Attach(collision.transform.parent.GetComponent<Rigidbody2D>());
                }
            }
        }

        public void Attach(Rigidbody2D ropeBone)
        {
            rigit.gravityScale = 0;
            attached = true;
            GetComponent<BourbonAttack>().canAttack = false;
        }
        public void Detach()
        {
            anim.SetBool("isOnRope", false);
            rigit.gravityScale = initGravity;
            attached = false;
            GetComponent<BourbonAttack>().canAttack = true;
        }


        //public bool isStillAttachRope()
        //{
        //    float faceDir = anim.GetFloat("faceRight") >= 0 ? 1 : -1;
        //    RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.right * faceDir, 0.001f, ropeBoundLayer);

        //    return hit.collider != null;
        //}
    }
   
   
}
