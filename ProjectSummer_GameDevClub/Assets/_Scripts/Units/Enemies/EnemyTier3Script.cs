using System.Collections;
using UnityEngine;
using System;

public class EnemyTier3Script : EnemyUnitBase
{
    [SerializeField] protected float speed;
    [SerializeField] protected Vector2 dirMove;
    [SerializeField] protected float timeInOneDirect = 3;

    [Header ("Attack")]
    [SerializeField] protected Vector2 visionSize;
    [SerializeField] protected Vector2 alligVision;
    [SerializeField] protected float attackRange;
    [SerializeField] protected Vector2 allignAttackBound;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float attackTimmer = 0.4f;
    protected float attackCounter = 0;
    protected float counterInOneDirect;
    [SerializeField] protected bool isHor = true;
    protected virtual void Awake()
    {
        enemyGraphix = transform.Find("EnemyGraphix").gameObject;
        anim = enemyGraphix.GetComponent<Animator>();
        counterInOneDirect = 0;
        bourbon = GameObject.FindGameObjectWithTag("Bourbon").gameObject;
        attackCounter = Mathf.Infinity;
        
    }



    protected void MoveInDirection(Vector2 dir)
    {
        dir.Normalize();

        Vector2 currPos = transform.position;
        currPos.x += dir.x * speed * Time.deltaTime;
        currPos.y += dir.y * speed * Time.deltaTime;
        transform.position = currPos;

    }
    //Enemy patrolling (tuan` tra) in a axis
    protected void Patrolling(float timeInOneDirect, int timeInIdle = 1)
    {
        if (anim.GetBool("idle") == true) return;
        else if (counterInOneDirect > timeInOneDirect)
        {

            StartCoroutine(IdleTime(timeInIdle));
        }
        else
        {
            counterInOneDirect += Time.deltaTime;
            MoveInDirection(dirMove);
        }
        
    }
    IEnumerator IdleTime(int sec)
    {
        anim.SetBool("idle", true);
        yield return new WaitForSeconds(sec);
        anim.SetBool("idle", false);
        dirMove.x *= -1;
        counterInOneDirect = 0;
    }
    protected void CheckIfHitCharacter()
    {
        BoxCollider2D bound = (BoxCollider2D)gameObject.GetComponentInChildren(typeof(BoxCollider2D));
        Vector2 dir = Vector2.right * allignAttackBound.x * dirMove.x + Vector2.up * allignAttackBound.y;
        Collider2D[] hit = Physics2D.OverlapCircleAll(bound.bounds.center + (Vector3)dir, attackRange, playerLayer);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i] != null)
            {
                anim.SetTrigger("attack");
            }
            else continue;
        }

    }
   
    private void OnDrawGizmos()
    {
        //if (!Application.isPlaying) return;
        //Gizmos.color = Color.gray;
        //BoxCollider2D bound = (BoxCollider2D)gameObject.GetComponentInChildren(typeof(BoxCollider2D));
        //Vector2 dir = Vector2.right * allignAttackBound.x * dirMove.x + Vector2.up * allignAttackBound.y;
        //Gizmos.DrawWireSphere(bound.bounds.center + (Vector3)dir, attackRange);
        //Gizmos.DrawLine(bound.bounds.center, bourbon.transform.position);
    }
}
