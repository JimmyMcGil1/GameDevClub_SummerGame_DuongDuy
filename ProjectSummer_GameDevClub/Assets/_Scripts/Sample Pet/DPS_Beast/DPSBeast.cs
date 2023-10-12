using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
namespace Units.Pet {
public class DPSBeast : PetFigure
{

    float t = 0;
    Rigidbody2D rigit;
    Vector3 initPos;
    // Start is called before the first frame update
    public override void Awake() {
        base.Awake();
    }
    float attackDelay = 0.5f;
    void Start()
    {
        rigit = GetComponent<Rigidbody2D>();
        flyingPet = false;
        controller = GetComponentInParent<PetController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public override void NormalAttack(Vector3 target)
        {
            float dist = (target - controller.petFigure.transform.position).magnitude;
            PetAttackState controllerAS = controller.attackState;
            controllerAS.attackDelay -= Time.deltaTime;
            
            if (controller.attackState.attackDelay < 0){
                controller.ChangeState(controller.followMState);
            }
            else {
                var currentX = controllerAS.posX +  dist *(0.625 -  (attackDelay - 0.25) * (attackDelay - 0.25)); 
                controller.petFigure.transform.position = new Vector2((float)currentX, controller.petFigure.transform.position.y);
            }
        }
        
        public override void NormalAttackDone()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(cirCol.bounds.center,5f, layerMask: enemyLayer);
            foreach (var hit in hits)
            {
                Debug.Log("Attack done hit collider: " + hit);
              //  Vector2 dirRepel = (hit.transform.position - transform.position).normalized;
                Rigidbody2D hitRigit = hit.GetComponent<Rigidbody2D>();
                hitRigit.velocity = Vector2.up * hitRigit.mass * 3;
                StartCoroutine("StopRepel", hitRigit);
            }
            controller.ChangeState(controller.followMState);

        }
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(cirCol.bounds.center, 5f);
        }   
        IEnumerator StopRepel(Rigidbody2D rigit) {
            yield return new WaitForSeconds(0.1f);
            rigit.velocity = new Vector2(rigit.velocity.x, 0);
        }
        Vector3 BallisticVel(Vector3 target, float angle, float t){
    //     Vector3 dir = target - transform.position;
    //     float h = dir.y;
    //     dir.y = 0;
    //     float dist = dir.magnitude;
    //     float a = angle * Mathf.Deg2Rad;  // convert angle to radians
    //     dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
    //     dist += h / Mathf.Tan(a);  // correct for small height differences
    //     // calculate the velocity magnitude
    //     float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
    //   //  float vel = Mathf.Sqrt(dist * 4 / Mathf.Sin(2 * a));
    //     return vel * dir.normalized;
        float posX = transform.position.x;
        posX = (float)(target.x * (0.25 - (0.5 - t) * (0.5 - t)));
        return new Vector3(posX, transform.position.y, transform.position.z);
        }
}
}
