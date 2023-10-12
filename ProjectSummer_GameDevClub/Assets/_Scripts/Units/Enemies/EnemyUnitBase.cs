
using System.Collections;
using Units.Bourbon;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Enemy
{

public class EnemyUnitBase : UnitBase
{
    protected Slider healSld;
    protected Text nameTxt;
    protected GameObject enemyGraphix;
    protected Animator anim;
    protected GameObject bourbon;

    protected EnemyCondition condition;
    [SerializeField] protected AudioClip hitSoundClip;

    [SerializeField] protected int maxHp = 100;
    [SerializeField] public int attackPower { get; protected set; } = 30;
        protected int currentHp;
    

    public virtual void TakeDamage(int _dmg)
    {
            AudioSystemScript.instance.PlaySound(hitSoundClip, transform.position, 1);  
            StartCoroutine(DisplayEnemyHealBar(2));
            currentHp = currentHp + _dmg > 0 ? currentHp + _dmg : 0;
            
        }   
    protected IEnumerator DisplayEnemyHealBar(int sec)
    {
        healSld.gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);
        healSld.gameObject.SetActive(false);
    }
    public void RespawnBourbon(GameObject newBourbon)
    {
        this.bourbon = newBourbon;
    }
    }
    public enum EnemyCondition
    {
        idle,
        patroll,
        attack,
        chase,
        dead,
    }
}
