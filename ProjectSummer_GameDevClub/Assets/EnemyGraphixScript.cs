using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGraphixScript : MonoBehaviour
{
    [SerializeField] GameObject enemyCtrller;
    public void Attack()
    {
        enemyCtrller.GetComponent<EnemyUnitBase>().Attack();
    }
    public void DestroyEnemy()
    {
        Debug.Log("destroy enemy");
        Destroy(gameObject.transform.parent.gameObject);
    }
}
