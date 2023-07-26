using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class BourbonMoveset : BourbonUnitBase
{
    public float hor;
    float oldFace;
    [SerializeField] float speed;
    [SerializeField] float JumpForce;
   
    [SerializeField] LayerMask tilemapLayer;
    [SerializeField] LayerMask ropeBoundLayer;
   
    bool isDashing;
    [SerializeField] float dashingTime;
    [SerializeField] float dashingDis;
    [SerializeField] float fadeTime;
    [SerializeField] float dashTimmer = 0.5f;
     float dashCounter;

    [SerializeField] float dustTimmer = 0.01f;

    [SerializeField] Stats initStats;


    public float pushForce = 10;
    public bool attached = false;
    public Transform attachedTo;
    private GameObject disregard;

    float initGravity;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landingSound;
    float timeCounter = 0;
    float dirAutoMove = 1;

    protected  void Awake()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        rigit = GetComponent<Rigidbody2D>();
        isDead = false; 
        _canMove = true;
        _bringSword = 0;
        _runLeft = 0; 
        bourbonEffect = transform.Find("Effect").gameObject.GetComponent<BourbonEffectScript>();
        canJump = true;
        SetStats(scriptableHero);
        dashCounter = Mathf.Infinity;
        BourbonUI.instance.HealTxtChange(currStats.Health);
    }
   
    private void Update()
    {
        
        hor = Input.GetAxisRaw("Horizontal");
        anim.SetBool("walk", (hor != 0 && _canMove && !inWater ) || (timeCounter > 0));
        anim.SetFloat("faceRight", oldFace);
        if (_canMove && !isDead)
        {
            if (anim.GetFloat("faceRight") < 0)
            {
                Vector2 faceDir = transform.localScale;
                faceDir.x = -1;
                transform.localScale = faceDir;
            }
            else
            {
                Vector2 faceDir = transform.localScale;
                faceDir.x = 1;
                transform.localScale = faceDir;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isDead)
        {
            if ((_canMove && canJump) || inWater || isStillAttachRope())
            {
                NormalJump();
            }
        }
       

        /////////Tesing 
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(-50);
        }
        if (Input.GetKeyDown(KeyCode.E) && !isDead)
        {
            if (dashCounter > dashTimmer)
            {
                StartDash();
                dashCounter = 0;
            }
        }
        dashCounter += Time.deltaTime;

        CheckInputInRope();
    }
    private void FixedUpdate()
    {
        
        if (hor != 0 && _canMove && !isDead && timeCounter == 0 )
        {
            Walk();
        }
        
        if (timeCounter > 0)
        {
            hor = dirAutoMove;
            Walk();
            timeCounter -= Time.deltaTime;
        }
        
        if (IsGround())
        {
            rigit.gravityScale = 3f;
            canJump = true;
        }
        else
        {
            if (!attached)
                rigit.gravityScale = 4f;
            canJump = false;
        }


        //Testing climbing on Rope
        if (attached) rigit.velocity = Vector2.zero;
    }

    void CheckInputInRope()
    {
        if (isStillAttachRope())
        {
            anim.SetBool("isOnRope", true);

            if (Input.GetKey(KeyCode.W) && attached)
            {
                Slide(1);
            }
            else if (Input.GetKey(KeyCode.S) && attached)
            {
                Slide(-1);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && attached)
            {
                Detach();
            }
            else anim.SetFloat("isClimbing", 0);
        }
        else Detach();
        
    }
    public void Attach(Rigidbody2D ropeBone)
    {
        rigit.gravityScale = 0;
        attached = true;
        GetComponent<BourbonAttack>().canAttack = false;
    }
    public void Detach()
    {
        //anim.SetBool("isOnRope", false);
        //hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        //attached = false;
        //hj.enabled = false;
        //hj.connectedBody = null;
        anim.SetBool("isOnRope", false);
        rigit.gravityScale = initGravity;
        attached = false;
        GetComponent<BourbonAttack>().canAttack = true;
    }
    void Slide(int direct)
    {
        
        anim.SetFloat("isClimbing", 1);
        Vector2 currPos = transform.position;
        currPos.y += speed * Time.deltaTime * direct;
        transform.position = currPos;
    }

    bool isStillAttachRope()
    {
        float faceDir = anim.GetFloat("faceRight") >= 0 ? 1 : -1;
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.right * faceDir, 0.001f, ropeBoundLayer);

        return hit.collider != null ;
    }
    public void MoveInDirection(float dir, float sec)
    {
        timeCounter = sec;
        dirAutoMove = dir;
    }
   
    public void Walk()
    {
        oldFace = hor;
        Vector2 curPos = gameObject.transform.position;
        curPos.x += hor * speed * Time.deltaTime;
        gameObject.transform.position = curPos;
    }
    void NormalJump()
    {
        AudioSystemScript.instance.PlaySound(jumpSound,gameObject.transform.position, 0.6f);
        canJump = false;
        anim.SetTrigger("norJump");
        rigit.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
    bool IsGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.08f, tilemapLayer);
       
        return hit.collider != null;
    }
    public void IntoWater()
    {
        inWater = true;
    }
    public void OutWater()
    {
        inWater = false;
        anim.SetBool("isSwimming", false);
    }
   
    void StartDash()
    {
        if (isDashing) return;
        else StartCoroutine(Dasing(dashingTime));
    }
    IEnumerator Dasing(float sec)
    {
        isDashing = true;
        rigit.velocity = Vector2.right * dashingDis * Mathf.Sign(transform.localScale.x) ;
        StartCoroutine(Fade(fadeTime));
        anim.SetBool("isDash",true);
        yield return new WaitForSeconds(sec);
        StartCoroutine(Appear(fadeTime));
        bourbonEffect.CastDustOfDash();
        anim.SetBool("isDash", false);
        isDashing = false;
        rigit.velocity = Vector2.zero;
    }
    public  IEnumerator Appear(float sec)
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        while (color.a < 1f)
        {
            color.a += 0.05f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(sec);
        }
    }
    public  IEnumerator Fade(float sec)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            if (!isDead)
            {
                AudioSystemScript.instance.PlaySound(hitSound, transform.position, 1);
                TakeDamage(-10000000);
            }
            
        }
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
        if (!attached)
        {
            if (collision.gameObject.CompareTag("RopeBound"))
            {
                Attach(collision.transform.parent.GetComponent<Rigidbody2D>());
            }
        }
    }
    
    
}
