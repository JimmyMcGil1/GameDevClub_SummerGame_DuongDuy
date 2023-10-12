using Spine;
using Spine.Unity;
using UnityEngine;
using Units.Bourbon;
using Units.Enemy;

public class EnemyGraphixScript : MonoBehaviour
{
     GameObject bourbon;
     int attackPower;
    private void Start()
    {
        SkeletonAnimation anim = GetComponent<SkeletonAnimation>();
        bourbon = GameObject.FindGameObjectWithTag("Bourbon").gameObject;
        anim.state.Event += OnEvent;
        attackPower = transform.parent.GetComponent<EnemyUnitBase>().attackPower;
    }

    private void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "hit")
        {
            bourbon.GetComponent<BourbonController>().TakeDamage(-attackPower);
        }
        
    }
    
    public void DestroyEnemy()
    {
        Debug.Log("destroy enemy");
        Destroy(gameObject.transform.parent.gameObject);
    }
}
