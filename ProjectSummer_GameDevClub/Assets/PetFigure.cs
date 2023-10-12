using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;
using Spine;
using Units.Pet;
using System.Linq.Expressions;
namespace Units.Pet {
public class PetFigure : MonoBehaviour
{
    SkeletonAnimation anim;
    GameObject chimera;
    [SerializeField] GameObject lazerRayPref;
    [SerializeField] float attackTimmer = 1;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] public bool flyingPet {get;  set;} = true;
    float attackCounter;
    public GameObject bourbon {get; private set;}
    GameObject currentTargetEnemy;
    public CircleCollider2D cirCol;
    protected PetController controller;
    public virtual void Awake()
    {
        anim = GetComponent<SkeletonAnimation>();
        chimera = GameObject.FindGameObjectWithTag("Enemy");
        bourbon = GameObject.FindGameObjectWithTag("Bourbon");
        cirCol = gameObject.GetComponent<CircleCollider2D>();
        
    }
    private void Start()
    {
        anim.AnimationName = "action/idle/normal";
        attackCounter = 0;
    }
    private void Update()
    {
    }
    public void JumpOnEnemy()
    {
        if (chimera != null)
        {
            Transform parent = transform.parent;
            parent.DOMove(chimera.transform.position, 0.3f).SetLoops(2, LoopType.Yoyo);
        }
    }
    public bool HitWall() {
            CircleCollider2D cirCol = gameObject.GetComponent<CircleCollider2D>();
            RaycastHit2D rhit = Physics2D.Raycast(new Vector2(cirCol.bounds.max.x, cirCol.bounds.center.y), Vector2.right, 0.5f, wallLayer);
            RaycastHit2D lhit = Physics2D.Raycast(new Vector2(cirCol.bounds.min.x, cirCol.bounds.center.y), Vector2.left, 0.5f, wallLayer);
            return rhit.collider != null || lhit.collider != null;
    }
    public bool HitGround() {
            CircleCollider2D cirCol = gameObject.GetComponent<CircleCollider2D>();
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(cirCol.bounds.center.x, cirCol.bounds.min.y), Vector2.down, 0.01f, groundLayer);
            return hit.collider != null;
    }
    public void Fire()
    {
        gameObject.GetComponent<BulletController>().FireParabol();
    }
   
    public virtual void NormalAttack(Vector3 target) {
         Transform firePos = transform.parent.Find("firePos");
        GameObject lazer = Instantiate(lazerRayPref, firePos.position, Quaternion.identity);
    
        Vector3 dir = target - lazer.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lazer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        lazer.GetComponent<RayLazer>().Fire();
    }
    
    public virtual void NormalAttackDone() {
        
    }
}
}