
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnitBase : UnitBase
{
    protected Slider healSld;
    [SerializeField] protected Stats initStats;
    protected GameObject enemyGraphix;
    protected Animator anim;
    protected GameObject bourbon;

    [SerializeField] protected AudioClip hit;
    public Stats currStats { get; protected set; }
    
    protected virtual void SetStats(Stats stats)
    {
        _Stats = stats;
        currStats = _Stats;
    }
    public override void TakeDamage(int _dmg)
    {
        AudioSystemScript.instance.PlaySound(hit, transform.position, 1);
        enemyGraphix.GetComponent<Animator>().SetTrigger("hit");
        Stats stat = currStats;
        stat.Health = stat.Health - _dmg <= 0 ? 0 : stat.Health - _dmg;
        currStats = stat;
        healSld.value = currStats.Health;
        StartCoroutine(DisplayEnemyHealBar(2));
        if (currStats.Health == 0) Dead();
    }   
    protected IEnumerator DisplayEnemyHealBar(int sec)
    {
        healSld.gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);
        healSld.gameObject.SetActive(false);
    }
    protected virtual void Dead()
    {
        transform.Find("BodyBound").gameObject.SetActive(false);
        anim.SetTrigger("dead");
    }
    
    public virtual void Attack()
    {
        bourbon.GetComponent<BourbonMoveset>().TakeDamage(-currStats.AttackPower);
    }
    public void RespawnBourbon(GameObject newBourbon)
    {
        this.bourbon = newBourbon;
    }
}
