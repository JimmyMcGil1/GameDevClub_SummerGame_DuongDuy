using UnityEngine;
using UnityEngine.UI;
namespace Units.Enemy
{
    public class GauMatHi : EnemyTier3Script
    {
        [SerializeField] float checkAttackDis;
        private void Start()
        {
            healSld = transform.Find("Enemy_t1_2_Canvas").Find("healSld").gameObject.GetComponent<Slider>();
            healSld.maxValue = _Stats.Health;
            healSld.value = _Stats.Health;
            healSld.gameObject.SetActive(false);

        }

        private void Update()
        {
            ChangeDir();
            Patrolling(timeInOneDirect, 2);
            if (Mathf.Abs(enemyGraphix.transform.position.x - bourbon.transform.position.x) <= checkAttackDis)
            {
                if (attackCounter > attackTimmer)
                {
                    CheckIfHitCharacter();
                    attackCounter = 0;
                }
            }
            attackCounter += Time.deltaTime;
        }
        void ChangeDir()
        {
            Vector2 loc = enemyGraphix.transform.localScale;
            if (dirMove.x > 0)
            {
                loc.x = -1;
                enemyGraphix.transform.localScale = loc;
            }
            else
            {
                loc.x = 1;
                enemyGraphix.transform.localScale = loc;
            }
        }
    }
}