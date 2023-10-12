using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Spine.Unity;
namespace Units.Bourbon
{
    public class BourbonMoveset : MonoBehaviour
    {

        [Header ("Spine animation")]
        [SpineAnimation]
        public string idleAnimationName;
        [SpineAnimation]
        public string runAnimationName;
        [SpineAnimation]
        public string jumpAnimationName;
        [SpineAnimation]
        public string hangAnimationName;
        [SpineAnimation]
        public string dashAnimationName;
        [SpineAnimation]
        string attackAnimationName;
        
        public SkeletonAnimation sktAnim;
        [SerializeField] InputManagement inputManager;
        
        [Header ("Movement")]
        [SerializeField] float  initSpeed;
        float currentSpeed;
        
        [Header("Jump")]
        [SerializeField] float jumpForce;
        [SerializeField] int maxConsecutiveJump = 1;
        [SerializeField] float gravityFallScale = 3f;
        public int jumpCounter;
        bool jumping = false;
        float jumpDuration = 0.1f;
        [SerializeField] AudioClip jumpSound;
        [SerializeField] Vector2 wallJumpForce;
        float wallJumpDirection = 1f;
       
        [Header ("Dash")]
        bool isDashing;
        [SerializeField] float dashingTime;
        [SerializeField] float dashingDis;
        [SerializeField] float fadeTime;
        [SerializeField] float dashTimmer = 0.5f;
        float dashCounter;

        float oldGravity = 0;

        [SerializeField] Stats initStats;
        public float pushForce = 10;
        public bool attached = false;
        public Transform attachedTo;
        public float timeCounter = 0;
        float dirAutoMove = 1;
        BourbonController controller;
        BourbonEffectScript bourbonEffect;
        public Animator anim;
        CapsuleCollider2D capCol;
        public Rigidbody2D rigit;

        //test scriableobject tile
        [SerializeField] MapManager mapManager;

        public Vector2 faceDir = Vector2.right; // nhan vat xuat hien quay mat qua phai 
        #region Get Input state from Input manager
        public float horizotal { get { if (inputManager != null) { return inputManager.horizontalMovement; } else return 0; } private set { } }
        public bool jumpInput
        {
            get
            {
                if (inputManager != null)
                {
                    return inputManager.jumpStarted;
                }
                else return false;
            }
        }
        public bool jumpHeld
        {
            get
            {
                if (inputManager != null)
                {
                    return inputManager.jumpHeld;
                }
                else return false;
            }
        }
        public bool dashInput
        {
            get
            {
                if (inputManager != null)
                {
                    return inputManager.dashStart;
                }
                else return false;
            }
        }
        #endregion
        protected void Awake()
        {
            anim = GetComponent<Animator>();
            sktAnim = GetComponent<SkeletonAnimation>();
            rigit = GetComponent<Rigidbody2D>();
            capCol = GetComponent<CapsuleCollider2D>();
            bourbonEffect = GetComponent<BourbonEffectScript>();
            dashCounter = Mathf.Infinity;
            controller = gameObject.GetComponent<BourbonController>();
        }
        private void Start()
        {
            controller.ChangeState(controller.idleState);
            sktAnim.state.SetAnimation(0, idleAnimationName, true);
            jumpCounter = 0;
        }
        private void Update()
        {
            //check buff speed 
            // Vector2 checkStandingPos = new Vector2(capCol.bounds.center.x, capCol.bounds.min.y - 0.3f);
            // float buffSpeed =  mapManager.GetTileWalkingSpeed(checkStandingPos);
            // if (buffSpeed != 0) {
            //     currentSpeed = initSpeed + buffSpeed;
            // }
            // else currentSpeed = initSpeed;
            currentSpeed = initSpeed;
           // HandleState();
            HandleFaceDirection();
            controller.UpdateSM();
            dashCounter += Time.deltaTime;

            //CheckInputInRope();
        }
         private void OnGUI() {
            GUI.Label(new Rect(10, 110, 160, 40), $"{controller.currentState}");
        }
        void HandleState()
        {
            if (controller.IsGround())
            {
                controller.isGround = controller.IsGround();
                rigit.gravityScale = 3f;
            }
          //  controller.isHitWall = controller.IsClingingWall();
        }
        private void LateUpdate()
        {
           
            //Testing climbing on Rope
            if (attached) rigit.velocity = Vector2.zero;
        }
        public void Falling()
        {
           
            rigit.AddForce(Vector2.down * rigit.mass * gravityFallScale , ForceMode2D.Force);
        }
        public void TiepDat()
        {
            rigit.gravityScale = 3f;
            jumpCounter = 0;
        }
        //void CheckInputInRope()
        //{
        //    if (controller.isStillAttachRope())
        //    {
        //        anim.SetBool("isOnRope", true);

        //        if (Input.GetKey(KeyCode.W) && attached)
        //        {
        //            Slide(1);
        //        }
        //        else if (Input.GetKey(KeyCode.S) && attached)
        //        {
        //            Slide(-1);
        //        }
        //        else if (Input.GetKeyDown(KeyCode.Space) && attached)
        //        {
        //            controller.Detach();
        //        }
        //        else anim.SetFloat("isClimbing", 0);
        //    }
        //    else controller.Detach();
        //}
      
        void Slide(int direct)
        {

            anim.SetFloat("isClimbing", 1);
            Vector2 currPos = transform.position;
            currPos.y += currentSpeed * Time.deltaTime * direct;
            transform.position = currPos;
        }
        public void MoveInDirection(float dir, float sec)
        {
            timeCounter = sec;
            dirAutoMove = dir;
        }
        #region Input Handle and movement
        // void ProcessInput()
        // {
        //     HandleMovementInput();
        //     if (jumpInput)
        //         HandleJumpInput();
        //     HandleDashInput();
        // }
        public void HandleMovementInput()
        {
          
            Walk();
        }
        public void Walk()
        {
            Vector2 curPos = gameObject.transform.position;
            curPos.x += horizotal * currentSpeed * Time.deltaTime;
            gameObject.transform.position = curPos;
            //  rigit.velocity = new Vector2(currentSpeed * horizotal , rigit.velocity.y);
        }
        public void HandleJumpInput()
        {
            if (jumpCounter < maxConsecutiveJump && controller.condition != CharacterConditions.Dead)
            {
                StartCoroutine("Jump", 1.0f);
                AudioSystemScript.instance.PlaySound(jumpSound, gameObject.transform.position, 0.6f);
            //old anim code    anim.SetTrigger("norJump");
                sktAnim.state.AddAnimation(0, jumpAnimationName, false, -0.6f);
            }
        }
        IEnumerator Jump(float powerMul)
        {
                jumping = true;
                float time = 0;
                rigit.velocity = new Vector2(rigit.velocity.x, 0);
                rigit.AddForce(transform.up * jumpForce * powerMul, ForceMode2D.Impulse);
                while (time < jumpDuration)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
            // rigit.velocity = Vector2.zero;
                jumping = false;
                jumpCounter += 1;
        }
        public void HandleDashInput()
        {

            if (dashCounter > dashTimmer)
            {
                StartDash();
                    dashCounter = 0;
            }
        }
        void StartDash()
        {
            if (isDashing) return;
            else StartCoroutine(Dasing(dashingTime));
        }
        IEnumerator Dasing(float sec)
        {
            sktAnim.state.AddAnimation(0, dashAnimationName, false, -0.6f);
            isDashing = true;
            currentSpeed += dashingDis;
            rigit.velocity = new Vector2(dashingDis * controller.moveSet.faceDir.x, rigit.velocity.y);
         //   StartCoroutine(Fade(fadeTime));
            // anim.SetBool("isDash", true);
            yield return new WaitForSeconds(sec);
           // StartCoroutine(Appear(fadeTime));
            bourbonEffect.CastDustOfDash();
            //anim.SetBool("isDash", false);
            rigit.velocity = new Vector2(0, rigit.velocity.y);
            sktAnim.state.AddAnimation(0, idleAnimationName, true, 0f);
            isDashing = false;
            currentSpeed -= dashingDis;
            //rigit.velocity = Vector2.zero;
            }
        public void HandleFaceDirection()
        {
                if (horizotal != 0)
                {
                    if (horizotal == 1) faceDir = Vector2.right;
                    else faceDir = Vector2.left;
                }
                faceDir.y = transform.localScale.y;
                // faceDir.x = transform.localScale.x * faceDir.x;
                transform.localScale = faceDir;
            // }
        }

        public IEnumerator Appear(float sec)
        {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            while (color.a < 1f)
            {
                color.a += 0.05f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
                yield return new WaitForSeconds(sec);
            }
        }
        public IEnumerator Fade(float sec)
        {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            while (color.a >= 0.605)
            {
                color.a -= 0.05f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
                yield return new WaitForSeconds(sec);
            }
            color.a = 0f;
        }
        #endregion
        #region JumpOnWall
        public void ClingingOnWall()
        {
            oldGravity = rigit.gravityScale;
            rigit.velocity = Vector2.zero;
            rigit.gravityScale = 0;
        }
        //public void Fall()
        //{
        //    rigit.gravityScale = 5;
        //    rigit.AddForce(Vector2.down, ForceMode2D.Impulse);
        //}
        public void WallJumpHandle()
        {
     
            StartCoroutine("WallJump", 1);
        }
        IEnumerator WallJump(float powerMul)
        {
            jumping = true;
            float time = 0;
            rigit.velocity = new Vector2(rigit.velocity.x, 0);
            if (transform.localScale.x > 0)
                wallJumpDirection = -1;
            else wallJumpDirection = 1;
            Vector2 wallJumpVector = new Vector2(wallJumpDirection * wallJumpForce.x * rigit.mass, Mathf.Sqrt(2f * wallJumpForce.y * rigit.mass)); 
          //  rigit.AddForce(wallJumpVector, ForceMode2D.Impulse);
            rigit.velocity = wallJumpVector; 
            while (time < 0.5f)
            {
                time += Time.deltaTime;
                yield return null;
            }
            rigit.velocity = new Vector2(0, rigit.velocity.y);
            jumping = false;
        }
        #endregion
        public bool PlayerWantClingeOnWall() {
            if (controller.HitWallLeft() && controller.moveSet.horizotal == -1)
                return true;
            else if (controller.HitWallRight() && controller.moveSet.horizotal == 1) return true;
            return false;
        }
    }
}
