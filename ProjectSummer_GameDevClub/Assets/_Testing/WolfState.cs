using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Units.Enemy;
using UnityEngine;

namespace Testing
{
    public class WolfState : StateScene
    {
        [SerializeField]  GameObject wolfPref;
        List<GameObject> currentPool = new List<GameObject>();

        public override void OnEnterState(ControlStateInScene ctrler)
        {
            base.OnEnterState(ctrler);
            GameObject wolfL = Instantiate(wolfPref, transform.parent.Find("Summon Pos Left").transform.position, Quaternion.identity);
            GameObject wolfR = Instantiate(wolfPref, transform.parent.Find("Summon Pos Right").transform.position, Quaternion.identity);
            currentPool.Add(wolfL);
            currentPool.Add(wolfR);
            Vector2 scale = new Vector2(-1, 1);
            wolfL.transform.Find("EnemyGraphix").transform.localScale = scale;
            wolfL.GetComponent<EnemyTier3Script>().dirMove = Vector2.right;
            wolfR.GetComponent<EnemyTier3Script>().dirMove = Vector2.left;

        }
        public override void UpdateState()
        {
            base.UpdateState();
            foreach (var item in currentPool)
            {
                if (item != null) return;
            }
            controller.ChangeState(transform.parent.Find("FlyingSlimeState").GetComponent<StateScene>());
        }
    }
}
