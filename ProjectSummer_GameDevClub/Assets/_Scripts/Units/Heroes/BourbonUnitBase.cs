using System.Collections;
using UnityEngine;
using System;

public class BourbonUnitBase : UnitBase
{
    public static Stats currStats { get; protected set; }
    
    public static bool _canMove { get;  set; }
    protected static bool canJump;
    public static bool isDead { get; protected set; }
    public static bool inWater;
    public int _bringSword { get;  set; }
    protected int _runLeft;
    protected Animator anim;
    protected BourbonEffectScript bourbonEffect;
    public static event Action<int> changeHeal;
    public static event Action bourbonDead;
    protected Rigidbody2D rigit;
    protected BoxCollider2D box;

    [SerializeField] protected  ScriptableHero scriptableHero;

    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected AudioClip loseSound;

    public void SetStats(Stats stats)
    {
        _Stats = stats;
        currStats = _Stats;
    }
    public void SetStats(ScriptableHero scriptableHero)
    {
        if (currStats.Health == 0)
            currStats = scriptableHero.initStats;
        else
            currStats = scriptableHero.BaseStats;
    }


    /// <summary>
    /// Change the state of character from bringing sword to not of vise vesa 
    /// BringToNoSword is initialy setted true as well as change from bringing to not bringing
    /// </summary>
    /// <param name="BringToNoSword"></param>
    public void ChangeState(bool BringToNoSword = true)
    {
        if (BringToNoSword)
        {
            _bringSword = 0;
        }
        else _bringSword = 1;
        anim.SetFloat("bringSword", _bringSword);

    }
    /// <summary>
    /// Make Character can not move in sec time.
    /// </summary>
    /// <param name="sec"></param>
    /// <returns></returns>
    public IEnumerator CannotMove(float sec)
    {
        _canMove = false;
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(sec);
        }
        _canMove = true;
    }
    public override void TakeDamage(int _dmg)
    {
        if (isDead) return;
        AudioSystemScript.instance.PlaySound(hitSound, transform.position, 1);
        Stats stats = currStats;
        stats.Health = stats.Health + _dmg <= 0 ? 0 : stats.Health + _dmg;
        currStats = stats;
        anim.SetTrigger("isHit");
        bourbonEffect.CastHit();
        changeHeal.Invoke(currStats.Health);
        if (currStats.Health == 0) BourbonDead();
    }
    public void TakeDamage(int _dmg, Transform pos)
    {
        if (isDead) return;
        AudioSystemScript.instance.PlaySound(hitSound, transform.position, 1);
        Stats stats = currStats;
        stats.Health = stats.Health + _dmg <= 0 ? 0 : stats.Health + _dmg;
        currStats = stats;
        anim.SetTrigger("isHit");
        changeHeal.Invoke(currStats.Health);
        bourbonEffect.CastHit(pos);
        if (currStats.Health == 0) BourbonDead();
    }
    public void BuffHeal(int heal)
    {
        Stats stats = currStats;
        stats.Health = stats.Health + heal >= scriptableHero.initStats.Health ? scriptableHero.initStats.Health : stats.Health + heal;
        currStats = stats;
    }
    protected void BourbonDead()
    {
        AudioSystemScript.instance.PlaySound(loseSound,1);
        anim.SetBool("dead", true);
        bourbonEffect.CastBlood();
        bourbonDead?.Invoke();
        isDead = true;
        MainCanvas._instance.DisplayTxt("You Dead", 2);
        SaveStats();
        Invoke("RespawnBourbonAfterDead", 4);
    }
    void RespawnBourbonAfterDead()
    {
        Destroy(gameObject.transform.parent.gameObject);
        Systems.instance.SpawningBourbon();
    }
    protected void OnApplicationQuit()
    {
        scriptableHero.SaveStats(currStats);
    }
    public void  SaveStats()
    {
        scriptableHero.SaveStats(currStats);
    }
}
