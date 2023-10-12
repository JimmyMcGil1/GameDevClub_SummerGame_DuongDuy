using System.Collections;
using System.Collections.Generic;
using Units.Enemy;
using UnityEngine;

namespace Testing
{
    public class FlyingSlimeState : StateScene
    {
        [SerializeField] GameObject enemyPref;
        [SerializeField] GameObject jawPref;
        [SerializeField] float spawnTimmer = 1f;
        [SerializeField] GameObject uiInLevel;
        float spawnCounter = Mathf.Infinity;
        GameObject jaw;
        public override void OnEnterState(ControlStateInScene ctrler)
        {
            base.OnEnterState(ctrler);
            jaw  =  Instantiate(jawPref, transform.parent.Find("JawInitPosRight").transform.position, Quaternion.identity);
            Jaw jawSt = jaw.gameObject.GetComponent<Jaw>();
            jawSt.dirMove = Vector2.left;
            jawSt.speed = 3f;
            jawSt.isHor = true;
            uiInLevel?.SetActive(true);
        }
        public override void UpdateState()
        {
            Debug.Log("In slime State");
            base.UpdateState();
            spawnCounter += Time.deltaTime;
            if (spawnCounter > spawnTimmer)
            {
                Instantiate(enemyPref, transform.parent.Find("Summon Pos Top").transform.position, Quaternion.identity);
                Instantiate(enemyPref, transform.parent.Find("Summon Pos Top").transform.position, Quaternion.identity);
                spawnCounter = 0;
            }
        }
        public override void ExitState()
        {
            Destroy(jaw);
        }
    }
}
 
